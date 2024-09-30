using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using System.Runtime.InteropServices.JavaScript;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class PrincipalDesignateTask
{
    public YesNoNotApplicable? CommissionedExternalExpertVisitToSchool { get; set; }

    public DateTime? ExpectedDatePrincipalDesignateAppointed { get; set; }

    public bool?  TrustAppointedPrincipalDesignate { get; set; }
    
    public DateTime?  ActualDatePrincipalDesignateAppointed { get; set; }
    
}