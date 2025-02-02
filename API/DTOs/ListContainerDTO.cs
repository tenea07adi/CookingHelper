namespace API.DTOs
{
    public class ListContainerDTO<T> where T : class
    {
        public int NextOffset { get; set; }
        public int Count { get; set; }
        public List<T> Records { get; set; }
    }
}
