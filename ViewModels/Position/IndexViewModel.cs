namespace InternBackendC_.ViewModels.Position
{
    public class PositionQueryRequest
    {
        public string? text { get; set; }
    }

    public class PositionQueryResponse
    {
        public string positionId { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? description { get; set; }
    }
}
