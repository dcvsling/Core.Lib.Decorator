using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Lib.Decorator.Abstractions;

namespace Core.Lib.Decorator
{
    public class DecoratorBuilder<TService> : DecoratorBuilder where TService : class
    {
        public DecoratorBuilder(IServiceCollection services) : base(typeof(TService),services)
        {
        }

        public DecoratorBuilder<TService> Add<TImpl>()
            where TImpl : class, TService
        {
            AddDecoratorImpl(typeof(TImpl));
            return this;
        }

        public new DecoratorBuilder<TService> Add(params Type[] types)
        {
            base.Add(types);
            return this;
        }

        public new DecoratorBuilder<TService> AddDecoratorCore()
        {
            base.AddDecoratorCore();
            return this;
        }
    }
}
