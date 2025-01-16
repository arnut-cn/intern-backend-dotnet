namespace InternBackendC_.ViewModels.Team
{
    public class TeamCreateRequest
    {
        public string name { get; set; } = null!;
        public string? description { get; set; }

    }

    public class TeamUpdateRequest : TeamCreateRequest
    {
        public string teamId { get; set; } = null!;
    }
}
