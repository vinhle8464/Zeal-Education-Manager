using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.ViewModel
{
    public class PayViewModel
    {
        public PagedList<Pay> PagedList { get; set; }
        public Pay Pay { get; set; }
    }
}
