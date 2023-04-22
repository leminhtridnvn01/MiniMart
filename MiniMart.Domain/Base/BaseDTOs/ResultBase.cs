using System.Text.Json.Serialization;

namespace MiniMart.Domain.Base.BaseDTOs
{
    public class ResultBase : IResultBase
    {
        public ResultBase()
        {
        }

        public ResultBase(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        [JsonIgnore]
        public bool IsSuccess { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        public static ResultBase OK(string message = default)
        {
            return new ResultBase(true, message);
        }

        public static ResultBase<T> OK<T>(T data, string message = default)
        {
            return new ResultBase<T>(true, message, data);
        }

        public static ResultBase Error(string message)
        {
            return new ResultBase(false, message);
        }

        public static ResultBase<T> Error<T>(string message, T data = default)
        {
            return new ResultBase<T>(false, message, data);
        }
    }

    public class ResultBase<T> : ResultBase, IResultBase<T>
    {
        public ResultBase()
        {
        }

        public ResultBase(bool isSuccess, string message, T data)
            : base(isSuccess, message)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
