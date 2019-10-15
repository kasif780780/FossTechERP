using FossERp.Models;
using FossERp.Services;
using FossERp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FossERp.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private FosstechRoleManager _roleManager;
        public RolesController()
        {

        }
        public RolesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, FosstechRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = _roleManager;
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
        // GET: Roles
        public ActionResult Index(string searchTerm, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;
            RolesListingModels model = new RolesListingModels();
            model.SearchTerm = searchTerm;

            //model.Roles = accomodationService.GetAllAccomodation();

            model.Roles = SearchRoles(searchTerm, page.Value, recordSize);





            var totalRecords = SearchRolesCount(searchTerm);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);


            // return View(myuser.ToPagedList(pageNumber, pageSize));
        }

        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int page, int recordSize)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var skip = (page - 1) * recordSize;
            return roles.OrderBy(x => x.Name).Skip(skip).Take(recordSize).ToList();

        }

        public int SearchRolesCount(string searchTerm)
        {
            var roles = RoleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }




            return roles.Count();
        }


        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            RoleActionModel model = new RoleActionModel();
            if (!string.IsNullOrEmpty(ID))
            {
                var role = await RoleManager.FindByIdAsync(ID);
                model.ID = role.Id;

                model.Name = role.Name;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RoleActionModel model)
        {


            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var role = await RoleManager.FindByIdAsync(model.ID);

                role.Name = model.Name;


                result = await RoleManager.UpdateAsync(role);

            }

            else
            {
                var role = new IdentityRole();

                role.Name = model.Name;


                result = await RoleManager.CreateAsync(role);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            RoleActionModel model = new RoleActionModel();
            var role = await RoleManager.FindByIdAsync(ID);
            model.ID = role.Id;
            return PartialView("_Delete", model);

        }

        [HttpPost]
        public async Task<JsonResult> Delete(RoleActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var role = await RoleManager.FindByIdAsync(model.ID);




                result = await RoleManager.DeleteAsync(role);
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {

                json.Data = new { Success = false, Message = "Invalid Role" };
            }
            return json;
        }


    }
}