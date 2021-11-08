using BankDB;
using DataTierWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataTierWeb.Controllers
{
    public class WithdrawController : ApiController
    {
        public static BankDB.BankDB Instance { get; } = new BankDB.BankDB();

        //To access the functions provided AccountAccessInterfaces
        private static AccountAccessInterface account = Instance.GetAccountInterface();
 

        // POST: api/Withdraw
        public void Post([FromBody]AccountModel value)
        {
            uint accID = 0;
            
            try
            {
                //selecting the account and withdrawing from the respective accoutn
                account.SelectAccount(value.id);
                account.Withdraw(value.amount);
                Instance.SaveToDisk();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot withdraw: " + e);
            }
        }


        //all the otehr functions which are not used in the porject
        /*

        // GET: api/Withdraw
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Withdraw/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/Withdraw/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Withdraw/5
        public void Delete(int id)
        {
        }*/
    }
}
