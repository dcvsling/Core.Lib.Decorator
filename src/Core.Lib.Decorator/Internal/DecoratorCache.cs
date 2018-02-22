using Core.Lib.Decorator.Abstractions;
using System.Collections.Generic;

namespace Core.Lib.Decorator.Internal
{
    internal class DecoratorCache<T> : IDecoratorCache<T> where T : class
    {
        private readonly Stack<T> _stack;
        public DecoratorCache()
        {
            _stack = new Stack<T>();
            _stack.Push(default(T));
        }
        public T Value
        {
            get => _stack.Peek();
            set => _stack.Push(value);
        }
    }
}
