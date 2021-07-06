﻿using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class PaysServiceImpl : IPaysService
    {
        private readonly DatabaseContext context;
        public PaysServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<List<Pay>> FindAllPayByAccountId(string AccountId) => await context.Pays.Where(p => p.AccountId == AccountId).ToListAsync();

        public async Task<List<Pay>> FindAll() => await context.Pays.Take(10).ToListAsync();

        public bool Exists(int PayId) => context.Pays.Any(e => e.PayId == PayId);

        public async Task<Pay> Update(Pay pay)
        {
            context.Entry(pay).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return pay;
        }

        public async Task<Pay> FindById(int? PayId)
        {
            return await context.Pays
                .Include(p => p.Account)
                .FirstOrDefaultAsync(m => m.PayId == PayId);
        }

        public Pay GetFee(string studentid)
        {
            return context.Pays.SingleOrDefault(p => p.AccountId == studentid);
        }

        public async Task PayFee(int id)
        {
            var payment = await context.Pays.FirstOrDefaultAsync(p => p.PayId == id);
            payment.DatePaid = DateTime.Now;
            payment.PayStatus = true;
            context.Entry(payment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            //Thread.Sleep(10);
        }
    }
}