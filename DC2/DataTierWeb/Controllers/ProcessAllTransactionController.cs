using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankDB;

namespace DataTierWeb.Controllers
{

    public class ProcessAllTransactionController : ApiController
    {
        public static BankDB.BankDB Instance { get; } = new BankDB.BankDB();

        //To access the functions provided under user and AccountAccessInterfaces respectively
        private static UserAccessInterface user = Instance.GetUserAccess();
        private static TransactionAccessInterface trans = Instance.GetTransactionInterface();
        // GET: api/ProcessAllTransaction
 
        // POST: api/ProcessAllTransaction
        public void Post()
        {
            try
            {
                //process all transactions and save them to disk
                Instance.ProcessAllTransactions();
                Instance.SaveToDisk();
            }

            catch(Exception e)
            {
                Console.WriteLine("Cannot process transactions" + e);
            }
        }

        //all the otehr auto generated code which are not used 

        /*
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProcessAllTransaction/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/ProcessAllTransaction/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProcessAllTransaction/5
        public void Delete(int id)
        {
        }*/
    }
}
