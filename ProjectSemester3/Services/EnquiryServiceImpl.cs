using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class EnquiryServiceImpl : IEnquiryService
    {
        private DatabaseContext context;
        public EnquiryServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public Enquiry Create(Enquiry enquiry)
        {
            context.Enquiries.Add(enquiry);
            context.SaveChanges();
            return enquiry;
        }

        public async Task Delete(int id)
        {
            var enquiry = context.Enquiries.Find(id);
            enquiry.Status = false;
            context.Entry(enquiry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public Enquiry Find(int id)
        {
            return context.Enquiries.SingleOrDefault(e => e.Id == id);
        }

        public async Task<Enquiry> FindAjax(int id)
        {
            return await context.Enquiries.FirstOrDefaultAsync(e => e.Id == id && e.Status == true);
               
        }

        public List<Enquiry> FindAll()
        {
            return context.Enquiries.Where(e => e.Status == true).ToList();
        }

        public List<Enquiry> Search(string searchEnquiry)
        {
            var enquiries = context.Enquiries.AsQueryable();

            if (searchEnquiry != null) enquiries = enquiries.Where(b => b.Title.Contains(searchEnquiry));

            var result = enquiries.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }

        public async Task<dynamic> Update(Enquiry enquiry)
        {
            context.Entry(enquiry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           return await context.SaveChangesAsync();
        }
    }
}
