namespace Core.Lib.Decorator.Abstractions
{
    internal interface IDecoratorCache<T> where T : class, IDecorator<T>
    {
        IDecorator<T> Value { get; set; }
    }
}
