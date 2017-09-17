/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.ServiceProxy.Secure;
using System.Web.Security;
using Bam.Net.Encryption;

namespace Bam.Net.ServiceProxy
{
    public enum ValidationFailures
    {
        None,
        TokenValidationError,
        UnknownTokenValidationResult,
        TokenHashFailed,
        TokenNonceFailed,
        ClassNameNotSpecified,
        ClassNotRegistered,
        MethodNameNotSpecified,
        MethodNotFound,
        MethodNotProxied,
        ParameterCountMismatch,
        PermissionDenied,
        InvalidApiKeyToken,
        RemoteExecutionNotAllowed
    }

    public class ValidationResult
    {
        ExecutionRequest _toValidate;
        public ValidationResult() { }
        public ValidationResult(ExecutionRequest request, string messageDelimiter = null)
        {
            this._toValidate = request;
            this.Delimiter = messageDelimiter ?? ",\r\n";
        }
        
        internal void Execute(IHttpContext context, Decrypted input)
        {
            List<ValidationFailures> failures = new List<ValidationFailures>();
            List<string> messages = new List<string>();

            ValidateEncryptedToken(context, input, failures, messages);
            
            if (string.IsNullOrWhiteSpace(_toValidate.ClassName))
            {
                failures.Add(ServiceProxy.ValidationFailures.ClassNameNotSpecified);
                messages.Add("ClassName not specified");
            }

            if (_toValidate.TargetType == null)
            {
                failures.Add(ServiceProxy.ValidationFailures.ClassNotRegistered);
                messages.Add("Class {0} was not registered as a proxied service.  Register the class with the ServiceProxySystem first."._Format(_toValidate.ClassName));
            }

            if (string.IsNullOrWhiteSpace(_toValidate.MethodName))
            {
                failures.Add(ServiceProxy.ValidationFailures.MethodNameNotSpecified);
                messages.Add("MethodName not specified");
            }

            if (_toValidate.TargetType != null && _toValidate.MethodInfo == null)
            {
                failures.Add(ServiceProxy.ValidationFailures.MethodNotFound);
                string message = "Method ({0}) was not found"._Format(_toValidate.MethodName);
                if (!failures.Contains(ServiceProxy.ValidationFailures.ClassNameNotSpecified))
                {
                    message = "{0} on class ({1})"._Format(message, _toValidate.ClassName);
                }
                messages.Add(message);
            }

            if (_toValidate.TargetType != null && 
                _toValidate.MethodInfo != null &&
                _toValidate.MethodInfo.HasCustomAttributeOfType(out ExcludeAttribute attr))
            {
                if(attr is LocalAttribute)
                {
                    if (!context.Request.UserHostAddress.StartsWith("127.0.0.1"))
                    {
                        failures.Add(ServiceProxy.ValidationFailures.RemoteExecutionNotAllowed);
                        messages.Add("The specified method is marked [Local] and the request was directed to {0}: {1}"._Format(context.Request.UserHostAddress, _toValidate.MethodName));
                    }
                }
                else
                {
                    failures.Add(ServiceProxy.ValidationFailures.MethodNotProxied);
                    messages.Add("The specified method has been explicitly excluded from being proxied: {0}"._Format(_toValidate.MethodName));
                }
            }

            if (_toValidate.ParameterInfos != null && _toValidate.ParameterInfos.Length != _toValidate.Parameters.Length)
            {
                failures.Add(ServiceProxy.ValidationFailures.ParameterCountMismatch);
                messages.Add("Wrong number of parameters specified: expected ({0}), recieved ({1})"._Format(_toValidate.ParameterInfos.Length, _toValidate.Parameters.Length));
            }

            if (_toValidate.TargetType != null &&
                _toValidate.MethodInfo != null &&
                _toValidate.MethodInfo.HasCustomAttributeOfType(true, out RoleRequiredAttribute requiredMethodRoles))
            {
                if (requiredMethodRoles.Roles.Length > 0)
                {
                    CheckRoles(failures, messages, requiredMethodRoles, context);
                }
            }

            if (_toValidate.TargetType != null &&
                _toValidate.TargetType.HasCustomAttributeOfType(true, out RoleRequiredAttribute requiredClassRoles))
            {
                if (requiredClassRoles.Roles.Length > 0)
                {
                    CheckRoles(failures, messages, requiredClassRoles, context);
                }
            }

            ApiKeyRequiredAttribute keyRequired;
            if (_toValidate.TargetType != null &&
                _toValidate.MethodInfo != null &&
                (
                    _toValidate.TargetType.HasCustomAttributeOfType(true, out keyRequired) ||
                    _toValidate.MethodInfo.HasCustomAttributeOfType(true, out keyRequired)                
                ))
            {
                ValidateApiKeyToken(failures, messages);
            }

            ValidationFailures = failures.ToArray();
            Message = messages.ToArray().ToDelimited(s => s, Delimiter);
            this.Success = failures.Count == 0;
        }

