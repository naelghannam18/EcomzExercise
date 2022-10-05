using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace EcomzExercise.Middleware
{
    public class HttpLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Get The Request And Response
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            // Enabling Buffering to Be able to Read Request Body
            request.EnableBuffering();

            // Continuing the Api Call, we dont need to wait for the Api Call to Finish
            _next(context);

            // Getting Request Headers Information And Body
            string requestString = "Request Information:";
            requestString += "\n====================\n\n";
            requestString += "Date: " + DateTime.Now + "\n";
            foreach(var key in request.Headers.Keys)
            {
                requestString += key + " : " + request.Headers[key] + "\n";
            }

            var bodyStream = new StreamReader(request.Body);

            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);

            var bodyText = bodyStream.ReadToEnd();


            // Checking for Body Length 
            if (bodyText.Length > 0)
            {
                requestString += "Body: " + bodyText;
            }
            requestString += "\n\n====================\n\n";


            // getting Response Information
            string responseString = "Response Information:";
            responseString += "\n====================\n\n";
            responseString += "Date: " + DateTime.Now + "\n";
            foreach (var key in response.Headers.Keys)
            {
                responseString += key + " : " + response.Headers[key] + "\n";
            }
            responseString += "Status Code : " + response.StatusCode + "\n";

            responseString += "\n\n====================\n\n";

            // Logging the Data 
            _logger.LogInformation(requestString + responseString);

            // Saving the Data
            await WriteToFile(requestString + responseString);


        }

        

        // Saving to Logs to a Local File
        private async Task WriteToFile(string log)
        {
            string projectDirectory = @"C:\Users\G.K\Desktop\exercise\";
            string filename = "ApiLogs.txt";
            string fullPath = projectDirectory + filename;
            await File.AppendAllTextAsync(fullPath, log);
        }
    }
}
