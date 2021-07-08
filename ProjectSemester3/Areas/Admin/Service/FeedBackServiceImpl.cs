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
    public class FeedBackServiceImpl : IFeedBackService
    {
        private readonly DatabaseContext context;
        public FeedBackServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<Feedback> Create(Feedback Feedback)
        {

            context.Feedbacks.Add(Feedback);
            await context.SaveChangesAsync();
            return Feedback;

        }
        public async Task CreateFeedbackFaculty(FeedbackFaculty feedbackFaculty)
        {
            context.FeedbackFaculties.Add(feedbackFaculty);
            await context.SaveChangesAsync();
        }


        public async Task Delete(int FeedbackId)
        {
            context.Feedbacks.Remove(context.Feedbacks.Find(FeedbackId));
            await context.SaveChangesAsync();
        }

        public async Task<Feedback> Find(int FeedbackId) => await context.Feedbacks.FirstOrDefaultAsync(p => p.FeedbackId == FeedbackId);

        public async Task<List<Feedback>> FindAll() => await context.Feedbacks.OrderByDescending(f => f.FeedbackId).Take(10).ToListAsync();

        public async Task<List<string>> GetListFaculty(string keyword)
        {
            return await context.Accounts.Where(a => a.RoleId == "role02" && a.Active == true && a.Status == true).Select(a => a.Fullname).ToListAsync();
        }

        public List<Subject> GetListSubjectAsync(string facultyName)
        {
            var facultyId = context.Accounts.FirstOrDefault(a => a.Fullname == facultyName);
            return context.Professionals.Where(p => p.FacultyId == facultyId.AccountId).Select(p => p.Subject).ToList();
        }

        public async Task<Feedback> Update(Feedback Feedback)
        {
            context.Entry(Feedback).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return Feedback;
        }

        public List<Feedback> Search(string searchFeedback)
        {
            var feedbacks = context.Feedbacks.AsQueryable();

            if (searchFeedback != null) feedbacks = feedbacks.Where(b => b.Subject.SubjectName.Contains(searchFeedback));

            var result = feedbacks.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }

    }
}
