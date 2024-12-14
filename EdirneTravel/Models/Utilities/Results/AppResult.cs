namespace EdirneTravel.Models.Utilities.Results
{
    public class AppResult : IAppResult
    {
        public AppResult(bool success, string message) : this(success)
        {
            Message = message;
        }

        public AppResult(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}
