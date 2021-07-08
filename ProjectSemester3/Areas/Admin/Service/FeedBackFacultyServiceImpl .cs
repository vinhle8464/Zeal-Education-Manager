using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class FeedbackFacultyServiceImpl : IFeedbackFacultyService
    {
        private readonly DatabaseContext context;
        public FeedbackFacultyServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<dynamic> Create(FeedbackFaculty FeedbackFaculty)
        {

            if (context.FeedbackFaculties.Any(p => p.FeedbackId.Equals(FeedbackFaculty.FeedbackId) && p.FacultyId == FeedbackFaculty.FacultyId && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.FeedbackFaculties.Add(FeedbackFaculty);
                return await context.SaveChangesAsync();
            }

        }

        public async Task Delete(int feedbackId, string facultyId)
        {
            context.FeedbackFaculties.Remove(context.FeedbackFaculties.FirstOrDefault(f => f.FeedbackId == feedbackId && f.FacultyId == facultyId));
            await context.SaveChangesAsync();
        }

        public async Task<FeedbackFaculty> Find(int feedbackId, string facultyId) => await context.FeedbackFaculties.FirstOrDefaultAsync(p => p.FeedbackId == feedbackId && p.FacultyId == facultyId);

        public async Task<List<FeedbackFaculty>> FindAll() => await context.FeedbackFaculties.OrderByDescending(f => f.FeedbackId).Take(10).ToListAsync();


        public async Task<FeedbackFaculty> Update(FeedbackFaculty FeedbackFaculty)
        {
            context.Entry(FeedbackFaculty).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return FeedbackFaculty;
        }

        public List<FeedbackFaculty> Search(string searchFeedbackFaculty)
        {
            var feedbackFaculty = context.FeedbackFaculties.AsQueryable();

            if (searchFeedbackFaculty != null) feedbackFaculty = feedbackFaculty.Where(b => b.Faculty.Fullname.Contains(searchFeedbackFaculty));

            var result = feedbackFaculty.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }
    }
}
