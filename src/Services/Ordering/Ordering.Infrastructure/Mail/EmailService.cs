using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var to = new EmailAddress(email.To);

            var from = new EmailAddress(_emailSettings.FromAddress, _emailSettings.FromName);

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, string.Empty);

            var response = await client.SendEmailAsync(message);

            _logger.LogInformation($"Sending email to {email.To} with subject {email.Subject}");

            if (response.IsSuccessStatusCode)
                return true;

            _logger.LogError($"An error occurred while sending email to {email.To} with subject {email.Subject}");
            return false;
        }
    }
}