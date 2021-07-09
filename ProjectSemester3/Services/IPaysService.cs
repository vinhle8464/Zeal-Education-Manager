using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IPaysService
    {
        Task<List<Pay>> FindAll();
        Task<List<Pay>> FindAllPayByAccountId(string AccountId);
        Task<Pay> FindById(int? PayId);
        Task<Pay> Update(Pay pay);
        bool Exists(int PayId);
        Pay GetFee(string studentid, string title);
        Task PayFee(int id);
        public List<Pay> Search(string searchPay);
    }
}
