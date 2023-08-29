using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks
{
    public class SchoolTaskModel : PageModel
    {
        private readonly ILogger<SchoolTaskModel> _logger;

        public SchoolTaskModel(ILogger<SchoolTaskModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogMethodEntered();


        }
    }
}
