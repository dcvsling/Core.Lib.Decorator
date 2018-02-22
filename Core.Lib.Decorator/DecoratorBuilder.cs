using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.Lib.Decorator.Abstractions;
using Core.Lib.Decorator.Internal;

namespace Core.Lib.Decorator
{
    public class DecoratorBuilder
    {
        private DecoratorFeature _feature;
        public DecoratorBuilder(Type type,IServiceCollection services)
        {
            DecoratorType = type;
            Services = services;
            _feature = new DecoratorFeature();
        }

        public Type DecoratorType { get; }
        public IServiceCollection Services { get; }

        public DecoratorBuilder Add(params Type[] types)
        {
            //(types.All(DecoratorType.IsAssignFrom)
            //    ? () => AddDecoratorImpls(types)
            //    : types.GroupBy(DecoratorType.IsAssignFrom)
            //        .Where(x => !x.Key)
            //        .SelectMany(x => x, (_, y) => y.Name)
            //        .JoinBy(",")
            //        .Throw(msg => new ArgumentException(msg)))
            //    .Invoke();
            
            AddDecoratorImpls(types);
            return this;
        }

        internal DecoratorBuilder AddDecoratorCore()
        {
            Services.AddOptions();
            Services.TryAddSingleton(this);
            Services.TryAddSingleton(typeof(IDecoratorImpl<,>), typeof(DecoratorImpl<,>));
            Services.TryAddSingleton(typeof(IDecoratorCache<>), typeof(DecoratorCache<>));
            Services.TryAddSingleton(typeof(IDecoratorBuilder<>), typeof(Internal.DecoratorBuilder<>));
            Services.Configure<DecoratorFeature>(DecoratorType.FullName,o => o.Decorators.AddRange(_feature.Decorators));
            Services.TryAddSingleton(typeof(IDecorator<>),typeof(DecoratorProvider<>));
            return this;
        }

        protected internal void AddDecoratorImpls(IEnumerable<Type> types)
            => _feature.Decorators.AddRange(types);

        protected internal void AddDecoratorImpl(Type type)
            => _feature.Decorators.Add(type);
    }
}
