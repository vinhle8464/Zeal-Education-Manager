using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.ViewModel
{
    public class CourseSubjectViewModel
    {
        public CourseSubject CourseSubject { get; set; }
        public PagedList<CourseSubject> PagedList { get; set; }
    }
}
