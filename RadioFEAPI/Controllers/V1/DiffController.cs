using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RadioFEAPI.Models.V1;
using RadioFEAPI.Services.V1.Interfaces;
using System.Text;

namespace RadioFEAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiffController : ControllerBase
    {
        private readonly IDiffService _service;

        public DiffController(IDiffService service)
        {
            _service = service;
        }

        [HttpPost("{id}/left")]
        public async Task<IActionResult> SetLeftValue([FromRoute] int id, [FromBody] InputObject input)
        {
            try
            {
                // Call the service and return answer
                return new OkObjectResult(await _service.Set(new ComparisonInsertion
                {
                    Id = id,
                    Input = input.Input,
                    Side = SideToSet.Left
                }));

            } catch
            {
                return new ObjectResult(new FeedbackObject { Message = "Action failed, internal server issue. Contact developer", Success = false }) { StatusCode = StatusCodes.Status500InternalServerError};
            }
        }

        [HttpPost("{id}/right")]
        public async Task<IActionResult> SetRightValue([FromRoute] int id, [FromBody] InputObject input)
        {
            try
            {
                // Call the service and return answer
                return new OkObjectResult(await _service.Set(new ComparisonInsertion
                {
                    Id = id,
                    Input = input.Input,
                    Side = SideToSet.Right
                }));

            }
            catch
            {
                return new ObjectResult(new FeedbackObject { Message = "Action failed, internal server issue. Contact developer", Success = false }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComparison([FromRoute] int id)
        {
            try
            {
                // Call the service and return answer
                return new OkObjectResult(await _service.Get(id));
            } 
            catch (ArgumentException ex)
            {
                return new NotFoundObjectResult(new FeedbackObject { Message = ex.Message, Success = false});
            }
            catch
            {
                return new ObjectResult(new FeedbackObject { Message = "Action failed, internal server issue. Contact developer", Success = false }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }
    }
}
