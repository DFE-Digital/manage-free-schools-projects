using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

public class FaithStatusAndTypeModel : CreateProjectBaseModel
{
    [BindProperty(Name = "faith-status")]
    [Required(ErrorMessage = "Select the faith status of the free school.")]
    public FaithStatus FaithStatus { get; set; }
    
    [BindProperty(Name = "faith-type")]
    public FaithType FaithType { get; set; }
    
    [BindProperty(Name = "other-faith-type")]
    [Display(Name = "Other faith type")]
    public string OtherFaithType { get; set; }
    
    private readonly ICreateProjectCache _createProjectCache;
    private readonly ErrorService _errorService;

    public FaithStatusAndTypeModel(ICreateProjectCache createProjectCache, ErrorService errorService)
    {
        _createProjectCache = createProjectCache;
        _errorService = errorService;
    }
    
    public void OnGet()
    {
        var project = _createProjectCache.Get();
        BackLink = GetPreviousPage(CreateProjectPageName.FaithStatusAndType, project.Navigation);
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }
        
        ValidateFaithFields();
        
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }
        
        var project = _createProjectCache.Get();

        project.FaithType = FaithType;
        project.FaithStatus = FaithStatus;
        
        _createProjectCache.Update(project);

        return Redirect(GetNextPage(CreateProjectPageName.FaithStatusAndType));
    }

    private void ValidateFaithFields()
    {
        if ((FaithStatus == FaithStatus.Ethos || FaithStatus == FaithStatus.Designation) && (FaithType == FaithType.NotSet))
        {
            ModelState.AddModelError("faith-type", "Faith type is required.");
        }

        if (FaithStatus == FaithStatus.None)
        {
            FaithType = FaithType.NotSet;
        }

        if (FaithType == FaithType.Other)
        {
            if (string.IsNullOrEmpty(OtherFaithType))
            {
                ModelState.AddModelError("other-faith-type", "Other faith type is required.");
            }
            else if (OtherFaithType.Length > 100)
            {
                ModelState.AddModelError("other-faith-type", "Other faith type must be 100 characters or less.");
            }
            else if (Regex.Match(OtherFaithType, "[^a-zA-Z\\s]", RegexOptions.None, TimeSpan.FromSeconds(5)).Success)
            {
                ModelState.AddModelError("other-faith-type", "Other faith type must only contain letters and spaces.");
            }
        }
        else if (FaithType != FaithType.Other && !string.IsNullOrEmpty(OtherFaithType))
        {
            OtherFaithType = string.Empty;
        }
    }
    
}