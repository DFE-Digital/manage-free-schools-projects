using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace Dfe.ManageFreeSchoolProjects.Pages.BulkEdit
{
    public class BulkEditCompleteModel : PageModel
    {
        [FromQuery]
        public int Count { get; set; }
        public void OnGet()
        {
        }
    }
}
