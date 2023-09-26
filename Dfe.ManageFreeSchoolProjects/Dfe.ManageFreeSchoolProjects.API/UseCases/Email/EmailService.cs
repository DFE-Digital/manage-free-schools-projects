using Notify.Client;
using Notify.Models.Responses;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Email;


public interface IEmailService
{
    Task<EmailNotificationResponse> SendEmail(string email, string templateId);
}

public class EmailService : IEmailService
{
    //TODO: set this, where will we store & retrieve it for Dev & Prod?
    private const string _apiKey = "";

    public Task<EmailNotificationResponse> SendEmail(string email, string templateId)
    {
        try
        {
            var client = new NotificationClient(_apiKey);
            return client.SendEmailAsync(email, templateId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Sending Email through Gov Notify failed.");
            throw;
        }    
    }
}