using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
