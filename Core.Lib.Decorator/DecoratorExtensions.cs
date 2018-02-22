using System.Linq;
using System;
using System.Collections.Generic;
using Core.Lib.Decorator;
using Core.Lib.Decorator.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DecoratorExtensions
    {
        public static DecoratorBuilder<TService> AddDecorator<TService>(this IServiceCollection services)
            where TService : class
            => services.FindBuilderFromServices<TService>();

        public static DecoratorBuilder AddDecorator(this IServiceCollection services,Type decoratorType)
            => services.FindBuilderFromServices(decoratorType);
        
        private static DecoratorBuilder<TService> FindBuilderFromServices<TService>(this IServiceCollection services)
            where TService : class
            => (services.FirstOrDefault(x => x.ServiceType == typeof(DecoratorBuilder<TService>))
                ?.ImplementationInstance is DecoratorBuilder<TService> builder ? builder : default)
                ?? new DecoratorBuilder<TService>(services).AddDecoratorCore();

        private static DecoratorBuilder FindBuilderFromServices(this IServiceCollection services,Type type)
            => services.Where(x => x.ServiceType == typeof(DecoratorBuilder))
                .Select(x => x.ImplementationInstance)
                .Cast<DecoratorBuilder>()
                .FirstOrDefault(x => x.DecoratorType == type)
                ?? new DecoratorBuilder(type,services).AddDecoratorCore();
    }
}
