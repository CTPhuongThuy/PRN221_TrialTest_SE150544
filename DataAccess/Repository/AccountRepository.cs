using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Hraccount GetHraccount(string email)
        => AccountDAO.Instance.GetAccount(email);
    }
}
