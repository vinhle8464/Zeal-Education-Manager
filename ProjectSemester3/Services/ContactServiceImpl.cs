using MimeKit;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class ContactServiceImpl: IContactService
    {
        private readonly DatabaseContext context;
        public ContactServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public Mail Create(Mail mail)
        {
            context.Mail.Add(mail);
            context.SaveChanges();
            return mail;
        }

        
    }
}
