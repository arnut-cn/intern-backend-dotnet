namespace InternBackendC_.ViewModels.Position
{
    public class PositionCreateRequest
    {
        public string name { get; set; } = null!;
        public string? description { get; set; }

    }

    public class PositionUpdateRequest : PositionCreateRequest
    {
        public string positionId { get; set; } = null!;
    }
}
