
using DataTierWeb.Controllers;
using DataTierWeb.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    //file which implements all the functions declared in the business tier interface
    class BLoginImplementation : BusinessInterface
    {
        //creating objects of the controllers in the data tier to call the respective functions in data tier
        CreateUserController create = new CreateUserController();
        GetUserDetailsController details = new GetUserDetailsController();
        GetAccountInfoController account = new GetAccountInfoController();
        GetAccountBalanceController balance = new GetAccountBalanceController();
        DepositController deposit = new DepositController();
        WithdrawController withdraw = new WithdrawController();
        TransactionController transaction = new TransactionController();
        FundTransferController fund = new FundTransferController();
        ProcessAllTransactionController process = new ProcessAllTransactionController();
        CreateAccountController newAccount = new CreateAccountController();

        //creating objects of the model classes used in the data tier to pass values to the respective controllers of the data tier.
        UsersModel users = new UsersModel();
        AccountModel accModel = new AccountModel();
        TransactionModel transModel = new TransactionModel();


        //creating a user account by passing firstname and last name as input.
        public uint CreateUserAccount(String fname, String lname)
        {
            users.fname = fname;
            users.lname = lname;

            //calling createUserController of the data tier, and passing the inputs as model to the POST() function of the controller.
            uint userID = create.Post(users);
            
            return userID;
        }


        //Depositing amount to the selected account, by passing account ID and amount as inputs
        public uint Deposit(uint accid, uint amount)
        {
            accModel.id = accid;
            accModel.amount = amount;
            //calling DepositController of the data tier, and passing the inputs as model to the POST() function of the controller.
            deposit.Post(accModel);

            //after depositing, calling GetAccountBalanceController of the data tier, and passing the inputs as model to the POST() function of the controller.
            //It reads the account ID and returns the balance.
            uint accBalance = balance.Post(accModel);


            return accBalance;

        }

        //Withdrawing amount from the selected account, by passing account ID and amount as inputs
        public void Withdraw(uint accid, uint amount)
        {
            accModel.id = accid;
            accModel.amount = amount;

            //calling WithdrawController of the data tier, and passing the inputs as model to the POST() function of the controller.
            withdraw.Post(accModel);
        }


        //getiing balance of selected account by calling GetAccountBalanceController, and passing account id as model.
        public uint GetAccountBalance(uint accid)
        {
            accModel.id = accid;
            uint accBalance = balance.Post(accModel);
            return accBalance;
        }


        //Retrieving list of accounts of the selected user, by calling GetAccountInfoController POST() and passing account id through model.

        public List<uint> GetAccountIDsByUser(uint id)
        {
            accModel.userId = id;
            List<uint> accounts = account.Post(accModel);
            return accounts;
        }


        //Seraching for user account based on the userID passed
        public uint SearchUserAccount(uint id)
        {
            uint userID = 0;
            //calls get() of the GetUserDetailsController, and the output is stored in a userList
            List<uint> userList = details.Get();

          //condition to check whether id passed, exist in the userList
                if (userList.Contains(id))
                {
                    userID = id;
                    
                }
            
                
            return userID;

        }

        uint[] output = new uint[2];

        public string transferFund(uint sender, uint receiver, uint amount)
        {
            String result;
            transModel.sender = sender;
            transModel.receiver = receiver;
            transModel.amount = amount;
           
            //calling POST of the transactionController which returns an array
     
            output = transaction.Post(transModel);

          
            //first elemnt[0] of array has atransactionID and second element[1] of array has account balance.
            //checking whether there is insufficeinet fund to carryout transaction
            if (output[1] < amount)
            {
                Console.WriteLine("Client tried to trasnfer from an account which has insufficient fund...");
                throw new FaultException<InsufficientFund>(new InsufficientFund()
                { Issue = "Your account balance is insufficient to proceed transaction..." });
                result = "Your account balance is insufficient to proceed transaction...";
            }

            //if there is sucfficient fund
            else
            {
                uint transID = output[0];
                transModel.id = transID;
                transModel.sender = sender;
                transModel.receiver = receiver;
                transModel.amount = amount;

                //call the post method of the fundTransferController() and carryout the transaction by passing values as model.
                fund.Post(transModel);
                result = "Rs." + amount + " transfered from " + sender + "to " + receiver;
            }



            //Instance.ProcessAllTransactions();
            //Instance.SaveToDisk();
                

            return result;
        }

        //To get the list of transactions
        public List<uint> GetTransactions()
        {
            List<uint> tranList = new List<uint>();
            //output[0] has transaction ID. So each time when a transaction is created, new ID will be generated and stored in output[0].
            //this id is added to the list, so that admin can view all the ids generated by calling this function in client GUI.
            tranList.Add(output[0]);
            return tranList;
        }

        //to return all the existing list of users, Get() method of GetUserDetailsController is called.
        public List<uint> GetAllUsers()
        {
            List<uint> userList = new List<uint>();
            userList = details.Get();

            return userList;
        }

        //getting the username and password of the respective user and storing the result in an array.
        //first element stores firstname[0]
        //second element stores lastname[1]
        public string[] GetSpecificUserDetails(uint id)
        {
            users.id = id;
            string[] output = new string[2];
            output = details.Post(users);

            return output;
        }

        //to process all transactions
        public void ProcessTransactions(List<uint> trans)
        {
          foreach(uint element in trans)
            {
               // uint sender =
                process.Post();
            }
        }

        //to create a new account for the specific user.
        public uint CreateAccount(uint userID)
        {
            accModel.userId = userID;
           uint accID =  newAccount.Post(accModel);
            return accID;
        }
    }
}
