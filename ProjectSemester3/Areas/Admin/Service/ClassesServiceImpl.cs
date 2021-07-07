using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class ClassesServiceImpl : IClassesService
    {

        private readonly DatabaseContext context;
        public ClassesServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }
        public async Task<int> CountId()
        {
            return await context.Classes.CountAsync();
        }

        //public async Task<int> CountIdById(string ClassId)
        //{
        //    return await context.Classes.Where(p => p.ClassId.Contains(ClassId)).CountAsync();
        //}

        public async Task<dynamic> Create(Class classes)
        {
            if (context.Classes.Any(p => p.ClassName.Equals(classes.ClassName) && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.Classes.Add(classes);
                return await context.SaveChangesAsync();
            }
           
        }

        public async Task Delete(string ClassId)
        {
            var classes = context.Classes.Find(ClassId);
            classes.Status = false;
            context.Entry(classes).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Class> Find(string ClassId)
        {
            return await context.Classes.FindAsync(ClassId);
        }

        public async Task<List<Class>> FindAll()
        {
            return await context.Classes.Where(p => p.Status == true).ToListAsync();
        }



        public string GetNewestId()
        {

            return (from classes in context.Classes
                    orderby
                      classes.ClassId descending
                    select classes.ClassId).Take(1).SingleOrDefault();

        }

        public bool RoleExists(string ClassId) => context.Classes.Any(e => e.ClassId == ClassId);

        public async Task<Class> Update(Class classes)
        {
            context.Entry(classes).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return classes;
        }

    }
}
