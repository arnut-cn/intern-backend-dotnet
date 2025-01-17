using InternBackendC_.Database;
using InternBackendC_.ViewModels.Shared;
using InternBackendC_.ViewModels.Employee;
using Microsoft.EntityFrameworkCore;

namespace InternBackendC_.BusinessLogics.Position

{
    public class EmployeeBusinessLogic
    {
        private readonly AppDbContext context;
        private readonly ILogger<EmployeeBusinessLogic> _logger;

        public EmployeeBusinessLogic(AppDbContext context, ILogger<EmployeeBusinessLogic> logger)
        {
            this.context = context;
            _logger = logger;
        }

        public async Task<PagedDataResult<EmployeeQueryResponse>> Index(PagedDataQuery<EmployeeQueryRequest> query)
        {
            try
            {
                var iQueryable = context.employees.Where(w => w.is_enable);

                if (query.search != null)
                {
                    var search = query.search;
                    iQueryable = iQueryable.Where(w =>
                    (string.IsNullOrEmpty(search.text)
                    || w.firstname.ToLower().Contains(search.text.ToLower())
                    || w.lastname.ToLower().Contains(search.text.ToLower())
                    || w.email.ToLower().Contains(search.text.ToLower()))
                    && (string.IsNullOrEmpty(search.positionId) || w.position_id == search.positionId)
                    && (string.IsNullOrEmpty(search.teamId) || w.team_id == search.teamId)
                    );
                }

                var rowCount = await iQueryable.CountAsync();

                int num = query.pageIndex * query.pageSize;
                int pageIndex = query.pageIndex;
                if (num < rowCount)
                {
                    iQueryable = iQueryable.Skip(num).Take(query.pageSize);
                }
                else
                {
                    iQueryable = iQueryable.Take(query.pageSize);
                    pageIndex = 0;
                }

                var data = await iQueryable.Select(s => new EmployeeQueryResponse
                {
                    employeeId = s.employee_id,
                    firstname = s.firstname,
                    lastname = s.lastname,
                    dateOfBirth = s.date_of_birth,
                    email = s.email,
                    positionId = s.position_id,
                    teamId = s.team_id,
                    phones = s.phones.Select(s => new PhoneModel
                    {
                        phoneId = s.phone_id,
                        phoneNumber = s.phone_number
                    }).ToList()
                }).ToListAsync();

                return new PagedDataResult<EmployeeQueryResponse>()
                {
                    pageSize = query.pageSize,
                    pageIndex = pageIndex,
                    rowCount = rowCount,
                    data = data
                };
            }
            catch (Exception ex)
            {
                return new PagedDataResult<EmployeeQueryResponse>();
            }
        }

        public async Task<string> Create(EmployeeCreateRequest request)
        {

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {


                var entity = new employee
                {
                    employee_id = Guid.NewGuid().ToString(),
                    firstname = request.firstname,
                    lastname = request.lastname,
                    date_of_birth = request.dateOfBirth,
                    email = request.email,
                    position_id = request.positionId,
                    team_id = request.teamId,
                    is_enable = true,
                };


                await context.employees.AddAsync(entity);
                await context.SaveChangesAsync();

                foreach (var item in request.phones)
                {
                    var _phone = new phone
                    {
                        employee_id = entity.employee_id,
                        phone_id = Guid.NewGuid().ToString(),
                        phone_number = item.phoneNumber
                    };
                    await context.phones.AddAsync(_phone);
                }

                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return entity.employee_id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<string> Update(EmployeeUpdateRequest request)
        {

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var entity = await context.employees.Where(w => w.is_enable && w.employee_id == request.employeeIdId).SingleAsync();

                entity.firstname = request.firstname;
                entity.lastname = request.lastname;
                entity.date_of_birth = request.dateOfBirth;
                entity.email = request.email;
                entity.position_id = request.positionId;
                entity.team_id = request.teamId;

                var oldPhone = await context.phones.Where(w => w.employee_id == entity.employee_id).ToListAsync();
                context.phones.RemoveRange(oldPhone);

                foreach (var item in request.phones)
                {
                    var _phone = new phone
                    {
                        employee_id = entity.employee_id,
                        phone_id = Guid.NewGuid().ToString(),
                        phone_number = item.phoneNumber
                    };
                    await context.phones.AddAsync(_phone);
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return entity.employee_id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<EmployeeGetDetailResponse> GetDetail(string id)
        {
            try
            {
                var model = await context.employees
                    .Where(w => w.is_enable && w.employee_id == id)
                    .Select(s => new EmployeeGetDetailResponse
                    {
                        employeeIdId = s.position_id,
                        firstname = s.firstname,
                        lastname = s.lastname,
                        dateOfBirth = s.date_of_birth,
                        email = s.email,
                        positionId = s.position_id,
                        teamId = s.team_id,
                        phones = s.phones.Select(s => new PhoneModel
                        {
                            phoneId = s.phone_id,
                            phoneNumber = s.phone_number
                        }).ToList()

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
                var entity = await context.employees
                    .Where(w => w.is_enable && w.employee_id == id)
                    .SingleAsync();

                context.employees.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
            }
        }

    }
}
