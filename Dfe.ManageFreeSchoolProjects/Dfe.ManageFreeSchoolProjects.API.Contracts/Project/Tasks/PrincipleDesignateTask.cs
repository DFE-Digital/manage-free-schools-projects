using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using System.Runtime.InteropServices.JavaScript;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class PrincipalDesignateTask
{
    
    public bool?  TrustAppointedPrincipleDesignate { get; set; }
    
    public DateTime?  TrustAppointedPrincipleDesignateDate { get; set; }
    
    public YesNoNotApplicable? CommissionedExternalExpertVisitToSchool { get; set; }
}