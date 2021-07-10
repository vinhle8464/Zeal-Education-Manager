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
            try
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
                    if (((decimal)mark.Mark1 / (decimal)mark.MaxMark) * 100 >= (decimal)mark.Rate)
                    {
                        i++;
                    }

                }
                ViewBag.passpercent = ((decimal)((i * 100) / numbermark));
                ViewBag.pass = i;
                ViewBag.failpercent = (decimal)100 - ViewBag.passpercent;
                ViewBag.fail = numbermark - i;
                ViewBag.maxscore = Convert.ToInt32(examinationService.examdetail(examid).Marks.Max(m => m.MaxMark));
                ViewBag.mediumscore = Convert.ToInt32(examinationService.examdetail(examid).Marks.Sum(m => m.Mark1) / (decimal)examinationService.examdetail(examid).Marks.Count());
                return View("examanalysis");
            }
            catch
            {
                ViewBag.faculty = ViewBag.account = examinationService.getfacultyid(facultyid);
                ViewBag.examdetail = examinationService.examdetail(examid);
                ViewBag.highestscore =0;
                ViewBag.lowestscore =0;
                
                ViewBag.passpercent =0;
                ViewBag.pass = 0;
                ViewBag.failpercent =0;
                ViewBag.fail =0;
                ViewBag.maxscore = 0;
                ViewBag.mediumscore = 0;
                return View("examanalysis");
            }
           
        }
    }
}
