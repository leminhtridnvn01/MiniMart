namespace MiniMart.Domain.Base.BaseDTOs
{
    public interface IResultBase
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }

    public interface IResultBase<T> : IResultBase
    {
        T Data { get; set; }
    }
}
