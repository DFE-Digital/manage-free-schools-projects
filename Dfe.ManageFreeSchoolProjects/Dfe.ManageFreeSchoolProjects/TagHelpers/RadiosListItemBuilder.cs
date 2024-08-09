using Microsoft.AspNetCore.Mvc.ViewFeatures;

internal static class RadiosListItemBuilder
{

    public static string BuildHint(string value, string hint)
    {
        return $@"<div id=""{value.ToLower()}-hint"" class=""govuk-hint govuk-radios__hint"">
                         {hint}
                     </div>";
    }

    public static string BuildLabel(string value, string description)
    {
        return $@"<label class=""govuk-label govuk-radios__label"" for=""project-status-{value.ToLower()}"">
                         {description}
                     </label>";
    }

    public static string BuildRadioInput(string value, ModelExpression aspfor, string conditionallink = null)
    {
        var radioChecked = value == aspfor.Model.ToString() ? "checked=\"checked\"" : "";
        var conditional = conditionallink != null ? $@"data-aria-controls=""{conditionallink}""" : "";
        return $@"<input class=""govuk-radios__input"" id=""project-status-{value}"" name=""project-status"" type=""radio"" value=""{value}"" {radioChecked} {conditional}/>";
    }
}