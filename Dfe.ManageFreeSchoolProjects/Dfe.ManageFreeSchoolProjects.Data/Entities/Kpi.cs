using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Data.Entities.Existing
{
    public partial class Kpi
    {
        public int? UserId { get; set; }

        public User? User { get; set; }

        public string SchoolDetailsAlternativeProvision { get; set; }

        public string SchoolDetailsSpecialEducationNeeds { get; set; }
        public string KeyContactsFsgLeadContactEmail { get; set; }
        public string KeyContactsFsgTeamLeaderEmail { get; set; }
        public string KeyContactsFsgGrade6Email { get; set; }
        public string KeyContactsEsfaCapitalProjectDirectorEmail { get; set; }
        public string KeyContactsEsfaCapitalProjectManagerEmail { get; set; }
        public string KeyContactsChairOfGovernorsMatRole { get; set; }
        public string KeyContactsOfstedContact { get; set; }
        public string KeyContactsOfstedContactEmail { get; set; }
        public string KeyContactsOfstedContactPhone { get; set; }
        public string KeyContactsOfstedContactRole { get; set; }
    }
}
