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
    public class DepositController : ApiController
    {
        //cretaing object of bandBD.dll
        public static BankDB.BankDB Instance { get; } = new BankDB.BankDB();

        //To access the functions provided under AccountAccessInterfaces 

        private static AccountAccessInterface account = Instance.GetAccountInterface();
  

        // POST: api/Deposit
        public void Post([FromBody]AccountModel value)
        {
            try
            {
                //depositing amount to the respective account
                account.SelectAccount(value.id);
                account.Deposit(value.amount);
                Instance.ProcessAllTransactions();
                Instance.SaveToDisk();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot deposit: " + e);
            }
        }
        

        //all the other functions which are not used in this project
        /*

        // PUT: api/Deposit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Deposit/5
        public void Delete(int id)
        {
        }

        // GET: api/Deposit
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Deposit/5
        public string Get(int id)
        {
            return "value";
        }
        */
    }
}
