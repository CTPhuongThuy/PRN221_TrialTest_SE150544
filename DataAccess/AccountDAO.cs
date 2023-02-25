using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class AccountDAO
    {
        private static AccountDAO instance = null;
        private static readonly object instanceLock = new object();
        private AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }

        }

        public Hraccount GetAccount(string email)
        {
            Hraccount account = null;
            try
            {
                using var context = new CandidateManagementContext();
                account = context.Hraccounts.SingleOrDefault(a => a.Email.Equals(email));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }
    }
}
