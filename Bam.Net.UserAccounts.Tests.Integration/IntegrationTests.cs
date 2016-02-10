﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using System.Reflection;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts.Tests;
using Bam.Net.UserAccounts.Data;
using FakeItEasy;

namespace Bam.Net.UserAccounts.Tests.Integration
{
    [IntegrationTestContainer]
    public class IntegrationTests
    {
        [IntegrationTest]
        public void ResetPasswordShouldBeLoginnable()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            string email = "bryan.apellanes@gmail.com";
            UserTestTools.SignUp(userName, email);
            User user = User.GetByEmail(email);
            Expect.AreEqual(0, user.PasswordResetsByUserId.Count);

            UserManager userMgr = UserTestTools.CreateTestUserManager();
            userMgr.HttpContext = A.Fake<IHttpContext>();
            userMgr.HttpContext.Request = new TestRequest();

            string password = ServiceProxySystem.GenerateId();
            ForgotPasswordResponse forgot = userMgr.ForgotPassword(email);
            PasswordResetResponse reset = userMgr.ResetPassword(password.Sha1(), (string)forgot.Data);
            LoginResponse login = userMgr.Login(user.UserName, password.Sha1());
            Expect.IsTrue(login.Success, "Login failed");
        }

        [IntegrationTest]
        public void ResetPasswordShouldSucceed()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            string email = "bryan.apellanes@gmail.com";
            UserTestTools.SignUp(userName, email);
            User user = User.GetByEmail(email);
            Expect.AreEqual(0, user.PasswordResetsByUserId.Count);

            UserManager userMgr = UserTestTools.CreateTestUserManager();
            userMgr.HttpContext = A.Fake<IHttpContext>();
            userMgr.HttpContext.Request = new TestRequest();

            string password = ServiceProxySystem.GenerateId();
            ForgotPasswordResponse forgot = userMgr.ForgotPassword(email);
            PasswordResetResponse reset = userMgr.ResetPassword(password.Sha1(), (string)forgot.Data);
            Expect.IsTrue(reset.Success, "Reset failed");
        }

        [IntegrationTest]
        public void ForgotPasswordShouldSucceed()
        {
            UserTestTools.ClearAllUserInfo();
            string userName = MethodBase.GetCurrentMethod().Name;
            string email = "bryan.apellanes@gmail.com";
            UserTestTools.SignUp(userName, email);
            User user = User.GetByEmail(email);
            Expect.AreEqual(0, user.PasswordResetsByUserId.Count);

            UserManager userMgr = UserTestTools.CreateTestUserManager();
            userMgr.HttpContext = A.Fake<IHttpContext>();
            userMgr.HttpContext.Request = new TestRequest();

            ForgotPasswordResponse response = userMgr.ForgotPassword(email);

            Expect.IsTrue(response.Success);
        }

        [IntegrationTest]
        public void ShouldBeAbleToRequestConfirmationEmail()
        {
            UserManager mgr = UserTestTools.CreateTestUserManager();
            mgr.HttpContext = A.Fake<IHttpContext>();
            mgr.HttpContext.Request = new TestRequest();
            UserTestTools.SignUp("monkey", "bryan.apellanes@gmail.com");
            SendEmailResponse result = mgr.RequestConfirmationEmail("bryan.apellanes@gmail.com");

            Expect.IsTrue(result.Success, result.Message);
        }

        [IntegrationTest]
        public void ForgotPasswordShouldCreatePasswordResetEntry()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            string email = "bryan.apellanes@gmail.com";
            UserTestTools.SignUp(userName, email);
            User user = User.GetByEmail(email);
            Expect.AreEqual(0, user.PasswordResetsByUserId.Count);

            UserManager userMgr = UserTestTools.CreateTestUserManager();
            userMgr.HttpContext = A.Fake<IHttpContext>();
            userMgr.HttpContext.Request = new TestRequest();

            userMgr.ForgotPassword(email);

            user.PasswordResetsByUserId.Reload();
            Expect.AreEqual(1, user.PasswordResetsByUserId.Count);
        }
    }
}