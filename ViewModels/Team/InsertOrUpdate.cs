namespace InternBackendC_.ViewModels.Team
{
    public class CreateRequest
    {
        public string name { get; set; } = null!;
        public string? description { get; set; }

    }

    public class UpdateRequest : CreateRequest
    {
        public string teamId { get; set; } = null!;
    }
}
