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
        InvalidApiKeyToken
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

            ValidateApiToken(context, input, failures, messages);
            
            if (string.IsNullOrWhiteSpace(_toValidate.ClassName))
            {
                failures.Add(ValidationFailures.ClassNameNotSpecified);
                messages.Add("ClassName not specified");
            }

            if (_toValidate.TargetType == null)
            {
                failures.Add(ValidationFailures.ClassNotRegistered);
                messages.Add("Class {0} was not registered as a proxied service.  Register the class with the ServiceProxySystem first."._Format(_toValidate.ClassName));
            }

            if (string.IsNullOrWhiteSpace(_toValidate.MethodName))
            {
                failures.Add(ValidationFailures.MethodNameNotSpecified);
                messages.Add("MethodName not specified");
            }

            if (_toValidate.TargetType != null && _toValidate.MethodInfo == null)
            {
                failures.Add(ValidationFailures.MethodNotFound);
                string message = "Method ({0}) was not found"._Format(_toValidate.MethodName);
                if (!failures.Contains(ValidationFailures.ClassNameNotSpecified))
                {
                    message = "{0} on class ({1})"._Format(message, _toValidate.ClassName);
                }
                messages.Add(message);
            }

            if (_toValidate.TargetType != null && 
                _toValidate.MethodInfo != null &&
                _toValidate.MethodInfo.HasCustomAttributeOfType<ExcludeAttribute>())
            {
                failures.Add(ValidationFailures.MethodNotProxied);
                messages.Add("The specified method has been explicitly excluded from being proxied: {0}"._Format(_toValidate.MethodName));
            }

            if (_toValidate.ParameterInfos != null && _toValidate.ParameterInfos.Length != _toValidate.Parameters.Length)
            {
                failures.Add(ValidationFailures.ParameterCountMismatch);
                messages.Add("Wrong number of parameters specified: expected ({0}), recieved ({1})"._Format(_toValidate.ParameterInfos.Length, _toValidate.Parameters.Length));
            }

            RoleRequiredAttribute requiredMethodRoles;
            if (_toValidate.TargetType != null && 
                _toValidate.MethodInfo != null &&
                _toValidate.MethodInfo.HasCustomAttributeOfType<RoleRequiredAttribute>(true, out requiredMethodRoles))
            {
                if (requiredMethodRoles.Roles.Length > 0)
                {
                    CheckRoles(failures, messages, requiredMethodRoles, context);
                }
            }

            RoleRequiredAttribute requiredClassRoles;
            if (_toValidate.TargetType != null &&
                _toValidate.TargetType.HasCustomAttributeOfType<RoleRequiredAttribute>(true, out requiredClassRoles))
            {
                if (requiredClassRoles.Roles.Length > 0)
                {
                    CheckRoles(failures, messages, requiredClassRoles, context);
                }
            }

            ApiKeyRequiredAttribute keyRequired;
            if(_toValidate.TargetType != null &&
                _toValidate.TargetType.HasCustomAttributeOfType<ApiKeyRequiredAttribute>(true, out keyRequired))
            {
                ValidateApiKeyToken(failures, messages);
            }

            ValidationFailure = failures.ToArray();
            Message = messages.ToArray().ToDelimited(s => s, Delimiter);
            this.Success = failures.Count == 0;
        }

        private void ValidateApiKeyToken(List<ValidationFailures> failures, List<string> messages)
        {
            ApiKeyResolver resolver = _toValidate.ApiKeyResolver;
            if (!resolver.IsValid(_toValidate))
            {
                failures.Add(ValidationFailures.InvalidApiKeyToken);
                messages.Add("ApiKeyValidation failed");
            }
        }

        private static void ValidateApiToken(IHttpContext context, Decrypted input, List<ValidationFailures> failures, List<string> messages)
        {
            if (input != null)
            {
                try
                {
                    TokenValidationStatus tokenStatus = ApiValidation.ValidateToken(context, input.Value);
                    switch (tokenStatus)
                    {
                        case TokenValidationStatus.Unkown:
                            failures.Add(ValidationFailures.UnknownTokenValidationResult);
                            messages.Add("ApiValidation.ValidateToken failed");
                            break;
                        case TokenValidationStatus.HashFailed:
                            failures.Add(ValidationFailures.TokenHashFailed);
                            messages.Add("ApiValidation.ValidateToken failed: TokenHashFailed");
                            break;
                        case TokenValidationStatus.NonceFailed:
                            failures.Add(ValidationFailures.TokenNonceFailed);
                            messages.Add("ApiValidation.ValidateToken failed: TokenNonceFailed");
                            break;
                        case TokenValidationStatus.Success:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    failures.Add(ValidationFailures.TokenValidationError);
                    messages.Add(ex.Message);
                }
            }
        }

        private static void CheckRoles(List<ValidationFailures> failures, List<string> messages, RoleRequiredAttribute requiredRoles, IHttpContext context)
        {
            IUserResolver userResolver = ServiceProxySystem.UserResolvers;
            IRoleResolver roleResolver = ServiceProxySystem.RoleResolvers;

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
                failures.Add(ValidationFailures.PermissionDenied);
                messages.Add("Permission Denied");
            }
        }

        internal string Delimiter { get; set; }
        public bool Success { get; set; }
        public string Message { get; private set; }
        public ValidationFailures[] ValidationFailure { get; set; }
    }
}
