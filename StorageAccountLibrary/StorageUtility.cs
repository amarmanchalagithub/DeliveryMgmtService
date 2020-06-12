using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using System.Diagnostics;
using System.Configuration;




namespace StorageAccountLibrary
{
    //public class StorageUtility
    //{
    //    //public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
    //    //{
    //    //    CloudStorageAccount storageAccount;
    //    //    try
    //    //    {
    //    //        storageAccount = CloudStorageAccount.Parse(storageConnectionString);
    //    //    }
    //    //    catch (FormatException)
    //    //    {
    //    //        Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
    //    //        throw;
    //    //    }
    //    //    catch (ArgumentException)
    //    //    {
    //    //        Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
    //    //        Console.ReadLine();
    //    //        throw;
    //    //    }

    //    //    return storageAccount;
    //    //}
    //    //public CloudTable CreateCloudTable(string storageQueueConnectionString)
    //    //{
    //    //    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageQueueConnectionString);
    //    //    CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
    //    //    CloudTable cloudTable = tableClient.GetTableReference("");
                       
    //    //        if (!cloudTable.CreateIfNotExists())
    //    //        {
                  
    //    //             return cloudTable;
    //    //        }
    //    //    return null;

    //    //}
    //}


    public class ServiceAPITokens : TableEntity
    {
        public ServiceAPITokens()
        {
        }

        public ServiceAPITokens(string ServiceTokenEndpointURL, string userName , string password,string token,DateTime Token_Created)
        {
            this.ServiceTokenEndpointURL = ServiceTokenEndpointURL;
            this.userName = userName;
            this.password = password;
            this.token = token;
            this.Token_Created = Token_Created;
        }

        public string ServiceTokenEndpointURL { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string token {get; set;}
        public DateTime Token_Created  { get; set; }
    }

}

