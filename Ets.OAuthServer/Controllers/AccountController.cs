using System.Globalization;
using System.Text.RegularExpressions;
using Ets.OAuthServer;
using Ets.OAuthServer.Utility;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ets.OAuthServer.Bll.IBll;
using Microsoft.AspNet.Identity;
using Ets.Common.Utility;
namespace Ets.OAuthServer
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public IAuthInfoBll Bll { get; set; }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
             
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // GET: /Account/CodeLogin
        [AllowAnonymous]
        public ActionResult CodeLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult LoginSuccess(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.PhoneNumber, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:                   
                default:
                    ModelState.AddModelError("", "用户名或密码错误~");
                    return View(model);
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CodeLogin(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = await UserManager.FindByNameAsync(model.PhoneNumber);          
            var verifyResult = await UserManager.UserTokenProvider.ValidateAsync("Login", model.Code, UserManager, user);         
            if (!verifyResult)//登录成功
            {
                ModelState.AddModelError("", "用户名或密码错误~");
                return View(model);
            }

            user.PhoneNumberConfirmed = true;
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "登录失败~");
                return View(model);               
            }
            return View("LoginSuccess");
        }
       
            //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //return RedirectToAction("Index", "Manage");
                    return RedirectToAction("Index", "Home");
                    //return View("DisplayEmail");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

                [AllowAnonymous]
        public ActionResult CodeForgotPassword()
        {
            return View();
        }


        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CodeForgotPassword(CodeForgotPasswordViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.PhoneNumber);
                if (user==null)
                {
                    return View("ForgotPasswordConfirmation");
                }

                //验证码验证
                var code = await UserManager.UserTokenProvider.GenerateAsync("EtsForgot", UserManager, user);
                if (!code.Equals(model.Code))
                {
                    return View(model);
                }

                //获取密码hash值
                PasswordHasher passwordHasher = new PasswordHasher();
                var passWord = passwordHasher.HashPassword(model.NewPassWord);
                user.PasswordHash = passWord;

                var result = UserManager.UpdateAsync(user);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return View("ForgotPasswordConfirmation");
                }
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode     
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var user = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie);
            var userId1 = user.Identity.GetUserId();
            var code = await UserManager.GenerateUserTokenAsync("ryan", userId1);

            var verifyResult = await UserManager.VerifyUserTokenAsync(userId1, "ryan", code);
            if (verifyResult)
            {
                return Content("haha");
            }

            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           
            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendVerificateChangeCode(string phoneNumber)
        {
            phoneNumber = "13148372114";
            if (phoneNumber.IsNullOrWhiteSpace()||!CommonUtility.IsMobilephone(phoneNumber))
            {
                return new JsonResult
                {
                    Data = new
                    {
                        State = false,
                        Message = "手机号码错误"
                    }
                };
            }
          

            var user = await UserManager.FindByNameAsync(phoneNumber);
            if (user == null) //验证码注册
            {
                var applicationUser = new ApplicationUser { PhoneNumber = phoneNumber, UserName = phoneNumber };
                var result = await UserManager.CreateAsync(applicationUser);
                if (result.Succeeded)
                {
                    user = applicationUser;
                }
            }

            var tmpcode = await UserManager.UserTokenProvider.GenerateAsync("EtsChange", UserManager, user);
            var content = "您的验证码：" + tmpcode + "，请在5分钟内填写。此验证码只用于修改密码，如非本人操作，请不要理会。";

            SmsHelper etaoshiSMS = new SmsHelper();
            string mess = string.Empty;
            etaoshiSMS.SendSmsSaveLog(phoneNumber, content, "EtsChange", 0);
            return new JsonResult
            {
                Data = new
                {
                    State = true,
                    Message = "发送验证码成功",
                }
            };         
        }

        //发送忘记密码验证码
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendForgotVerificateCode(string phoneNumber)
        {
            if (phoneNumber.IsNullOrWhiteSpace() || !CommonUtility.IsMobilephone(phoneNumber))
            {
                return new JsonResult
                {
                    Data = new
                    {
                        State = false,
                        Message = "手机号码错误"
                    }
                };
            }


            var user = await UserManager.FindByNameAsync(phoneNumber);
            if (user == null) //验证码注册
            {
                var applicationUser = new ApplicationUser { PhoneNumber = phoneNumber, UserName = phoneNumber };
                var result = await UserManager.CreateAsync(applicationUser);
                if (result.Succeeded)
                {
                    user = applicationUser;
                }
            }

            var tmpcode = await UserManager.UserTokenProvider.GenerateAsync("EtsForgot", UserManager, user);
            var content = "您的验证码：" + tmpcode + "，请在5分钟内填写。此验证码只用于忘记密码，如非本人操作，请不要理会。";

            SmsHelper etaoshiSMS = new SmsHelper();
            string mess = string.Empty;
            etaoshiSMS.SendSmsSaveLog(phoneNumber, content, "EtsForgot", 0);
            return new JsonResult
            {
                Data = new
                {
                    State = true,
                    Message = "发送验证码成功",
                }
            };  
        }


        /// <summary>
        /// Sends the code.
        /// </summary>      
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="type">The type.(1:登录；2：重置)</param>
        /// <param name="isVoice">if set to <c>true</c> [is voice].</param>
        /// <returns></returns>
        /// 创建者：林燕平
        /// 创建日期：10/30/2015 4:53 PM
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendVerificateCode(string phoneNumber, int type, bool isVoice)
        {
            Regex expressionstr = new Regex(@"^[-]?\d+[.]?\d*$");
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new JsonResult
                {
                    Data = new
                    {
                        State = false,
                        Message = "手机号码错误"
                    }
                };
            }
            if (phoneNumber.Trim().Length != 11 || !expressionstr.IsMatch(phoneNumber))
            {
                return new JsonResult
                {
                    Data = new
                    {
                        State = false,
                        Message = "手机号码错误"
                    }
                };
            }

            var user = await UserManager.FindByNameAsync(phoneNumber);
            if (user == null) //验证码注册
            {
                var applicationUser = new ApplicationUser { PhoneNumber = phoneNumber, UserName = phoneNumber };
                var result = await UserManager.CreateAsync(applicationUser);
                if (result.Succeeded)
                {
                    user = applicationUser;
                }               
            }
               
            var tmpcode = await UserManager.UserTokenProvider.GenerateAsync("Login", UserManager, user);          
            string sendtmpcode = tmpcode;
            if (isVoice)
            {
                string SendCodestr = "";
                for (int i = 0; i < tmpcode.Length; i++)
                {
                    SendCodestr += tmpcode[i] + ",";
                }
                sendtmpcode = SendCodestr.TrimEnd(',');
            }
            string content;
            switch (type)
            {
                case 1:
                    content = "您的验证码：" + sendtmpcode + "，请在5分钟内填写。此验证码只用于登录，如非本人操作，请不要理会。";
                    break;
                case 2:
                    content = "您的验证码：" + sendtmpcode + "，请在5分钟内填写。此验证码只用于重置密码，如非本人操作，请不要理会。";
                    break;
                default:
                    content = "您的验证码：" + sendtmpcode + "，请在5分钟内填写。此验证码只用于登录，如非本人操作，请不要理会。";
                    break;
            }
            SmsHelper etaoshiSMS = new SmsHelper();
            string mess = string.Empty;
            if (isVoice)
            {
                mess = etaoshiSMS.SendVoiceSmsLogSpeed(phoneNumber, content);
            }
            else
            {
                mess = etaoshiSMS.SendSmsSaveLogSpeed(phoneNumber, content, "EtsLogin", 0);
            }

            if (mess != "发送成功")
            {              
                return new JsonResult
                {
                    Data = new
                    {
                        State = false,
                        Message = "发送验证码失败"
                    }
                };
            }

            return new JsonResult
            {
                Data = new
                {
                    State = true,
                    Message = "发送验证码成功",                  
                }
            };         
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}