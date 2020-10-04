using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StorageAccountLibrary;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.Cosmos.Table;


namespace Function_Load_SeedData
{
    public static class LoadData
    {
        [FunctionName("LoadData")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            string SecretIdentifier = Environment.GetEnvironmentVariable("StorageAccounykeySecretidentifier");

            try
            {

                var connStr = (kv.GetSecretAsync(SecretIdentifier)).GetAwaiter().GetResult().Value;

                var connectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}",
                                                       "ordermgmtstrgacnt",connStr);
               
                CloudStorageAccount storageAccount;
                storageAccount = CloudStorageAccount.Parse(connStr);
                    //"DefaultEndpointsProtocol=https;AccountName=ordermgmtstrgacnt;AccountKey=psZfKJvyIbf1QdrYkmp6PYCdyMxDyXAlaEIXB8tzrXEi9MaWlFoPiAcq5RLklxiHZdJEXHqCucJQfj7gFvacNg==;EndpointSuffix=core.windows.net");

                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable cloudTable = tableClient.GetTableReference("tabledeliveryservieapikeys");

                cloudTable.CreateIfNotExists();


                //Delete Rows-Updated from visual Studio code
                
                var query = new TableQuery<ServiceAPITokens>();
                var result = await cloudTable.ExecuteQuerySegmentedAsync(query, null);

                // Create the batch operation.  
                TableBatchOperation batchDeleteOperation = new TableBatchOperation();

                foreach (var row in result)
                {
                    batchDeleteOperation.Delete(row);
                }

                // Execute the batch operation.
                if (result.Results.Count > 0)
                {
                    await cloudTable.ExecuteBatchAsync(batchDeleteOperation);
                }

                //Inserting Seed Data

                ServiceAPITokens apitokenData = new ServiceAPITokens();

                apitokenData.RowKey = "DeliveryService";
                apitokenData.PartitionKey = "Token";
                apitokenData.userName = "amarnath";
                apitokenData.password = "Am@rn2t8";
                apitokenData.ServiceTokenEndpointURL = "vdvdvdv";
                apitokenData.token = "dvdvdv";
                apitokenData.Token_Created = DateTime.Now;

                TableOperation tableOperation = TableOperation.InsertOrReplace(apitokenData);
                cloudTable.Execute(tableOperation);
                //ServiceAPITokens tokenseedData = new ServiceAPITokens();

            }
            catch (Exception Ex)
            {
                return new OkObjectResult(Ex.Message);

            }


            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            string responseMessage = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."+$"Hello,This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
