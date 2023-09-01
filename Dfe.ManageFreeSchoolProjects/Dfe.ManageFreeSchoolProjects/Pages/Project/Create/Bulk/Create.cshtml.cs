using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Bulk
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public IFormFile Upload { get; set; }

        public ProjectTable ProjectTable { get; set; }

        public List<ProjectRowError> RowErrors { get; set; } = new();

        private readonly IProjectTableReader _projectTableReader;
        private readonly ICreateBulkProjectValidator _createBulkProjectValidator;

        public CreateModel(
            IProjectTableReader projectTableReader, 
            ICreateBulkProjectValidator createBulkProjectValidator)
        {
            _projectTableReader = projectTableReader;
            _createBulkProjectValidator = createBulkProjectValidator;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            using MemoryStream stream = new MemoryStream();

            await Upload.CopyToAsync(stream);

            var projectTable = _projectTableReader.Read(stream, Upload.ContentType);

            RowErrors = _createBulkProjectValidator.Validate(projectTable);

            ProjectTable = projectTable;

            return Page();
        }
    }
}
