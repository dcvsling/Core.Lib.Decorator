using System.Threading;
using System.Runtime.CompilerServices;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Core.Lib.Decorator.Abstractions;

namespace Core.Lib.Decorator.Internal
{
    internal class DecoratorBuilder<T> : IDecoratorBuilder<T>
        where T : class
    {
        private readonly DecoratorFeature _features;
        private readonly IServiceProvider _provider;

        public DecoratorBuilder(IOptionsSnapshot<DecoratorFeature> features, IServiceProvider provider)
        {
            _features = features.Get(GetKey(typeof(T)));
            _provider = provider;
        }
        
        public T Build()
            => _features.Decorators
                .Select(x => typeof(IDecoratorImpl<,>).MakeGenericType(typeof(T), GetCloseImplType(x)))
                .Select(x => _provider.GetRequiredService(x))
                .Cast<IDecoratorImpl<T>>()
                .Aggregate(new object(), (_, next) => next.Create()) as T;

        private bool IsOpenGenericType(Type implType)
            => implType.IsGenericType && implType.IsGenericTypeDefinition;

        private Type CloseImplType(Type implType)
            => implType.MakeGenericType(typeof(T).GetGenericArguments());

        private Type GetCloseImplType(Type type)
            => IsOpenGenericType(type) ? CloseImplType(type) : type;

        private string GetKey(Type type)
        {
            return type.IsGenericType ? type.GetGenericTypeDefinition().FullName : type.FullName;
        }
    }



    public class DecoratorProvider<T> : IDecorator<T> where T : class
    {
        private readonly IDecoratorBuilder<T> _builder;

        public DecoratorProvider(IDecoratorBuilder<T> builder)
        {
            _builder = builder;
        }

        public T Value => _builder.Build();
    }
}
