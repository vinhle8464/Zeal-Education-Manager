using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IMailService
    {
        public string SendCodeForgotPass(string mail);
        public string Reply(string mail, string title, string content);
        Mail InsertIntoDb(Mail mail);
        Task<List<Mail>> FindAll();
        Mail Find(int mailid);
        Task Delete(int MailId);
        Task<Mail> Update(Mail mail);
        bool Exists(int MailId);
        public List<Mail> Search(string searchMail);

    }
}
