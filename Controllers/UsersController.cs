using System;
using FossERp.Models;
using FossERp.Services;
using FossERp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FossERp.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private FosstechRoleManager _roleManager;
        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, FosstechRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        public FosstechRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<FosstechRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: Users
        public ActionResult Index(string searchTerm, int? page, string roleID)
        {
            int recordSize = 5;
            page = page ?? 1;
            UserListingModels model = new UserListingModels();
            model.SearchTerm = searchTerm;
            model.RoleID = roleID;
            model.Roles = RoleManager.Roles.ToList();

            model.Users = SearchUsers(searchTerm, roleID, page.Value, recordSize);





            var totalRecords = SearchUsersCount(searchTerm, roleID);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);


            // return View(myuser.ToPagedList(pageNumber, pageSize));
        }

        public IEnumerable<ApplicationUser> SearchUsers(string searchTerm, string roleID, int page, int recordSize)
        {
            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                users = users.Where(x => x.Roles.Select(y => y.RoleId).Contains(roleID));
            }

            var skip = (page - 1) * recordSize;
            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }
        public int SearchUsersCount(string searchTerm, string roleID)
        {
            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                //users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            //if (accPacid.HasValue && accpacid.value>0)

            //{
            //    acc = acc.where(a => a.AccomdID == accpacid.Value);
            //}

            return users.Count();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            UserActionModel model = new UserActionModel();
            if (!string.IsNullOrEmpty(ID))
            {
                var user = await UserManager.FindByIdAsync(ID);

                model.ID = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;
            }
            // model.Role = accomodationPS.GetAllaCOMOdationPS();
            return PartialView("_Action", model);
        }


        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model)
        {


            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.UpdateAsync(user);

            }

            else
            {
                var user = new ApplicationUser();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            UserActionModel model = new UserActionModel();
            var user = await UserManager.FindByIdAsync(ID);
            model.ID = user.Id;
            return PartialView("_Delete", model);

        }

        [HttpPost]
        public async Task<JsonResult> Delete(UserActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.DeleteAsync(user);
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid user" };
            }
            return json;
        }


        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {


            UserRolesModel model = new UserRolesModel();
            model.Roles = RoleManager.Roles.ToList();

            model.UserID = ID;


            var user = await UserManager.FindByIdAsync(ID);
            var userRolesIDs = user.Roles.Select(x => x.RoleId).ToList();
            model.UserRoles = RoleManager.Roles.Where(x => userRolesIDs.Contains(x.Id)).ToList();
            model.Roles = RoleManager.Roles.Where(x => !userRolesIDs.Contains(x.Id)).ToList();
            return PartialView("_UserRoles", model);
        }


        [HttpPost]
        public async Task<JsonResult> UserRoles(UserActionModel model)
        {


            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.UpdateAsync(user);

            }

            else
            {
                var user = new ApplicationUser();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            return json;
        }

        //[HttpGet]
        //public async Task<ActionResult> UserRoles(string ID)
        //{


        //    UserRolesModel model = new UserRolesModel();
        //    model.UserID = ID;
        //    var user = await UserManager.FindByIdAsync(ID);
        //    model.Roles = RoleManager.Roles.ToList();
        //    var userRolesIDs = user.Roles.Select(x => x.RoleId).ToList();
        //    model.UserRoles = RoleManager.Roles.Where(x => userRolesIDs.Contains(x.Id)).ToList();
        //    return PartialView("_UserRoles", model);
        //}


        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string userID, string roleID, bool isDelete = false)
        {


            JsonResult json = new JsonResult();
            var user = await UserManager.FindByIdAsync(userID);
            var role = await RoleManager.FindByIdAsync(roleID);
            if (user != null && role != null)
            {
                IdentityResult result = null;
                if (!isDelete)
                {
                    result = await UserManager.AddToRoleAsync(userID, role.Name);
                }
                else
                {
                    result = await UserManager.RemoveFromRoleAsync(userID, role.Name);
                }

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Operation." };
            }
            return json;
        }
    }
}