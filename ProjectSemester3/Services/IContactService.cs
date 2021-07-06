using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IContactService
    {
        public Mail Create(Mail mail);
        
    }
}
