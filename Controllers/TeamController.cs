using InternBackendC_.BusinessLogics.Team;
using InternBackendC_.Database;
using InternBackendC_.ViewModels.Shared;
using InternBackendC_.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;

namespace InternBackendC_.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        private readonly TeamBusinessLogic teamBusinessLogic;

        public TeamController(ILogger<TeamController> logger, TeamBusinessLogic teamBusinessLogic)
        {
            _logger = logger;
            this.teamBusinessLogic = teamBusinessLogic;
        }

        [HttpPost]
        public async Task<PagedDataResult<TeamQueryResponse>> Index([FromBody] PagedDataQuery<TeamQueryRequest> req)
        {
            return await teamBusinessLogic.Index(req);
        }

        [HttpPost]
        public async Task<string> Create([FromBody] CreateRequest req)
        {
            return await teamBusinessLogic.Create(req);
        }

        [HttpPost]
        public async Task<string> Update([FromBody] UpdateRequest req)
        {
            return await teamBusinessLogic.Update(req);
        }

        [HttpGet]
        public async Task<GetDetailResponse> GetDetail([FromQuery] string id)
        {
            return await teamBusinessLogic.GetDetail(id);
        }

        [HttpPost]
        public async Task Delete([FromBody] string id)
        {
            await teamBusinessLogic.Delete(id);
        }

        [HttpGet]
        public async Task<List<DropdownViewModel<string>>> GetTeamDropdown()
        {
            return await teamBusinessLogic.GetList();
        }
    }
}
