namespace Api.DTOs
{
    public class CFApiResponseDTO<T> where T : class
    {
        public string status { get; set; }
        public List<T> result { get; set; }
    }
}
