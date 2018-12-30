using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.CoreServices.AccessControl.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Services;
using Bam.Net.CoreServices.AccessControl.Data.Dao.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.AccessControl
{
    [Proxy("accessControlSvc")]
    [ApiKeyRequired]
    [ServiceSubdomain("accesscontrol")]
    public class AccessControlService : AsyncProxyableService
    {
        protected AccessControlService() { }

        public AccessControlService(AccessControlRepository accessControlServiceRepository)
        {
            AccessControlRepository = accessControlServiceRepository;
            AccessControlRepository.EnsureDaoAssemblyAndSchema();
        }

        public AccessControlRepository AccessControlRepository { get; }

        public ServiceResponse<ResourceInfo> GetResourceInfo(string resourcePath)
        {
            try
            {
                ResourceUri resourceUri = new ResourceUri(resourcePath);
                ResourceHost resourceHost = AccessControlRepository.GetOneResourceHostWhere(h => h.Name == resourceUri.Host);
                Resource resource = AccessControlRepository.GetOneResourceWhere(r => r.ResourceHostId == resourceHost.Id && r.FullPath == resourceUri.Path);
                if (string.IsNullOrEmpty(resource.Name))
                {
                    resource.Name = resourceUri.PathSegments[resourceUri.PathSegments.Length - 1];
                    AccessControlRepository.SaveAsync(resource);
                }
                return GetSuccessResponse(resource.ToInfo(AccessControlRepository));
            }
            catch (Exception ex)
            {
                return GetErrorResponse<ResourceInfo>(ex);
            }
        }

        public ServiceResponse Deny(ulong resourceId, string userIdentifier)
        {
            try
            {
                List<PermissionSpecification> perms = AccessControlRepository.PermissionSpecificationsWhere(p => p.ResourceId == resourceId && p.UserIdentifier == userIdentifier).ToList();
                foreach(PermissionSpecification perm in perms)
                {
                    AccessControlRepository.Delete(perm);
                }

                return AddResourcePermission(resourceId, new PermissionSpecification { ResourceId = resourceId, UserIdentifier = userIdentifier, Permission = Permissions.None });
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex);
            }
        }

        public ServiceResponse<PermissionSpecificationInfo> AddResourcePermission(ulong resourceId, PermissionSpecification permission)
        {
            try
            {
                Args.ThrowIfNull(permission);
                Args.ThrowIfNullOrEmpty(permission.UserIdentifier);
                Resource resource = AccessControlRepository.Retrieve<Resource>(resourceId);
                Args.ThrowIfNull(resource);
                PermissionSpecification toSave = permission.CopyAs<PermissionSpecification>();
                toSave.Id = 0; // resave
                toSave.ResourceId = resourceId;
                toSave = AccessControlRepository.Save(toSave);
                return GetSuccessResponse(toSave.ToInfo(AccessControlRepository));
            }
            catch (Exception ex)
            {
                return GetErrorResponse<PermissionSpecificationInfo>(ex);
            }
        }

        public ServiceResponse<ResourceInfo> RemoveResourcePermission(ulong resourceId, ulong permissionSpecificationId)
        {
            try
            {
                PermissionSpecification perm = AccessControlRepository.Retrieve<PermissionSpecification>(permissionSpecificationId);
                Args.ThrowIf(perm.ResourceId != resourceId, "Specified permission {0} is not for the specified resource {1}", permissionSpecificationId, resourceId);

                return new ServiceResponse<ResourceInfo> { Success = AccessControlRepository.Delete(perm), Data = AccessControlRepository.Retrieve<Resource>(resourceId).ToInfo(AccessControlRepository) };
            }
            catch (Exception ex)
            {
                return GetErrorResponse<ResourceInfo>(ex);
            }
        }

        public ServiceResponse<ResourceInfo> SetResourcePermissions(ulong resourceId, params PermissionSpecification[] permissions)
        {
            try
            {
                Resource resource = AccessControlRepository.Retrieve<Resource>(resourceId);
                Args.ThrowIfNull(resource);
                Parallel.ForEach(permissions, (ps) =>
                {
                    PermissionSpecification copy = ps.CopyAs<PermissionSpecification>();
                    copy.Id = 0;
                    copy.ResourceId = resourceId;
                    AccessControlRepository.Save(copy);
                });
                return GetSuccessResponse(resource.ToInfo(AccessControlRepository));
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ResourceInfo> { Success = false, Message = ex.Message };
            }
        }

        public override object Clone()
        {
            AccessControlService clone = new AccessControlService();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        private ServiceResponse<T> GetErrorResponse<T>(Exception ex) where T : new()
        {
            return new ServiceResponse<T> { Success = false, Message = ex.Message };
        }

        private ServiceResponse GetErrorResponse(Exception ex)
        {
            return new ServiceResponse { Success = false, Message = ex.Message };
        }

        private ServiceResponse<T> GetSuccessResponse<T>(T data) where T: new()
        {
            return new ServiceResponse<T> { Success = true, Data = data };
        }
    }
}
