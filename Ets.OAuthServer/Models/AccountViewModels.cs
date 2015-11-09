using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ets.OAuthServer
{
    public class ExternalLoginConfirmationViewModel
    {
        
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "验证码")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "记住浏览器选项？")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel
    {
        
        [Display(Name = "邮箱")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {      
        [Display(Name = "验证码")]          
        public string Code { get; set; }

        [Required]
        [Display(Name = "手机号")]
        [Phone]
        public string PhoneNumber { get; set; }
       
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "手机号")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}至少{2}字符长度", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码不匹配")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 长度至少 {2} 个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class CodeForgotPasswordViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "手机号")]
        public string PhoneNumber { get; set; }

        [Phone]
        [Display(Name = "验证码")]
        public string Code { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassWord { get; set; }
    }
}