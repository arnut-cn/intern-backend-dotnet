namespace InternBackendC_.ViewModels.Employee
{
    public class EmployeeQueryRequest
    {
        public string? text { get; set; }
        public string? positionId { get; set; } 
        public string? teamId { get; set; } 
    }

    public class EmployeeQueryResponse
    {
        public string employeeId { get; set; } = null!;
        public string firstname { get; set; } = null!;
        public string lastname { get; set; } = null!;
        public string email { get; set; } = null!;
        public string dateOfBirth { get; set; } = null!;
        public string positionId { get; set; } = null!;
        public string teamId { get; set; } = null!;
        public List<PhoneModel> phones { get; set; }
    }
}
