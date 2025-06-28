using System.Net.Mail;
using System.Net;
using MimeKit;
using Library.Infraestructure.Persistence.DTOs.Utils.Emails;

namespace Library.Infraestructure.Common.Helpers
{
    public static class MailDeliveryHelper
    {
        private static readonly string SMTP_HOST = BaseHelper.GetEnvVariable("SMTP_HOST");
        private static readonly string SMTP_FROM_NAME = BaseHelper.GetEnvVariable("SMTP_FROM_NAME");
        private static readonly string SMTP_FROM = BaseHelper.GetEnvVariable("SMTP_FROM");
        private static readonly int SMTP_PORT = Convert.ToInt16(BaseHelper.GetEnvVariable("SMTP_PORT"));
        private static readonly bool SMTP_SSL = Convert.ToBoolean(BaseHelper.GetEnvVariable("SMTP_SSL"));
        private static readonly int SMTP_TIMEOUT = Convert.ToInt16(BaseHelper.GetEnvVariable("SMTP_TIMEOUT"));
        private static readonly string SMTP_USERNAME = BaseHelper.GetEnvVariable("SMTP_USERNAME");
        private static readonly string SMTP_PASSWORD = BaseHelper.GetEnvVariable("SMTP_PASSWORD");

        public static async Task<bool> SendNotificationEmail(string subject, List<ToEmailsDto> toEmails, string template, List<AttachmentsDto>? sendAttachments, bool isTesting)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(SMTP_FROM_NAME, SMTP_FROM));

            if (isTesting)
            {
                message.To.Add(new MailboxAddress("Malcom Medina", "malcomgerardo.medina@gmail.com"));
            }

            foreach (var email in toEmails.Where(c => !string.IsNullOrEmpty(c.NameRecipient) && !string.IsNullOrEmpty(c.EmailRecipient)))
                message.To.Add(new MailboxAddress(email.NameRecipient, email.EmailRecipient));

            message.Subject = $"{SMTP_FROM_NAME} - {subject}";
            var builder = new BodyBuilder();
            builder.HtmlBody = template;

            if (sendAttachments != null && sendAttachments.Count > 0)
                foreach (var attachment in sendAttachments)
                    builder.Attachments.Add(attachment.FileName, attachment.File, ContentType.Parse("application/octet-stream"));

            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = SMTP_TIMEOUT;
                    await client.ConnectAsync(SMTP_HOST, SMTP_PORT, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(SMTP_USERNAME, SMTP_PASSWORD);
                    await client.SendAsync(message);
                    return true;
                }
                catch (Exception Ex)
                {
                    await BaseHelper.SaveErrorLog(Ex);
                    return false;
                }
                finally
                {
                    if (client.IsConnected)
                        await client.DisconnectAsync(true);
                }
            }
        }

        public static async Task<bool> SendNoReplyEmail(string subject, List<ToEmailsDto> toEmails, string template, List<AttachmentsDto>? sendAttachments, bool isTesting)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(SMTP_FROM_NAME, SMTP_FROM));

            if (isTesting)
            {
                message.To.Add(new MailboxAddress("Malcom Medina", "malcomgerardo.medina@gmail.com"));
            }

            foreach (var email in toEmails.Where(c => !string.IsNullOrEmpty(c.NameRecipient) && !string.IsNullOrEmpty(c.EmailRecipient)))
                message.To.Add(new MailboxAddress(email.NameRecipient, email.EmailRecipient));

            message.Subject = $"{SMTP_FROM_NAME} - {subject}";
            var builder = new BodyBuilder();
            builder.HtmlBody = template;

            if (sendAttachments != null && sendAttachments.Count > 0)
                foreach (var attachment in sendAttachments)
                    builder.Attachments.Add(attachment.FileName, attachment.File, ContentType.Parse("application/octet-stream"));

            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = SMTP_TIMEOUT;
                    await client.ConnectAsync(SMTP_HOST, SMTP_PORT, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(SMTP_USERNAME, SMTP_PASSWORD);
                    await client.SendAsync(message);
                    return true;
                }
                catch (Exception Ex)
                {
                    await BaseHelper.SaveErrorLog(Ex);
                    return false;
                }
                finally
                {
                    if (client.IsConnected)
                        await client.DisconnectAsync(true);
                }
            }
        }

        public static async Task<bool> SendReportEmail(string subject, string body, List<string> toEmails, string fileName, byte[] file)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(SMTP_HOST)
                {
                    Port = SMTP_PORT,
                    Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD),
                    EnableSsl = SMTP_SSL,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(SMTP_FROM),
                    Subject = $"{SMTP_FROM_NAME} - {subject}",
                    Body = body,
                    IsBodyHtml = true
                };

                var attachment = new Attachment(new MemoryStream(file), $"{fileName}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                mailMessage.Attachments.Add(attachment);


                foreach (var email in toEmails)
                    mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                await BaseHelper.SaveErrorLog(ex);
                return false;
            }
        }

    }
}
