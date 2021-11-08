using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server started");

            //This is the actual host service system
            //ServiceHost provides everything you need to host a service
            ServiceHost host;

            //This represents a tcp/ip binding in the Windows network stack
            //create a runtime communication stack ; i think a stub
            NetTcpBinding tcpBinding = new NetTcpBinding();

            //Bind server to the implementation of DataServer
            host = new ServiceHost(typeof(BLoginImplementation));   
            host.AddServiceEndpoint(typeof(BusinessInterface), tcpBinding, "net.tcp://localhost:50002/BankBiz");     //What is listened to

            host.Open();    //Opening server

            Console.WriteLine("Press Enter to Exit: ");     //Preventing the server from closing
            Console.ReadLine();

            host.Close();   //Closing server
        }
    }
}
