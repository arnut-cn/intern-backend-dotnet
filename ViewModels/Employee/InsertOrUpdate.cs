namespace InternBackendC_.ViewModels.Employee
{
    public class EmployeeCreateRequest
    {
        public string firstname { get; set; } = null!;
        public string lastname { get; set; } = null!;
        public string email { get; set; } = null!;
        public string dateOfBirth { get; set; } = null!;
        public string positionId { get; set; } = null!;
        public string teamId { get; set; } = null!;
        public List<PhoneModel> phones { get; set; }

    }

    public class PhoneModel
    {
        public string phoneId { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;

    }

    public class EmployeeUpdateRequest : EmployeeCreateRequest
    {
        public string employeeIdId { get; set; } = null!;
    }
}
