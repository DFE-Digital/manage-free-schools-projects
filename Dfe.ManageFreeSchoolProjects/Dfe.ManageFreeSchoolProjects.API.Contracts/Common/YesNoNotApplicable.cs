using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Common
{
    public enum YesNoNotApplicable
    {
        [Description("Yes")]
        Yes = 1,
        [Description("No")]
        No = 2,
        [Description("Not applicable")]
        NotApplicable = 3
    }
}
