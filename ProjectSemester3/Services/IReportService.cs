﻿using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IReportService
    {
        List<Batch> GetCourse(string studentid);
        List<ScholarshipStudent> GetScholarships(string studentid);
        public List<Attendance> GetDiligence(string studentid);

        public List<CourseSubject> GetSubject(string classid);
        public List<Attendance> GetAttendances(string subjectid, string studentid);
        public List<Attendance> GetAttendances(string studentid);
        public int GetDateOff(string subjectid, string studentid);
        public Task<string> GetStatusSubject(string classid, string studentid, string subjectid);
        public Task<List<Subject>> GetSubject();

        public Task<Batch> GetDateAttendance(string classid);


    }
}