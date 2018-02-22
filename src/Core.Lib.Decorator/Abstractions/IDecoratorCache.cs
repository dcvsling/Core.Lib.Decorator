namespace Core.Lib.Decorator.Abstractions
{
    internal interface IDecoratorCache<T>
        where T :class
    {
        T Value { get; set; }
    }
}
