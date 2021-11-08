using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    //Contracts are useful for building WCF service applications.
    //They define what the protocol (binding) the service uses, how the communication will be done,
    //what message exchange format to use and so on.

    //The following are the contracts are available in WCF:

    /* Service contracts
     Data Contracts
     Message contracts
     Fault Contract
     Operation Contract*/

    [ServiceContract]
    public interface BusinessInterface
    {
        //declaring all the available functions as operation contract so that these are made available to the client
        //client can access these functions through endpoints
        [OperationContract]
        uint CreateUserAccount(String fname, String lname);

        [OperationContract]
        uint CreateAccount(uint userID);

        [OperationContract]
        uint SearchUserAccount(uint id);

        [OperationContract]
        List<uint> GetAccountIDsByUser(uint id);

        [OperationContract]
        uint GetAccountBalance(uint accid);

        [OperationContract]
        uint Deposit(uint accid , uint amount);

        [OperationContract]
        void Withdraw(uint accid, uint amount);

        [OperationContract]
        string transferFund(uint sender, uint receiver, uint amount);

        [OperationContract]
        List<uint> GetTransactions();

        [OperationContract]
        List<uint> GetAllUsers();

        [OperationContract]
        string[] GetSpecificUserDetails(uint id);

        [OperationContract]
        void ProcessTransactions(List<uint> trans);
    }
}
