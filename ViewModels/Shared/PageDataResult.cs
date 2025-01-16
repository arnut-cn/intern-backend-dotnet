namespace InternBackendC_.ViewModels.Shared
{
    public class PagedDataQuery<T>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public T? search { get; set; }
    }

    public abstract class PagedDataResultBase
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int rowCount { get; set; }
    }

    public class PagedDataResult<T> : PagedDataResultBase where T : class
    {
        public IList<T> data { get; set; }
        public PagedDataResult()
        {
            data = new List<T>();
        }
    }
}
