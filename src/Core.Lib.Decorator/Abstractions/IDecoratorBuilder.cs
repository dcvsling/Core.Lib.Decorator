namespace Core.Lib.Decorator.Abstractions
{
    public interface IDecoratorBuilder<T> where T : class
    {
        T Build();
    }
}
