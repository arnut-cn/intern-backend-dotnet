using InternBackendC_.Database;
using InternBackendC_.ViewModels.Shared;
using InternBackendC_.ViewModels.Position;
using Microsoft.EntityFrameworkCore;

namespace InternBackendC_.BusinessLogics.Position

{
    public class PositionBusinessLogic
    {
        private readonly AppDbContext context;
        private readonly ILogger<PositionBusinessLogic> _logger;

        public PositionBusinessLogic(AppDbContext context, ILogger<PositionBusinessLogic> logger)
        {
            this.context = context;
            _logger = logger;
        }

        public async Task<PagedDataResult<PositionQueryResponse>> Index(PagedDataQuery<PositionQueryRequest> query)
        {
            try
            {
                var iQueryable = context.positions.Where(w => w.is_enable);

                if (query.search != null)
                {
                    var search = query.search;
                    iQueryable = iQueryable.Where(w => string.IsNullOrEmpty(search.text) || w.name.ToLower().Contains(search.text.ToLower()));
                }

                var rowCount = await iQueryable.CountAsync();

                int num = query.pageIndex * query.pageSize;
                if (num > 0)
                    iQueryable = iQueryable.Skip(num).Take(query.pageSize);

                var data = await iQueryable.Select(s => new PositionQueryResponse
                {
                    positionId = s.position_id,
                    name = s.name,
                    description = s.description,
                }).ToListAsync();

                return new PagedDataResult<PositionQueryResponse>()
                {
                    pageSize = query.pageSize,
                    pageIndex = query.pageIndex,
                    rowCount = rowCount,
                    data = data
                };
            }
            catch (Exception ex)
            {
                return new PagedDataResult<PositionQueryResponse>();
            }
        }

        public async Task<string> Create(PositionCreateRequest request)
        {
            try
            {
                var entity = new position
                {
                    position_id = new Guid().ToString(),
                    name = request.name,
                    description = request.description,
                    is_enable = true,
                };

                await context.positions.AddAsync(entity);
                await context.SaveChangesAsync();

                return entity.position_id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> Update(PositionUpdateRequest request)
        {
            try
            {
                var entity = await context.positions.Where(w => w.is_enable && w.position_id == request.positionId).SingleAsync();

                entity.name = request.name;
                entity.description = request.description;

                await context.SaveChangesAsync();

                return entity.position_id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PositionGetDetailResponse> GetDetail(string id)
        {
            try
            {
                var model = await context.positions
                    .Where(w => w.is_enable && w.position_id == id)
                    .Select(s => new PositionGetDetailResponse
                    {
                        positionId = s.position_id,
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
                var entity = await context.positions
                    .Where(w => w.is_enable && w.position_id == id)
                    .SingleAsync();

                context.positions.Remove(entity);
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
                var list = await context.positions
                    .Where(w => w.is_enable)
                    .Select(s => new DropdownViewModel<string>
                    {
                        value = s.position_id,
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
