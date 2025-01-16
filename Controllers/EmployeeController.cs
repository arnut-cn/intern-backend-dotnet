using InternBackendC_.Database;
using Microsoft.AspNetCore.Mvc;

namespace InternBackendC_.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        private readonly AppDbContext _context;

        public EmployeeController(ILogger<EmployeeController> logger, AppDbContext context)
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
