using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.ProjectStatus;

public class ProjectStatusOption
{
    private ProjectStatusOption()
    {
    }

    public ProjectStatusOption(string id, API.Contracts.Project.ProjectStatus value, string description, string hint,
        bool isVisible, bool isConditional)
    {
        Id = id;
        Value = value;
        Description = description;
        Hint = hint;
        IsVisible = isVisible;
        IsConditional = isConditional;
    }

    public ProjectStatusOption(string id, API.Contracts.Project.ProjectStatus value, string description, string hint,
        bool isVisible, bool isConditional, string dateInputId, string dateInputName, string dateInputLabel,
        string dateInputHint, DateTime? dateInputValueAspFor) : this()
    {
        Id = id;
        Value = value;
        Description = description;
        Hint = hint;
        IsVisible = isVisible;
        IsConditional = isConditional;
        DateInputId = dateInputId;
        DateInputName = dateInputName;
        DateInputLabel = dateInputLabel;
        DateInputHint = dateInputHint;
        DateInputValueAspFor = dateInputValueAspFor;
    }

    public string Id { get; set; }
    public API.Contracts.Project.ProjectStatus Value { get; set; }
    public string Description { get; set; }
    public string Hint { get; set; }
    public bool IsVisible { get; set; }
    public bool IsConditional { get; set; }
    public string DateInputId { get; set; }
    public string DateInputName { get; set; }
    public string DateInputLabel { get; set; }
    public string DateInputHint { get; set; }

    public DateTime? DateInputValueAspFor { get; set; }
}