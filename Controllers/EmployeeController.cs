using InternBackendC_.BusinessLogics.Position;
using InternBackendC_.ViewModels.Shared;
using InternBackendC_.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace InternBackendC_.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        private readonly EmployeeBusinessLogic positionBusinessLogic;

        public EmployeeController(ILogger<EmployeeController> logger, EmployeeBusinessLogic positionBusinessLogic)
        {
            _logger = logger;
            this.positionBusinessLogic = positionBusinessLogic;
        }

        [HttpPost]
        public async Task<PagedDataResult<EmployeeQueryResponse>> Index([FromBody] PagedDataQuery<EmployeeQueryRequest> req)
        {
            return await positionBusinessLogic.Index(req);
        }

        [HttpPost]
        public async Task<string> Create([FromBody] EmployeeCreateRequest req)
        {
            return await positionBusinessLogic.Create(req);
        }

        [HttpPost]
        public async Task<string> Update([FromBody] EmployeeUpdateRequest req)
        {
            return await positionBusinessLogic.Update(req);
        }

        [HttpGet]
        public async Task<EmployeeGetDetailResponse> GetDetail([FromQuery] string id)
        {
            return await positionBusinessLogic.GetDetail(id);
        }

        [HttpPost]
        public async Task Delete([FromBody] string id)
        {
            await positionBusinessLogic.Delete(id);
        }
    }
}
