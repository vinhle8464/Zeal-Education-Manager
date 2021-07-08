using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.ViewModel
{
    public class FeedbackFacultyViewModel
    {
        public PagedList<FeedbackFaculty> PagedList { get; set; }
        public FeedbackFaculty FeedbackFaculties { get; set; }
    }
}
