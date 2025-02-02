
namespace Core.Entities.Transfer
{
    public class PaginationParameters
    {
        public int Offset { get; set; }
        public int Maxsize { get; set; }
        public string OrderBy { get; set; } = String.Empty;
    }
}
