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

        Task Delete(int id);
        Task<dynamic> Update(Enquiry enquiry);
        List<Enquiry> Search(string searchEnquiry);
        Task<Enquiry> FindAjax(int id);
    }
}
