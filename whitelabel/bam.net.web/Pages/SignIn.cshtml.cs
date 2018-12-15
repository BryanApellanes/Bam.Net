using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net.Presentation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bam.Net.CoreServices;
using Bam.Net.Services;

namespace Bam.Net.Web.Pages
{
    public class SignInModel : PageModel
    {
        public SignInModel(ApplicationServiceRegistry applicationServiceRegistry)
        {
            UserNamePlaceHolder = "UserName";
            PasswordPlaceHolder = "Password";
            ForgotPasswordText = "Forgot Password?";
            RememberMeText = "Remember me";
            LoginText = "Sign In";
            SignupText = "Sign Up";
            ApplicationServiceRegistry = applicationServiceRegistry;
        }

        public ApplicationServiceRegistry ApplicationServiceRegistry { get; set; }

        public string UserNamePlaceHolder { get; set; }
        public string PasswordPlaceHolder { get; set; }
        public string RememberMeText { get; set; }
        public string ForgotPasswordText { get; set; }
        public string LoginText { get; set; }
        public string SignupText { get; set; }

        public bool ShowOAuthOptions
        {
            get
            {
                return OAuthSettings != null && OAuthSettings.Count() > 0;
            }
        }

        public IEnumerable<OAuthSetting> OAuthSettings { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}