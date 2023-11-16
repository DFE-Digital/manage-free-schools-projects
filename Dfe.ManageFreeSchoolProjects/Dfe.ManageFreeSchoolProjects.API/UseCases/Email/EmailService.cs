using System.ComponentModel.DataAnnotations;
using Notify.Client;
using Notify.Models.Responses;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Email;


public interface IEmailService
{
    bool IsEmailValid(string email);
    Task<EmailNotificationResponse> SendEmail(string email);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Task<EmailNotificationResponse> SendEmail(string email)
    {
        var apiKey = _configuration.GetValue<string>("GovNotify:ApiKey");

        if (string.IsNullOrEmpty(apiKey))
        {
            _logger.LogWarning("Missing API key for GovNotify.");
            return Task.FromResult(new EmailNotificationResponse());   
        }
        
        var templateId = _configuration.GetValue<string>("GovNotify:TemplateId");
        
        var client = new NotificationClient(apiKey);
        return client.SendEmailAsync(email, templateId);
    }

    public bool IsEmailValid(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}