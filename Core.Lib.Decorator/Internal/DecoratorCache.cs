using Core.Lib.Decorator.Abstractions;
using System.Collections.Generic;

namespace Core.Lib.Decorator.Internal
{
    internal class DecoratorCache<T> : IDecoratorCache<T> where T : class, IDecorator<T>
    {
        private readonly Stack<IDecorator<T>> _stack;
        public DecoratorCache()
        {
            _stack = new Stack<IDecorator<T>>();
            _stack.Push(default(T));
        }
        public IDecorator<T> Value
        {
            get => _stack.Peek();
            set => _stack.Push(value);
        }
    }
}