        private void ValidateApiKeyToken(List<ValidationFailures> failures, List<string> messages)
        {
            IApiKeyResolver resolver = _toValidate.ApiKeyResolver;
            if (!resolver.IsValidRequest(_toValidate))
            {
                failures.Add(ServiceProxy.ValidationFailures.InvalidApiKeyToken);
                messages.Add("ApiKeyValidation failed");
            }
        }

        private static void ValidateEncryptedToken(IHttpContext context, Decrypted input, List<ValidationFailures> failures, List<string> messages)
        {
            if (input != null)
            {
                try
                {
                    EncryptedTokenValidationStatus tokenStatus = ApiEncryptionValidation.ValidateEncryptedToken(context, input.Value);
                    switch (tokenStatus)
                    {
                        case EncryptedTokenValidationStatus.Unkown:
                            failures.Add(ServiceProxy.ValidationFailures.UnknownTokenValidationResult);
                            messages.Add("ApiEncryptionValidation.ValidateToken failed");
                            break;
                        case EncryptedTokenValidationStatus.HashFailed:
                            failures.Add(ServiceProxy.ValidationFailures.TokenHashFailed);
                            messages.Add("ApiEncryptionValidation.ValidateToken failed: TokenHashFailed");
                            break;
                        case EncryptedTokenValidationStatus.NonceFailed:
                            failures.Add(ServiceProxy.ValidationFailures.TokenNonceFailed);
                            messages.Add("ApiEncryptionValidation.ValidateToken failed: TokenNonceFailed");
                            break;
                        case EncryptedTokenValidationStatus.Success:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    failures.Add(ServiceProxy.ValidationFailures.TokenValidationError);
                    messages.Add(ex.Message);
                }
            }
        }

        private static void CheckRoles(List<ValidationFailures> failures, List<string> messages, RoleRequiredAttribute requiredRoles, IHttpContext context)
        {
            IUserResolver userResolver = (IUserResolver)ServiceProxySystem.UserResolvers.Clone();
            IRoleResolver roleResolver = (IRoleResolver)ServiceProxySystem.RoleResolvers.Clone();
            userResolver.HttpContext = context;
            roleResolver.HttpContext = context;
            List<string> userRoles = new List<string>(roleResolver.GetRoles(userResolver));
            bool passed = false;
            for (int i = 0; i < requiredRoles.Roles.Length; i++)
            {
                string requiredRole = requiredRoles.Roles[i];
                if (userRoles.Contains(requiredRole))
                {
                    passed = true;
                    break;
                }
            }

            if (!passed)
            {
                failures.Add(ServiceProxy.ValidationFailures.PermissionDenied);
                messages.Add("Permission Denied");
            }
        }

        internal string Delimiter { get; set; }
        public bool Success { get; set; }
        public string Message { get; private set; }
        public ValidationFailures[] ValidationFailures { get; set; }
    }
}
