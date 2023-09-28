namespace CleanArchitecture.WebUI.Utilities
{
    public class Constants
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
        public static string APIUrlBase { get; set; }
    }
}
