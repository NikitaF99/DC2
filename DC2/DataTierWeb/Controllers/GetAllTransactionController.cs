using BankDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataTierWeb.Controllers
{
    public class GetAllTransactionController : ApiController
    {
        public static BankDB.BankDB Instance { get; } = new BankDB.BankDB();

        //To access the functions provided under  TransactionAccessInterfaces respectively
        private static TransactionAccessInterface trans = Instance.GetTransactionInterface();



        // GET: api/GetAllTransaction
        public List<uint> Get()
        {
            //geting list of all transaction id
            List<uint> transList = trans.GetTransactions();
            return transList;
        }



        //other auto generated codes which are not used in this project
        /*
        // GET: api/GetAllTransaction/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetAllTransaction
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetAllTransaction/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetAllTransaction/5
        public void Delete(int id)
        {
        }
        */
    }
}
