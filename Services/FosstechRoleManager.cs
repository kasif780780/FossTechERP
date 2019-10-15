using FossERp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossERp.Services
{
    public class FosstechRoleManager : RoleManager<IdentityRole>
    {
        public FosstechRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {

        }

        public static FosstechRoleManager Create(IdentityFactoryOptions<FosstechRoleManager> options, IOwinContext context)
        {
            return new FosstechRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }
}