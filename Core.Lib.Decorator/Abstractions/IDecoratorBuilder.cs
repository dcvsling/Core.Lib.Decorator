namespace Core.Lib.Decorator.Abstractions
{
    public interface IDecoratorBuilder<T> where T : class,IDecorator<T>
    {
        T Build();
    }
}
