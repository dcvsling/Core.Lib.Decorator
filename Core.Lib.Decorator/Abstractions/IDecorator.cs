namespace Core.Lib.Decorator.Abstractions
{
    public interface IDecorator<T> where T : class, IDecorator<T>
    {
    }
}
