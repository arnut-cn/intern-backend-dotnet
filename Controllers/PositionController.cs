using InternBackendC_.Database;
using Microsoft.AspNetCore.Mvc;

namespace InternBackendC_.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PositionController : ControllerBase
    {
        private readonly ILogger<PositionController> _logger;

        private readonly AppDbContext _context;

        public PositionController(ILogger<PositionController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<employee> Index()
        {
            return _context.employees.Where(x => x.is_enable == 1).ToList();
        }
    }
}
