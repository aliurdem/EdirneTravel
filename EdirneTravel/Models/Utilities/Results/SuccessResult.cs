namespace EdirneTravel.Models.Utilities.Results
{
    public class SuccessResult : AppResult
    {
        public SuccessResult(string message) : base(true, message)
        {
        }

        public SuccessResult() : base(true)
        {
        }
    }
}
