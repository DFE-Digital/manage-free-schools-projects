using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

public class FaithStatusModel : CreateProjectBaseModel
{
    public void OnGet()
    {
        
    }

    public string BackLink { get; set; }
    public string FaithStatus { get; set; }
}