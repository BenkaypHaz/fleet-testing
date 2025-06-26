using Library.Infraestructure.Persistence.DTOs.General.ErrorLogs.Create;
using Library.Infraestructure.Persistence.DTOs.Helpers;
using Library.Infraestructure.Persistence.Models;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Infraestructure.Common.Helpers
{
    public static class BaseHelper
    {
        #region Environment
        public static string GetEnvVariable(string EnvVariableName)
        {
            string? connectionString = Environment.GetEnvironmentVariable(EnvVariableName);
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine($"The Environment variable {EnvVariableName} is not configured.");
                throw new Exception($"The Environment variable {EnvVariableName} is not configured.");
            }
            return connectionString;
        }
        #endregion

        #region Utils

        public static string GetCustomerFolder(string lockerId, string firstName, string lastName) 
        {
            return $"{lockerId.ToUpper()}-{firstName.ToUpper().Trim()}{lastName.ToUpper().Trim()}";
        }

        #endregion

        #region DataBaseConnection

        public static string GetConnectionString()
        {
            string? connectionString = Environment.GetEnvironmentVariable("DBConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("The Environment variable DBConnectionString is not configured.");
                throw new Exception("The Environment variable DBConnectionString is not configured.");
            }
            return connectionString;
        }

        #endregion

        #region Encrypt

        public static IConfiguration? Configuration { get; }

        public static byte[]? GetEncryptedSecretKey()
        {
            string? SecretKey = Environment.GetEnvironmentVariable("SecretKey");
            if (string.IsNullOrEmpty(SecretKey))
            {
                Console.WriteLine("The Environment variable SecretKey is not configured.");
                throw new Exception("The Environment variable SecretKey is not configured.");
            }

            string EncryptedSecretKey = GetSha256(SecretKey);
            var FormatKey = Encoding.ASCII.GetBytes(EncryptedSecretKey);
            return FormatKey;
        }

        public static string GetSha256(string str)
        {
            var sha256 = SHA256.Create();
            var encoding = new ASCIIEncoding();
            byte[] stream;
            var sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (var i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static int GenerateRandomNum(int length)
        {
            var random = new Random();
            return random.Next(0, (int)Math.Pow(10, length));
        }

        #endregion

        #region Emails

        public static async Task<bool> SendEmail(string NameRecipient, string emailRecipient, string template, string subject = "CoreExpress - Registro completado con éxito")
        {
            var emailSender = "info@coreexpresshn.com";
            var password = "ugov foas calx rits";
            // Configurar el mensaje MIME
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CoreExpress", emailSender));
            message.To.Add(new MailboxAddress(NameRecipient, emailRecipient));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = template
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    // Ignorar la validación del certificado para Gmail
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = 30000;
                    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(emailSender, password);
                    await client.SendAsync(message);
                    return true;
                }
                catch (Exception Ex)
                {
                    await SaveErrorLog(Ex);
                    return false;
                }
                finally
                {
                    if (client.IsConnected)
                        await client.DisconnectAsync(true);
                }
            }
        }

        #endregion

        #region Errors

        public static async Task<long> SaveErrorLog(Exception ex)
        {
            long response = 0;
            try
            {
                var errorDetails = GetExceptionDetails(ex);
                var model = new GeneralErrorLog
                {
                    Project = errorDetails.ProjectName,
                    Class = errorDetails.ProjectName,
                    Method = errorDetails.MethodName,
                    LineNumber = errorDetails.LineNumber,
                    ErrorDescription = errorDetails.Description,
                    CreatedDate = errorDetails.ExceptionDate
                };

                using (var db = new DataBaseContext())
                {
                    await db.GeneralErrorLogs.AddAsync(model);
                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                    {
                        await SendNotification(errorDetails);
                        response = model.Id;
                    }
                }
                return response;
            }
            catch (Exception innerEx)
            {
                Console.Error.WriteLine($"Critical error logging failed: {innerEx}");
                response = 0;
                return response;
            }
        }

        private static ErrorLogsCreateDTO GetExceptionDetails(Exception ex)
        {
            var stackTrace = new StackTrace(ex, true);
            var frame = stackTrace.GetFrame(0); // Primera línea del stack trace

            var response = new ErrorLogsCreateDTO();
            response.ProjectName = ex.TargetSite?.DeclaringType?.Assembly.GetName().Name ?? "CoReExpress Infraestructure";
            response.ClassName = ex.TargetSite?.DeclaringType?.FullName?.Split('+')[0] ?? "UnknownClass";
            response.MethodName = frame?.GetMethod()?.Name ?? "UnknownMethod";
            response.LineNumber = frame?.GetFileLineNumber() ?? -1;
            response.ExceptionDate = DateTime.UtcNow;

            var fullMessage = new StringBuilder();
            var currentEx = ex;
            while (currentEx != null)
            {
                fullMessage.AppendLine(currentEx.Message);
                currentEx = currentEx.InnerException;
            }
            response.Description = fullMessage.ToString();

            return response;
        }

        private static async Task SendNotification(ErrorLogsCreateDTO errorDetails)
        {
            var webhookUrl = "https://discord.com/api/webhooks/1235311698915623075/NmF3bZCDlv3CQ7g5G0wVp0RW0rFp6zplw2MyyIPgf1U9HbM0VCU-DG15ilz7oVstRyce";
            string payload = $@"
                                {{
                                    ""embeds"": [
                                        {{
                                            ""title"": ""An error has occurred!"",
                                            ""description"": ""{errorDetails.Description}"",
                                            ""color"": 16776960,
                                            ""fields"": [
                                                {{ ""name"": ""Project"", ""value"": ""{errorDetails.ProjectName}"", ""inline"": false }},
                                                {{ ""name"": ""Date"", ""value"": ""{errorDetails.ExceptionDate}"", ""inline"": true }},
                                                {{ ""name"": ""Source"", ""value"": ""{errorDetails.MethodName} Line: {errorDetails.LineNumber}"", ""inline"": true }}
                                            ]
                                        }}
                                    ]
                                }}";

            using (var client = new HttpClient())
            {
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                await client.PostAsync(webhookUrl, content);
            }
        }

        #endregion

        #region Media
        public static FileTypeDTO GetFileType(string FileBase64)
        {
            FileTypeDTO file = new FileTypeDTO();

            try
            {
                string base64SinEncabezado = Regex.Replace(FileBase64, "^.*?,", "");
                string base64Data = base64SinEncabezado.Trim();
                base64Data = base64Data.Replace(" ", "+"); // Reemplaza espacios en blanco con el caracter '+' si es necesario
                file.File = Convert.FromBase64String(base64Data);

                if (file.File != null && file.File.Length > 4)
                {
                    // Comparar los bytes de la firma de PNG
                    if (file.File[0] == 0x89 && file.File[1] == 0x50 && file.File[2] == 0x4E && file.File[3] == 0x47)
                    {
                        file.Type = "PNG Image";
                        file.Extension = "png";
                        file.MediaType = 1;
                    }

                    // Comparar los bytes de la firma de JPEG
                    else if (file.File[0] == 0xFF && file.File[1] == 0xD8 && file.File[2] == 0xFF)
                    {
                        file.Type = "JPEG Image";
                        file.Extension = "jpeg";
                        file.MediaType = 1;
                    }

                    // Comparar los bytes de la firma de GIF
                    else if (file.File[0] == 0x47 && file.File[1] == 0x49 && file.File[2] == 0x46)
                    {
                        file.Type = "GIF Image";
                        file.Extension = "gif";
                        file.MediaType = 1;
                    }

                    // Comparar los bytes de la firma de PDF
                    else if (file.File[0] == 0x25 && file.File[1] == 0x50 && file.File[2] == 0x44 && file.File[3] == 0x46)
                    {
                        file.Type = "PDF Document";
                        file.Extension = "pdf";
                        file.MediaType = 4;
                    }

                    // Comparar los bytes de la firma de MP3
                    else if (file.File[0] == 0xFF && (file.File[1] & 0xE0) == 0xE0)
                    {
                        file.Type = "MP3 Audio";
                        file.Extension = "mp3";
                        file.MediaType = 3;
                    }

                    // Comparar los bytes de la firma de MP4
                    else if (file.File[0] == 0x00 && file.File[1] == 0x00 && file.File[2] == 0x00 && (file.File[4] == 0x66))
                    {
                        file.Type = "MP4 Video";
                        file.Extension = "mp4";
                        file.MediaType = 2;
                    }

                    // Comparar los bytes de la firma de OGG (tanto audio como video)
                    else if (file.File[0] == 0x4F && file.File[1] == 0x67 && file.File[2] == 0x67 && file.File[3] == 0x53)
                    {
                        file.Type = "OGG Audio/Video";
                        file.Extension = "ogg";
                        file.MediaType = 3;
                    }

                    else
                        file.Type = "Others";
                }

            }
            catch (Exception Ex)
            {
                SaveErrorLog(Ex).GetAwaiter();
            }

            return file;
        }
        #endregion

        #region GoogleMaps
        public static async Task<(string city, string state, string country)> GetCityStateCountryFromCoordinates(double latitude, double longitude)
        {
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY");
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={apiKey}";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var jsonResponse = JObject.Parse(response);

                var city = jsonResponse["results"]?[0]["address_components"]?
                    .FirstOrDefault(component => component["types"].ToString().Contains("locality"))?["long_name"]?.ToString();

                var state = jsonResponse["results"]?[0]["address_components"]?
                    .FirstOrDefault(component => component["types"].ToString().Contains("administrative_area_level_1"))?["long_name"]?.ToString();

                var country = jsonResponse["results"]?[0]["address_components"]?
                    .FirstOrDefault(component => component["types"].ToString().Contains("country"))?["long_name"]?.ToString();

                // Depurar el estado para quitar "Department" si está presente
                if (!string.IsNullOrEmpty(state) && state.Contains("Department"))
                {
                    state = state.Split(new[] { " Department" }, StringSplitOptions.None)[0];
                }

                return (city, state, country);
            }
        }


        #endregion
    }
}
