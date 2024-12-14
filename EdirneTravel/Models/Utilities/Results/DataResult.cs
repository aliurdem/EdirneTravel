namespace EdirneTravel.Models.Utilities.Results
{
    public class DataResult<T> : AppResult, IDataResult<T>
    {
        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public DataResult(T data, bool success = true) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
