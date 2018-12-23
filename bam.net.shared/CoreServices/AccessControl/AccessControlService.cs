using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.CoreServices.AccessControl.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Services;

namespace Bam.Net.CoreServices.AccessControl
{
    [Proxy("accessControlSvc")]
    [ApiKeyRequired]
    [ServiceSubdomain("accesscontrol")]
    public class AccessControlService : AsyncProxyableService
    {
        public ServiceResponse GetResourceInfo(string resourcePath)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public ServiceResponse AddResourcePermission(ulong resourceId, PermissionSpecification permission)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public ServiceResponse RemoveResourcePermission(ulong resourceId, ulong permissionSpecificationId)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public ServiceResponse SetResourcePermissions(ulong resourceId, params PermissionSpecification[] permissions)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public override object Clone()
        {
            AccessControlService clone = new AccessControlService();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
