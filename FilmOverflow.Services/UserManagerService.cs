using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FilmOverflow.Services
{
	public class UserManagerService : BaseService, IUserManagerService
	{
		public UserManagerService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			Uow.UserManager = InitUserManager(Uow.UserManager);
		}

		private UserManager<ApplicationUser> InitUserManager(UserManager<ApplicationUser> manager)
		{
			manager.UserValidator = new UserValidator<ApplicationUser>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};
			manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
			{
				MessageFormat = "Your security code is: {0}"
			});
			manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
			{
				Subject = "Security Code",
				BodyFormat = "Your security code is: {0}"
			});
			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();
			var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("FilmOverflow");
			manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
			return manager;
		}

		public ApplicationUserDomainModel FindByName(string name)
		{
			ApplicationUser user = Uow.UserManager.FindByName(name);
			ApplicationUserDomainModel applicationUserDomainModel = Mapper.Map<ApplicationUser,
				ApplicationUserDomainModel>(user);
			return applicationUserDomainModel;
		}

		public async Task<ApplicationUserDomainModel> FindAsync(string email, string password)
		{
			ApplicationUser user = await Uow.UserManager.FindAsync(email, password);
			ApplicationUserDomainModel applicationUserDomainModel = Mapper.Map<ApplicationUser,
				ApplicationUserDomainModel>(user);
			return applicationUserDomainModel;
		}

		public async Task<String> GenerateEmailConfirmationTokenAsync(string id)
		{
			string code = await Uow.UserManager.GenerateEmailConfirmationTokenAsync(id);
			return code;
		}

		public async Task<IdentityResult> CreateAsync(ApplicationUserDomainModel userDomainModel, string password)
		{
			ApplicationUser user = Mapper.Map<ApplicationUserDomainModel,
				ApplicationUser>(userDomainModel);
			IdentityResult result = await Uow.UserManager.CreateAsync(user, password);
			return result;
		}

		public async Task SendEmailAsync(string userId, string message, string body)
		{
			await Uow.UserManager.SendEmailAsync(userId, message, body);
		}

		public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
		{
			IdentityResult result = await Uow.UserManager.ConfirmEmailAsync(userId, code);
			return result;
		}

		public async Task<ApplicationUserDomainModel> FindByNameAsync(string email)
		{
			ApplicationUser user = await Uow.UserManager.FindByNameAsync(email);
			ApplicationUserDomainModel applicationUserDomainModel = Mapper.Map<ApplicationUser,
				ApplicationUserDomainModel>(user);
			return applicationUserDomainModel;
		}

		public async Task<bool> IsEmailConfirmedAsync(string userId)
		{
			bool isEmailConfirmed = await Uow.UserManager.IsEmailConfirmedAsync(userId);
			return isEmailConfirmed;
		}

		public async Task<string> GeneratePasswordResetTokenAsync(string userId)
		{
			string code = await Uow.UserManager.GeneratePasswordResetTokenAsync(userId);
			return code;
		}

		public async Task<IdentityResult> ResetPasswordAsync(string userId, string code, string password)
		{
			IdentityResult result = await Uow.UserManager.ResetPasswordAsync(userId, code, password);
			return result;
		}

		public async Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo userLoginInfo)
		{
			IdentityResult result = await Uow.UserManager.RemoveLoginAsync(userId, userLoginInfo);
			return result;
		}

		public async Task<ApplicationUserDomainModel> FindByIdAsync(string userId)
		{
			ApplicationUser user = await Uow.UserManager.FindByIdAsync(userId);
			ApplicationUserDomainModel applicationUserDomainModel = Mapper.Map<ApplicationUser,
				ApplicationUserDomainModel>(user);
			return applicationUserDomainModel;
		}

		public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
		{
			IdentityResult result = await Uow.UserManager.ChangePasswordAsync(userId, oldPassword, newPassword);
			return result;
		}

		public async Task<IdentityResult> AddPasswordAsync(string userId, string newPassword)
		{
			IdentityResult result = await Uow.UserManager.AddPasswordAsync(userId, newPassword);
			return result;
		}

		public async Task<ApplicationUserDomainModel> FindAsync(UserLoginInfo userLoginInfo)
		{
			ApplicationUser user = await Uow.UserManager.FindAsync(userLoginInfo);
			ApplicationUserDomainModel applicationUserDomainModel = Mapper.Map<ApplicationUser,
				ApplicationUserDomainModel>(user);
			return applicationUserDomainModel;
		}

		public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo userLoginInfo)
		{
			IdentityResult result = await Uow.UserManager.AddLoginAsync(userId, userLoginInfo);
			return result;
		}

		public async Task<IdentityResult> CreateAsync(ApplicationUserDomainModel userDomainModel)
		{
			ApplicationUser user = Mapper.Map<ApplicationUserDomainModel, ApplicationUser>(userDomainModel);
			IdentityResult result = await Uow.UserManager.CreateAsync(user);
			return result;
		}

		public IList<UserLoginInfo> GetLogins(string userId)
		{
			IList<UserLoginInfo> linkedAccounts = Uow.UserManager.GetLogins(userId);
			return linkedAccounts;
		}

		public ApplicationUserDomainModel FindById(string userId)
		{
			ApplicationUser user = Uow.UserManager.FindById(userId);
			ApplicationUserDomainModel applicationUserDomainModel = Mapper.Map<ApplicationUser,
				ApplicationUserDomainModel>(user);
			return applicationUserDomainModel;
		}

		public IEnumerable<ApplicationUserDomainModel> GetUsers()
		{
			IQueryable<ApplicationUser> users = Uow.UserManager.Users;
			IEnumerable<ApplicationUserDomainModel> usersDomainModel = Mapper.Map<IEnumerable<ApplicationUser>,
				IEnumerable<ApplicationUserDomainModel>>(users);
			return usersDomainModel;
		}

		public bool IsInRole(string userId, string role)
		{
			bool isInRole = Uow.UserManager.IsInRole(userId, role);
			return isInRole;
		}

		public void RemoveFromRole(string userId, string role)
		{
			Uow.UserManager.RemoveFromRole(userId, role);
		}

		public void AddToRole(string userId, string role)
		{
			Uow.UserManager.AddToRole(userId, role);
		}

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserDomainModel userDomainModel)
		{
			ApplicationUser user = Mapper.Map<ApplicationUserDomainModel, ApplicationUser>(userDomainModel);
			ClaimsIdentity claimsIdentity = await user.GenerateUserIdentityAsync(Uow.UserManager);
			return claimsIdentity;
		}

		public IList<string> GetRoles(string userId)
		{
			IList<string> roles = Uow.UserManager.GetRoles(userId);
			return roles;
		}

		public void BanUser(string userId)
		{
			ApplicationUser targetUser = Uow.UserManager.FindById(userId);
			if (targetUser == null)
			{
				return;
			}
			targetUser.IsBanned = true;
			Uow.UserManager.Update(targetUser);
		}

		public void UnbanUser(string userId)
		{
			ApplicationUser targetUser = Uow.UserManager.FindById(userId);
			if (targetUser == null)
			{
				return;
			}
			targetUser.IsBanned = false;
			Uow.UserManager.Update(targetUser);
		}
	}
}
