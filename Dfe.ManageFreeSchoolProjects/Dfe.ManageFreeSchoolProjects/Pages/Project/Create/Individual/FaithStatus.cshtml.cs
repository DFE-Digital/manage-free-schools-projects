﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

public class FaithStatusModel : CreateProjectBaseModel
{
    [BindProperty(Name = "faith-status")]
    [Required(ErrorMessage = "Select the faith status of the free school.")]
    public FaithStatus FaithStatus { get; set; }

    private readonly ICreateProjectCache _createProjectCache;
    private readonly ErrorService _errorService;

    public FaithStatusModel(ICreateProjectCache createProjectCache, ErrorService errorService)
    {
        _createProjectCache = createProjectCache;
        _errorService = errorService;
    }

    public void OnGet()
    {
        var project = _createProjectCache.Get();
        FaithStatus = project.FaithStatus;

        BackLink = GetPreviousPage(CreateProjectPageName.FaithStatus, project.Navigation);
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        var project = _createProjectCache.Get();

        if (FaithStatus == FaithStatus.Ethos || FaithStatus == FaithStatus.Designation)
            project.Navigation = CreateProjectNavigation.GoToFaithType;
        
        project.FaithStatus = FaithStatus;

        _createProjectCache.Update(project);

        return Redirect(GetNextPage(CreateProjectPageName.FaithStatus,string.Empty, project.Navigation));
    }
}