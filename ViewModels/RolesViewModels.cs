using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossERp.ViewModels
{
    public class RolesListingModels
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }

    public class RoleActionModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}