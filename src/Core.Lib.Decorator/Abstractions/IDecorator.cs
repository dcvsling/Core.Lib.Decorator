namespace Core.Lib.Decorator.Abstractions
{
    public interface IDecorator<T> where T : class
    {
        T Value { get; }
    }
}
