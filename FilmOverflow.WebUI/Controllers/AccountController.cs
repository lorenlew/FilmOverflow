﻿using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace FilmOverflow.WebUI.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IUserManagerService _userManagerService;

		public AccountController(IUserManagerService userManagerService)
		{
			_userManagerService = userManagerService;

		}

		public ActionResult Summary()
		{
			var currentUser = _userManagerService.FindByName(User.Identity.Name);
			var applicationUserViewModel = Mapper.Map<ApplicationUserDomainModel, ApplicationUserViewModel>(currentUser);
			return View(applicationUserViewModel);
		}

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
		
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (model == null) throw new ArgumentNullException("model");
			if (ModelState.IsValid)
			{
				var user = await _userManagerService.FindAsync(model.Email, model.Password);
				if (user != null && user.EmailConfirmed && !user.IsBanned)
				{
					await SignInAsync(user, model.RememberMe);
					return RedirectToLocal(returnUrl);
				}
				if (user == null)
				{
					ModelState.AddModelError("", "Invalid username or password.");
				}
				else
				{
					if (!user.EmailConfirmed)
					{
						ModelState.AddModelError("", "Account is not confirmed via email.");
					}

					if (user.IsBanned)
					{
						ModelState.AddModelError("", "Account is banned.");
					}
				}


			}
			return View(model);
		}

		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register([Bind(Include = "Email,FirstName,LastName,PhoneNumber,Password,ConfirmPassword")]RegisterViewModel model)
		{
			if (model == null) throw new ArgumentNullException("model");
			if (ModelState.IsValid)
			{
				var user = new ApplicationUserDomainModel()
				{
					UserName = model.Email,
					Email = model.Email,
					FirstName = model.FirstName,
					LastName = model.LastName,
					PhoneNumber = model.PhoneNumber,
				};
				IdentityResult result = await _userManagerService.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					string code = await _userManagerService.GenerateEmailConfirmationTokenAsync(user.Id);
					var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
					await _userManagerService.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

					return View("CheckEmail");
				}
				AddErrors(result);
			}
			return View(model);
		}

		[AllowAnonymous]
		public async Task<ActionResult> ConfirmEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return View("Error");
			}

			IdentityResult result = await _userManagerService.ConfirmEmailAsync(userId, code);
			if (result.Succeeded)
			{
				return View("ConfirmEmail");
			}
			AddErrors(result);
			return View();
		}

		[AllowAnonymous]
		public ActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			if (model == null) throw new ArgumentNullException("model");
			if (ModelState.IsValid)
			{
				var user = await _userManagerService.FindByNameAsync(model.Email);
				if (user == null || !(await _userManagerService.IsEmailConfirmedAsync(user.Id)))
				{
					ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
					return View();
				}
				string code = await _userManagerService.GeneratePasswordResetTokenAsync(user.Id);
				var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
				await _userManagerService.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
				return RedirectToAction("ForgotPasswordConfirmation", "Account");
			}
			return View(model);
		}

		[AllowAnonymous]
		public ActionResult ForgotPasswordConfirmation()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult ResetPassword(string code)
		{
			if (code == null)
			{
				return View("Error");
			}
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (model == null) throw new ArgumentNullException("model");
			if (ModelState.IsValid)
			{
				var user = await _userManagerService.FindByNameAsync(model.Email);
				if (user == null)
				{
					ModelState.AddModelError("", "No user found.");
					return View();
				}
				IdentityResult result = await _userManagerService.ResetPasswordAsync(user.Id, model.Code, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("ResetPasswordConfirmation", "Account");
				}
				AddErrors(result);
				return View();
			}
			return View(model);
		}

		[AllowAnonymous]
		public ActionResult ResetPasswordConfirmation()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
		{
			ManageMessageId? message = null;
			IdentityResult result = await _userManagerService.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
			if (result.Succeeded)
			{
				var user = await _userManagerService.FindByIdAsync(User.Identity.GetUserId());
				await SignInAsync(user, isPersistent: false);
				message = ManageMessageId.RemoveLoginSuccess;
			}
			else
			{
				message = ManageMessageId.Error;
			}
			return RedirectToAction("Manage", new { Message = message });
		}

		public ActionResult Manage(ManageMessageId? message)
		{
			ViewBag.StatusMessage =
				message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
				: message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
				: message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
				: message == ManageMessageId.Error ? "An error has occurred."
				: "";
			ViewBag.HasLocalPassword = HasPassword();
			ViewBag.ReturnUrl = Url.Action("Manage");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Manage(ManageUserViewModel model)
		{
			if (model == null) throw new ArgumentNullException("model");
			bool hasPassword = HasPassword();
			ViewBag.HasLocalPassword = hasPassword;
			ViewBag.ReturnUrl = Url.Action("Manage");
			if (hasPassword)
			{
				if (ModelState.IsValid)
				{
					IdentityResult result = await _userManagerService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
					if (result.Succeeded)
					{
						var user = await _userManagerService.FindByIdAsync(User.Identity.GetUserId());
						await SignInAsync(user, isPersistent: false);
						return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
					}
					AddErrors(result);
				}
			}
			else
			{
				// User does not have a password so remove any validation errors caused by a missing OldPassword field
				ModelState state = ModelState["OldPassword"];
				if (state != null)
				{
					state.Errors.Clear();
				}

				if (ModelState.IsValid)
				{
					IdentityResult result = await _userManagerService.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
					if (result.Succeeded)
					{
						return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
					}
					AddErrors(result);
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult ExternalLogin(string provider, string returnUrl)
		{
			// Request a redirect to the external login provider
			return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
		}

		[AllowAnonymous]
		public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
		{
			var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
			if (loginInfo == null)
			{
				return RedirectToAction("Login");
			}

			// Sign in the user with this external login provider if the user already has a login
			var user = await _userManagerService.FindAsync(loginInfo.Login);
			if (user != null)
			{
				await SignInAsync(user, isPersistent: false);
				return RedirectToLocal(returnUrl);
			}
			else
			{
				// If the user does not have an account, then prompt the user to create an account
				ViewBag.ReturnUrl = returnUrl;
				ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
				return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LinkLogin(string provider)
		{
			// Request a redirect to the external login provider to link a login for the current user
			return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
		}

		public async Task<ActionResult> LinkLoginCallback()
		{
			var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
			if (loginInfo == null)
			{
				return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
			}
			IdentityResult result = await _userManagerService.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
			if (result.Succeeded)
			{
				return RedirectToAction("Manage");
			}
			return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Manage");
			}

			if (ModelState.IsValid)
			{
				// Get the information about the user from the external login provider
				var info = await AuthenticationManager.GetExternalLoginInfoAsync();
				if (info == null)
				{
					return View("ExternalLoginFailure");
				}
				var user = new ApplicationUserDomainModel()
				{
					UserName = model.Email
				};
				IdentityResult result = await _userManagerService.CreateAsync(user);
				if (result.Succeeded)
				{
					result = await _userManagerService.AddLoginAsync(user.Id, info.Login);
					if (result.Succeeded)
					{
						await SignInAsync(user, isPersistent: false);

						// For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
						// Send an email with this link
						string code = await _userManagerService.GenerateEmailConfirmationTokenAsync(user.Id);
						var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
						SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");

						return RedirectToLocal(returnUrl);
					}
				}
				AddErrors(result);
			}

			ViewBag.ReturnUrl = returnUrl;
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut();
			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public ActionResult ExternalLoginFailure()
		{
			return View();
		}

		[ChildActionOnly]
		public ActionResult RemoveAccountList()
		{
			var linkedAccounts = _userManagerService.GetLogins(User.Identity.GetUserId());
			ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
			return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
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

		private async Task SignInAsync(ApplicationUserDomainModel user, bool isPersistent)
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
			AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await _userManagerService.GenerateUserIdentityAsync(user));
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}

		private bool HasPassword()
		{
			var user = _userManagerService.FindById(User.Identity.GetUserId());
			if (user != null)
			{
				return user.PasswordHash != null;
			}
			return false;
		}

		private void SendEmail(string email, string callbackUrl, string subject, string message)
		{
			// For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
		}

		public enum ManageMessageId
		{
			ChangePasswordSuccess,
			SetPasswordSuccess,
			RemoveLoginSuccess,
			Error
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		private class ChallengeResult : HttpUnauthorizedResult
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
				if (context == null) throw new ArgumentNullException("context");
				var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
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