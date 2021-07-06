using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IEnquiryService
    {
        List<Enquiry> FindAll();
        Enquiry Create(Enquiry enquiry);
        Enquiry Find(int id);
    }
}
