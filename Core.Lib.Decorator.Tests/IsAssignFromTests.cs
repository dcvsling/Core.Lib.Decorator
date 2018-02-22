using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Core.Lib.Decorator.Abstractions;
using System;

namespace Core.Lib.Decorator.Tests
{
    
    public class IsAssignFromTests
    {
        [Theory]
        [InlineData(typeof(A),typeof(A<A>))]
        [InlineData(typeof(A), typeof(A<A, A>))]
        [InlineData(typeof(A<A>), typeof(A<A, A>))]
        [InlineData(typeof(IA), typeof(IA<A>))]
        [InlineData(typeof(IA), typeof(IA<A, A>))]
        [InlineData(typeof(IA<A>), typeof(IA<A, A>))]
        [InlineData(typeof(IA), typeof(A))]
        [InlineData(typeof(IA<A>), typeof(A<A>))]
        [InlineData(typeof(IA<A,A>), typeof(A<A,A>))]
        [InlineData(typeof(IA), typeof(IB))]
        [InlineData(typeof(IA<A>), typeof(IB<A>))]
        [InlineData(typeof(IA<A,A>), typeof(IB<A, A>))]
        [InlineData(typeof(IA), typeof(IA<>))]
        [InlineData(typeof(IA<>), typeof(IA<, >))]
        [InlineData(typeof(IA), typeof(IA<, >))]
        [InlineData(typeof(IA<>), typeof(IB<>))]
        [InlineData(typeof(IA<, >), typeof(IB<, >))]
        public void test_all_case_isassignfrom_true_case(Type @base,Type given)
        {
            Assert.True(@base.IsAssignFrom(given));
        }

        [Theory]
        [InlineData(typeof(A), typeof(A<>))]
        [InlineData(typeof(A), typeof(A<, >))]
        [InlineData(typeof(A<A>), typeof(A<, >))]
        [InlineData(typeof(IA<A>), typeof(IA))]
        [InlineData(typeof(IA<A, A>), typeof(IA))]
        [InlineData(typeof(IA<A, A>), typeof(IA<A>))]
        [InlineData(typeof(A), typeof(IA))]
        [InlineData(typeof(IA<>), typeof(A<A>))]
        [InlineData(typeof(IA<, >), typeof(A<A, A>))]
        [InlineData(typeof(IB), typeof(IA))]
        [InlineData(typeof(IA<A>), typeof(IB<>))]
        [InlineData(typeof(IA<A, A>), typeof(IB<, >))]
        [InlineData(typeof(IB<>), typeof(IA<>))]
        [InlineData(typeof(IB<,>), typeof(IA<,>))]
        public void test_all_case_isassignfrom_false_case(Type @base, Type given)
        {
            Assert.False(@base.IsAssignFrom(given));
        }

        public class A : IA { }
        public class A<T> : A,IA<T> { }
        public class A<T,T2> : A<T>,IA<T,T2> { }
        public interface IA { }
        public interface IA<T> : IA { }
        public interface IA<T, T2> : IA<T> { }
        public class B : IB { }
        public class B<T> : IB<T> { }
        public class B<T, T2> : IB<T, T2> { }
        public interface IB : IA { }
        public interface IB<T> : IA<T> { }
        public interface IB<T,T2> : IA<T,T2> { }
    }
}
