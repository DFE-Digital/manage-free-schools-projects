using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG
{
    public class StopPaymentTask
    {
        public string PaymentStopped { get; set; }

        public DateTime? PaymentStoppedDate { get; set; }
    }
}
