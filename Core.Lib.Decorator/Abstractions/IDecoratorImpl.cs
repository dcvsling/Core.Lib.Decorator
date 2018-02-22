namespace Core.Lib.Decorator.Abstractions
{
    public interface IDecoratorImpl<T> where T : class, IDecorator<T>
    {
        object Create();
    }

    public interface IDecoratorImpl<T, TImpl> : IDecorator<T>, IDecoratorImpl<T>
        where T : class, IDecorator<T>
        where TImpl : class, T
    {
        new TImpl Create();
    }
}
