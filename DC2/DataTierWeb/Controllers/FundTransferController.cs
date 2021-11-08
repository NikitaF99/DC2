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
    public class FundTransferController : ApiController
    {
        //cretaing object of bandBD
        public static BankDB.BankDB Instance { get; } = new BankDB.BankDB();

        //To access the functions provided under user and AccountAccessInterfaces respectively
        private static TransactionAccessInterface trans = Instance.GetTransactionInterface();
        private static AccountAccessInterface account = Instance.GetAccountInterface();

 
        // POST: api/FundTransfer
        public void Post([FromBody]TransactionModel value)
        {
            //selecting transaction id and account id, and withdrawing from sender's account
            trans.SelectTransaction(value.id);
            uint balance = 0;
            account.SelectAccount(value.sender);
            account.Withdraw(value.amount);
            balance = account.GetBalance();
          //  Instance.ProcessAllTransactions();
           Instance.SaveToDisk();


            //selecting account id of receiver and depositing in receivers account
            account.SelectAccount(value.receiver);
            account.Deposit(value.amount);
            balance = account.GetBalance();
            // Instance.ProcessAllTransactions();
            trans.SelectTransaction(value.id);

           Instance.SaveToDisk();

         //   Instance.ProcessAllTransactions();

        }







        //all the other auto generateed functions which are not used in this project
        /*
        // GET: api/FundTransfer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FundTransfer/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/FundTransfer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FundTransfer/5
        public void Delete(int id)
        {
        }*/
    }
}
