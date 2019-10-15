using FossERp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossERp.ViewModels
{
    public class SalesOrderViewodels
    {
        public IList<Lead> _lead { get; set; }
        public IList<SalesOrder> _msalesOrder { get;set; }

    }
}