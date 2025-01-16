namespace InternBackendC_.ViewModels.Shared
{
    public class DropdownViewModel<T>
    {
        public T value { get; set; } = default(T);
        public string text { get; set; } = string.Empty;
    }
}
