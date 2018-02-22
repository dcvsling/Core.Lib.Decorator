using System;
using Core.Lib.Decorator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Lib.Decorator.Internal
{
    internal class DecoratorImpl<T, TImpl> : IDecoratorImpl<T, TImpl>
        where T : class, IDecorator<T>
        where TImpl : class, T
    {
        private readonly IServiceProvider _provider;
        private readonly IDecoratorCache<T> _cache;

        public DecoratorImpl(IServiceProvider provider, IDecoratorCache<T> cache)
        {
            _provider = provider;
            _cache = cache;
        }
        public TImpl Create()
        {
            var result = CreateService(_cache.Value);
            _cache.Value = result;
            return result;
        }

        private TImpl CreateService(IDecorator<T> decorator)
            => decorator is T service
                ? CreateByService(service)
                : CreateRoot();
        private TImpl CreateByService(T service)
            => ActivatorUtilities.CreateInstance<TImpl>(_provider, service);

        private TImpl CreateRoot()
            => ActivatorUtilities.CreateInstance<TImpl>(_provider);
        object IDecoratorImpl<T>.Create() => Create();
    }
}
