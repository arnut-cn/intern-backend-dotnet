using InternBackendC_.BusinessLogics.Position;
using InternBackendC_.ViewModels.Shared;
using InternBackendC_.ViewModels.Position;
using Microsoft.AspNetCore.Mvc;

namespace InternBackendC_.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PositionController : ControllerBase
    {
        private readonly ILogger<PositionController> _logger;

        private readonly PositionBusinessLogic positionBusinessLogic;

        public PositionController(ILogger<PositionController> logger, PositionBusinessLogic positionBusinessLogic)
        {
            _logger = logger;
            this.positionBusinessLogic = positionBusinessLogic;
        }

        [HttpPost]
        public async Task<PagedDataResult<PositionQueryResponse>> Index([FromBody] PagedDataQuery<PositionQueryRequest> req)
        {
            return await positionBusinessLogic.Index(req);
        }

        [HttpPost]
        public async Task<string> Create([FromBody] PositionCreateRequest req)
        {
            return await positionBusinessLogic.Create(req);
        }

        [HttpPost]
        public async Task<string> Update([FromBody] PositionUpdateRequest req)
        {
            return await positionBusinessLogic.Update(req);
        }

        [HttpGet]
        public async Task<PositionGetDetailResponse> GetDetail([FromQuery] string id)
        {
            return await positionBusinessLogic.GetDetail(id);
        }

        [HttpPost]
        public async Task Delete([FromBody] string id)
        {
            await positionBusinessLogic.Delete(id);
        }

        [HttpGet]
        public async Task<List<DropdownViewModel<string>>> GetPositionDropdown()
        {
            return await positionBusinessLogic.GetList();
        }
    }
}
