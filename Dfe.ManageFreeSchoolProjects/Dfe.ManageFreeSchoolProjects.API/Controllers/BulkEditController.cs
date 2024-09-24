using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/bulkedit")]
    [ApiController]
    public class BulkEditController(Logger<BulkEditController> logger) : ControllerBase
    {

        [HttpPost]
        [Route("validate")]
        public async Task<ActionResult<ApiSingleResponseV2<BulkEditValidateResponse>>> validate(BulkEditValidateRequest request)
        {
            //            Are all headers in the possible list of headers
            //Has project id been provided
            //Are all values in rows valid for the header they are under
            //Does the project Id match a project in the system
            
            //* Is the data type valid
            //* Are there range validations
            //* Is this a required field
            //* Are there any special validation rules i.e.must be above another field
            return null;
        }
    }
}
