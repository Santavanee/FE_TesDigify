using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using MVCNetCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.Json;
using System.Web.Mvc;


namespace BuzzLogic.Transaction
{
    public class RegisterBL
    {
        private IConfiguration Configuration;
        private readonly ILogger _Logger;
        private string RouteAPI = "";
        private CustomerDetailResponse responseAPI = new CustomerDetailResponse();



        public RegisterBL(ILogger logger)
        {
            _Logger = logger;
            this.RouteAPI = "https://localhost:44385/";
        }

        public async Task<CustomerDetailResponse> Register(IFormCollection form, IFormFile FileNPWP, IFormFile FilePowerOfAttorey)
        {
            using var client = new HttpClient();
            using var content = new MultipartFormDataContent();

            // Create a customer object and serialize it to JSON
            var customerData = new
            {
                CompanyName = form["CompanyName"].ToString(), // Convert to single string
                NPWP = form["NPWP"].ToString(),
                DirectorName = form["DirectorName"].ToString(),
                PICName = form["PICName"].ToString(),
                Email = form["Email"].ToString(),
                PhoneNumber = form["PhoneNumber"].ToString(),
                AllowAccess = form["AllowAccess"].ToString()
            };

            // Serialize to JSON string
            string jsonData = JsonConvert.SerializeObject(customerData);

            // Add the JSON string as a form field
            content.Add(new StringContent(jsonData), "jsonData");

            // Add the files
            if (FileNPWP != null && FileNPWP.Length > 0)
            {
                var npwpStream = new StreamContent(FileNPWP.OpenReadStream());
                npwpStream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(FileNPWP.ContentType);
                content.Add(npwpStream, "FileNPWP", FileNPWP.FileName);
            }

            if (FilePowerOfAttorey != null && FilePowerOfAttorey.Length > 0)
            {
                var powerStream = new StreamContent(FilePowerOfAttorey.OpenReadStream());
                powerStream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(FilePowerOfAttorey.ContentType);
                content.Add(powerStream, "FilePowerOfAttorey", FilePowerOfAttorey.FileName);
            }

            var response = client.PostAsync(RouteAPI + "api/Customer/Insert", content).Result;
            string apiResponse = await response.Content.ReadAsStringAsync();
            responseAPI = JsonConvert.DeserializeObject<CustomerDetailResponse>(apiResponse);

            if (!response.IsSuccessStatusCode || responseAPI.Data == null)
            {
                responseAPI.Success = false;
           
            }
         
           

            return responseAPI;
           
        }
     


    }
}
