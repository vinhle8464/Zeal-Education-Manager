using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Areas.Faculty.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Controllers
{
    [Area("faculty")]
    [Route("examination")]
    [Route("faculty/examination")]
    public class ExaminationController : Controller
    {
        private readonly IExaminationService examinationService;
        public ExaminationController(IExaminationService _examinationService)
        {
            examinationService = _examinationService;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index(string facultyid)
        {
            ViewBag.faculty = examinationService.getfacultyid(facultyid);
            ViewBag.subjects = examinationService.subjects();
            return View();
        }
        [Route("exam")]
        public IActionResult Exam(string facultyid,string subjectid)
        {
            ViewBag.faculty = examinationService.getfacultyid(facultyid);
            ViewBag.exams = examinationService.exams(subjectid);
            return View("exam");
        }
        [Route("examanalysis")]
        public IActionResult Examanalysis(string facultyid,string examid)
        {
            ViewBag.faculty = ViewBag.account = examinationService.getfacultyid(facultyid);
            ViewBag.examdetail = examinationService.examdetail(examid);
            ViewBag.highestscore = examinationService.examdetail(examid).Marks.Max(m => m.Mark1).ToString();
            ViewBag.lowestscore = examinationService.examdetail(examid).Marks.Min(m => m.Mark1).ToString();
            int i = 0;
            int numbermark = 0;
            foreach (var mark in examinationService.examdetail(examid).Marks)
            {
                numbermark++;
                if (((decimal)mark.Mark1 / (decimal)mark.MaxMark) >= (decimal)0.4)
                {
                    i++;
                }

            }
            ViewBag.passpercent =Convert.ToInt32( ((decimal)(i / numbermark)*100));
            ViewBag.pass = i;
            ViewBag.failpercent = Convert.ToInt32(100 - ViewBag.passpercent);
            ViewBag.pass = numbermark - i;
            ViewBag.maxscore =Convert.ToInt32( examinationService.examdetail(examid).Marks.Max(m => m.MaxMark));
            ViewBag.mediumscore = Convert.ToInt32(examinationService.examdetail(examid).Marks.Sum(m => m.Mark1)/(decimal)examinationService.examdetail(examid).Marks.Count());
            return View("examanalysis");
        }
    }
}
