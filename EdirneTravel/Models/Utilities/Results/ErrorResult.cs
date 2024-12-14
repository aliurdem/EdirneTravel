namespace EdirneTravel.Models.Utilities.Results
{
    public class ErrorResult : AppResult
    {
        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult() : base(false)
        {
        }
    }
}
