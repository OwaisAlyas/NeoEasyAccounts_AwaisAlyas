using ClassifiedAds.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Data.Entity.Validation;
using Neo.Auth.ViewModels;
using ClassifiedAds.Model.Admin;
using System.Net;

namespace Neo.EasyAccounts.Web.UI.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private readonly IdentityContext db;
		private IdentityUserManager userManager;
		private IdentityRoleManager roleManager;

		public AdminController()
		{
			db = new IdentityContext();
		}

		public IdentityUserManager UserManager
		{
			get
			{
				return userManager ?? HttpContext.GetOwinContext().Get<IdentityUserManager>();
			}
			private set
			{
				userManager = value;
			}
		}
		public IdentityRoleManager RoleManager
		{
			get
			{
				return roleManager ?? HttpContext.GetOwinContext().Get<IdentityRoleManager>();
			}
			private set
			{
				roleManager = value;
			}
		}

		public async Task<ActionResult> SystemUsers()
		{
			List<UserViewModel> userList = null;
			try
			{
				var list = await db.Users.ToListAsync();

				if (list != null && list.Count > 0)
				{
					userList = new List<UserViewModel>();
					UserViewModel x = null;

					foreach (var item in list)
					{
						x = new UserViewModel(item);
						var roles = await UserManager.GetRolesAsync(item.Id);
						x.Roles = string.Join(",", roles);
						userList.Add(x);
					}
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex);
			}
			//((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.ToList().
			//	ForEach(d => System.Diagnostics.Debug.WriteLine(string.Format("Type:{0} - Value:{1} - ValueType:{2}", d.Type, d.Value, d.ValueType)));
			return View(userList);
		}
		public ActionResult AddUser()
		{
			var model = new UserViewModel();
			model.RolesList = Helpers.WebUtilHelper.GetRolesDictionary();
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddUser([Bind(Include = "ID, Email, Password, ConfirmPassword, FirstName, LastName, DateOfBirth, PhoneNumber, Roles")] UserViewModel model)
		{
			try
			{
				var user = new User(model);

				var userAddResult = await UserManager.CreateAsync(user, model.Password);

				if (userAddResult.Succeeded)
				{
					var roleAddResult = await UserManager.AddToRoleAsync(user.Id, model.Roles);

					if (roleAddResult.Succeeded) return RedirectToAction("SystemUsers");

					roleAddResult.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
				}
				else
				{
					userAddResult.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex);
			}
			return View(model);
		}
		public async Task<ActionResult> UpdateUser(long ID)
		{
			var model = new UserViewModel();
			try
			{
				model = await GetUserAsync(ID);
				var allClaims = await db.Claims.ToListAsync();
				var userClaims = await UserManager.GetClaimsAsync(ID);

				List<ClaimViewModel> claimsList = null;
				ClaimViewModel x = null;

				if (allClaims != null && allClaims.Count > 0)
				{
					claimsList = new List<ClaimViewModel>();

					foreach (var item in allClaims)
					{
						x = new ClaimViewModel(item);
						if (x != null && userClaims != null && userClaims.Count > 0)
						{
							x.Value = userClaims.FirstOrDefault(d => d.Type == item.Name).Value;
						}

						claimsList.Add(x);
					}
					model.Claims = claimsList;
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Translate());
			}
			model.RolesList = Helpers.WebUtilHelper.GetRolesDictionary();
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> UpdateUser([Bind(Include = "ID, Email, FirstName, LastName, DateOfBirth, PhoneNumber, Roles")] UserViewModel model)
		{
			try
			{
				var x = await UserManager.FindByIdAsync(model.ID);

				if (x == null) throw new ArgumentNullException("Unable to find this user");

				x.DateOfBirth = model.DateOfBirth;
				x.Email = model.Email;
				x.UserName = x.Email;
				x.FirstName = model.FirstName;
				x.LastName = model.LastName;
				x.PhoneNumber = model.PhoneNumber;

				var userUpdateResult = await UserManager.UpdateAsync(x);

				if (userUpdateResult.Succeeded)
				{
					var roles = await UserManager.GetRolesAsync(model.ID);

					if (roles != null && roles.Count > 0)
					{
						var rolesRemovalResult = await UserManager.RemoveFromRolesAsync(model.ID, roles.ToArray());

						if (!rolesRemovalResult.Succeeded) rolesRemovalResult.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
					}

					var roleAddResult = await UserManager.AddToRoleAsync(x.Id, model.Roles);

					if (roleAddResult.Succeeded) return RedirectToAction("Users");

					roleAddResult.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
				}
				else
				{
					userUpdateResult.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex);
			}
			return View(model);
		}

		public ActionResult SystemRoles()
		{
			var rolesList = new List<RoleViewModel>();
			try
			{
				foreach (var role in db.Roles.Include("Users"))
				{
					var roleModel = new RoleViewModel(role);
					rolesList.Add(roleModel);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex);
			}
			return View(rolesList);
		}

		public async Task<ActionResult> RoleRegEdit(long? id)
		{
			RoleViewModel model = null;
			try
			{
				if (id != null && id.HasValue)
				{
					var role = await RoleManager.FindByIdAsync(id.Value);
					model = new RoleViewModel(role);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex);
			}
			return View(model ?? new RoleViewModel());
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RoleRegEdit([Bind(Include = "ID,RoleName,Description")] RoleViewModel model)
		{
			try
			{
				IdentityResult result = model.ID > 0 ? db.UpdateRole(RoleManager, model) : db.CreateRole(RoleManager, model.RoleName, model.Description);

				if (result.Succeeded) return RedirectToAction("SystemRoles");

				result.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
			}
			catch (DbEntityValidationException ex)
			{
				foreach (var item in ex.EntityValidationErrors)
				{
					item.ValidationErrors.ToList().ForEach(error => ModelState.AddModelError("", string.Format("{0}:{1}", error.PropertyName, error.ErrorMessage)));
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Translate());
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> RoleDelete([Bind(Include = "ID")]RoleViewModel model)
		{
			try
			{
				var role = await RoleManager.FindByIdAsync(model.ID);
				var result = await RoleManager.DeleteAsync(role);
				if (result.Succeeded) return RedirectToAction("SystemRoles");

				result.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex);
			}
			return View(model);
		}

		public async Task<ActionResult> SystemClaims()
		{
			return View(await db.Claims.ToListAsync());
		}
		public async Task<ActionResult> ClaimsRegEdit(long? Id)
		{
			ClaimViewModel viewModel = null;

			if (Id != null && Id.HasValue)
			{
				var entity = await db.Claims.FindAsync(Id.Value);
				viewModel = new ClaimViewModel(entity);
			}

			return View(viewModel ?? new ClaimViewModel());
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ClaimsRegEdit([Bind(Include = "Id,Name,Description")] ClaimViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var entity = new ApplicationClaim(viewModel);

				db.Entry(entity).State = viewModel.Id > 0 ? EntityState.Modified : EntityState.Added;

				await db.SaveChangesAsync();
				return RedirectToAction("SystemClaims");
			}
			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteClaim(int id)
		{
			if (id < 1) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var entity = await db.Claims.FindAsync(id);

			if (entity == null) return HttpNotFound();

			db.Claims.Remove(entity);
			await db.SaveChangesAsync();
			return RedirectToAction("SystemClaims");
		}

		private async Task<UserViewModel> GetUserAsync(long ID)
		{
			var x = await UserManager.FindByIdAsync(ID);
			var model = new UserViewModel(x);
			if (x != null)
			{
				var roles = await UserManager.GetRolesAsync(x.Id);
				model.Roles = string.Join(",", roles);
			}
			return model;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && UserManager != null)
			{
				UserManager.Dispose();
				UserManager = null;
				RoleManager.Dispose();
				RoleManager = null;
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}