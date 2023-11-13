using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

public class NotifyUser : PageModel
{
    public void OnGet()
    {
        
    }

    [Required]
    [BindProperty(Name = "email")]
    public string Email { get; set; }
}