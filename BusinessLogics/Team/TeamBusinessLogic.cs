using InternBackendC_.Database;
using InternBackendC_.ViewModels.Shared;
using InternBackendC_.ViewModels.Team;
using Microsoft.EntityFrameworkCore;

namespace InternBackendC_.BusinessLogics.Team
{
    public class TeamBusinessLogic
    {
        private readonly AppDbContext context;
        private readonly ILogger<TeamBusinessLogic> _logger;

        public TeamBusinessLogic(AppDbContext context, ILogger<TeamBusinessLogic> logger)
        {
            this.context = context;
            _logger = logger;
        }

        public async Task<PagedDataResult<TeamQueryResponse>> Index(PagedDataQuery<TeamQueryRequest> query)
        {
            try
            {
                var iQueryable = context.teams.Where(w => w.is_enable);

                if (query.search != null)
                {
                    var search = query.search;
                    iQueryable = iQueryable.Where(w => string.IsNullOrEmpty(search.text) || w.name.ToLower().Contains(search.text.ToLower()));
                }

                var rowCount = await iQueryable.CountAsync();

                int num = query.pageIndex * query.pageSize;
                if (num > 0)
                    iQueryable = iQueryable.Skip(num).Take(query.pageSize);

                var data = await iQueryable.Select(s => new TeamQueryResponse
                {
                    teamId = s.team_id,
                    name = s.name,
                    description = s.description,
                }).ToListAsync();

                return new PagedDataResult<TeamQueryResponse>()
                {
                    pageSize = query.pageSize,
                    pageIndex = query.pageIndex,
                    rowCount = rowCount,
                    data = data
                };
            }
            catch (Exception ex)
            {
                return new PagedDataResult<TeamQueryResponse>();
            }
        }

        public async Task<string> Create(TeamCreateRequest request)
        {
            try
            {
                var entity = new team
                {
                    team_id = Guid.NewGuid().ToString(),
                    name = request.name,
                    description = request.description,
                    is_enable = true,
                };

                await context.teams.AddAsync(entity);
                await context.SaveChangesAsync();

                return entity.team_id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> Update(TeamUpdateRequest request)
        {
            try
            {
                var entity = await context.teams.Where(w => w.is_enable && w.team_id == request.teamId).SingleAsync();

                entity.name = request.name;
                entity.description = request.description;

                await context.SaveChangesAsync();

                return entity.team_id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TeamGetDetailResponse> GetDetail(string id)
        {
            try
            {
                var model = await context.teams
                    .Where(w => w.is_enable && w.team_id == id)
                    .Select(s => new TeamGetDetailResponse
                    {
                        teamId = s.team_id,
                        name = s.name,
                        description = s.description,

                    })
                    .SingleAsync();

                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var entity = await context.teams
                    .Where(w => w.is_enable && w.team_id == id)
                    .SingleAsync();

                context.teams.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
            }
        }

        public async Task<List<DropdownViewModel<string>>> GetList()
        {
            try
            {
                var list = await context.teams
                    .Where(w => w.is_enable)
                    .Select(s => new DropdownViewModel<string>
                    {
                        value = s.team_id,
                        text = s.name
                    })
                    .ToListAsync();

                return list;

            }
            catch (Exception ex)
            {
                return new List<DropdownViewModel<string>>();
            }
        }
    }
}
