namespace InternBackendC_.ViewModels.Team
{
    public class TeamQueryRequest
    {
        public string? text { get; set; }
    }

    public class TeamQueryResponse
    {
        public string teamId { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? description { get; set; }
    }
}
