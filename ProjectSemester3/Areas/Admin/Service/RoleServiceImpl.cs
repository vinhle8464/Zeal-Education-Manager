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
    public class RoleServiceImpl : IRolesService
    {
        private readonly DatabaseContext context;
        public RoleServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }




        public async Task<int> CountId()
        {
            return await context.Roles.Where(p => p.Status == true).CountAsync();
        }

        // public async Task<int> CountIdById(string RoleId) => await context.Roles.Where(p => p.RoleId.Contains(RoleId) && p.Status == true).CountAsync();

        public async Task<dynamic> Create(Role role)
        {
            if (context.Roles.Any(p => p.RoleName.Equals(role.RoleName) && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.Roles.Add(role);
                return await context.SaveChangesAsync();
            }

        }

        public async Task Delete(string RoleId)
        {
            var role = context.Roles.Find(RoleId);
            role.Status = false;
            context.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Role> Find(string RoleId) => await context.Roles.FirstOrDefaultAsync(p => p.RoleId == RoleId && p.Status == true);

        public async Task<List<Role>> FindAll() => await context.Roles.Where(p => p.Status == true).ToListAsync();

        public string GetNewestId()
        {

            return (from roles in context.Roles
                   
                    orderby
                      roles.RoleId descending
                    select roles.RoleId).Take(1).SingleOrDefault();

        }

        public bool Exists(string roleId) => context.Roles.Any(e => e.RoleId == roleId && e.Status == true);

        public async Task<Role> Update(Role role)
        {
            context.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> FindAjax(string RoleId) => await context.Roles.FirstOrDefaultAsync(r => r.RoleId == RoleId && r.Status == true);
      
    }
}
