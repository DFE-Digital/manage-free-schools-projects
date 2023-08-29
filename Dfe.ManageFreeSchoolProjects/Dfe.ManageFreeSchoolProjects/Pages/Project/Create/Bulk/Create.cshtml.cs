using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly ICreateProjectService _createProjectService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(
            IProjectTableReader projectTableReader, 
            ICreateBulkProjectValidator createBulkProjectValidator,
            ICreateProjectService createProjectService,
            ILogger<CreateModel> logger)
        {
            _projectTableReader = projectTableReader;
            _createBulkProjectValidator = createBulkProjectValidator;
            _createProjectService = createProjectService;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task OnPostAsync()
        {
            _logger.LogMethodEntered();

            try
            {

                using MemoryStream stream = new MemoryStream();

                await Upload.CopyToAsync(stream);

                var projectTable = _projectTableReader.Read(stream, Upload.ContentType);

                RowErrors = _createBulkProjectValidator.Validate(projectTable);

                ProjectTable = projectTable;

                List<CreateProjectRequest> createProjectsRequest = new List<CreateProjectRequest>();

                foreach (ProjectRow proj in ProjectTable.Rows)
                {
                    CreateProjectRequest projReq = new CreateProjectRequest()
                    {
                        ProjectId = proj.ProjectId,
                        SchoolName = proj.ProjectTitle,
                    };

                    createProjectsRequest.Add(projReq);
                };

                await _createProjectService.Execute(createProjectsRequest);
            }

            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }

            return Redirect("~/");
        }
    }
}
