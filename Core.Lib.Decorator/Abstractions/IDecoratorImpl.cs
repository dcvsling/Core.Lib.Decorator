namespace Core.Lib.Decorator.Abstractions
{
    public interface IDecoratorImpl<T> where T : class
    {
        object Create();
    }

    public interface IDecoratorImpl<T, TImpl> : IDecoratorImpl<T>
        where T : class
        where TImpl : class, T
    {
        new TImpl Create();
    }
}
