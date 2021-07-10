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
                await context.SaveChangesAsync();
            }
            return 1;
        }

        public async Task Delete(string classId)
        {
            var classes = await context.Classes.FirstOrDefaultAsync(c => c.ClassId == classId);
            classes.Status = false;
            context.Entry(classes).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Class> Find(string ClassId)
        {
            return await context.Classes.FindAsync(ClassId);
        }

        public async Task<Class> FindAjax(string classId)
        {
            return await context.Classes.FirstOrDefaultAsync(c => c.ClassId == classId && c.Status == true);
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

        public  List<Class> Search(string searchCLass, int filterNumber)
        {
            var classes = context.Classes.AsQueryable();

            if (filterNumber != 0) classes = classes.Where(s => s.NumberOfStudent > filterNumber);

            if (searchCLass != null) classes = classes.Where(b => b.ClassName.StartsWith(searchCLass));;

            var result = classes.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }

        public async Task<Class> Update(Class classes)
        {
            context.Entry(classes).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return classes;
        }

    }
}
