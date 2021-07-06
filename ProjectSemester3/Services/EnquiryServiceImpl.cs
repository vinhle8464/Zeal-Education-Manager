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

        public Enquiry Find(int id)
        {
            return context.Enquiries.SingleOrDefault(e => e.Id == id);
        }

        public List<Enquiry> FindAll()
        {
            return context.Enquiries.Where(e => e.Status == true).ToList();
        }
    }
}
