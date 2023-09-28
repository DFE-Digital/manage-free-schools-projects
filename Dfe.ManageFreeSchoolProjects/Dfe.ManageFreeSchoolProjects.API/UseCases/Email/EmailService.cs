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
    
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<EmailNotificationResponse> SendEmail(string email)
    {
        var client = new NotificationClient(_configuration.GetValue<string>("GovNotify:ApiKey"));
        return client.SendEmailAsync(email, _configuration.GetValue<string>("GovNotify:TemplateId"));
    }

    public bool IsEmailValid(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}