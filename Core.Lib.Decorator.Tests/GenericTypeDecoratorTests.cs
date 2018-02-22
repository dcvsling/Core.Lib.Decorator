using Core.Lib.Decorator.Internal;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Core.Lib.Decorator.Abstractions;

namespace Core.Lib.Decorator.Tests
{

    public class GenericTypeDecoratorTests
    {
        [Fact]
        public void SimpleGenericTypeDecoratorTest()
        {
            var services = new ServiceCollection()
                .AddDecorator(typeof(IWriter<>))
                    .Add(typeof(StringWriter<>), typeof(TypeNameWriter<>), typeof(DatetimeWriter<>))
                .Services;

            var provider = services.BuildServiceProvider();
            var builder = provider.GetRequiredService<IDecorator<IWriter<TextWriter>>>();
            var writer = builder.Value;

            var textwriter = new StringWriter();

            writer.Write(textwriter);

            Assert.Equal($"{DateTime.Now.ToShortDateString()}{nameof(TextWriter)} is Ok", textwriter.ToString());
        }

        public interface IWriter<TWriter> where TWriter : TextWriter
        {
            void Write(TWriter writer);
        }

        public class StringWriter<TWriter> : IWriter<TWriter> where TWriter : TextWriter
        {
            public void Write(TWriter writer)
                => writer.Write(" is Ok");
        }

        public class TypeNameWriter<TWriter> : IWriter<TWriter> where TWriter : TextWriter
        {
            private readonly IWriter<TWriter> _writer;

            public TypeNameWriter(IWriter<TWriter> writer)
            {
                _writer = writer;
            }
            public void Write(TWriter writer)
            {
                writer.Write(nameof(TextWriter));
                _writer.Write(writer);
            }
        }

        public class DatetimeWriter<TWriter> : IWriter<TWriter> where TWriter : TextWriter
        {
            private readonly IWriter<TWriter> _writer;

            public DatetimeWriter(IWriter<TWriter> writer)
            {
                _writer = writer;
            }
            public void Write(TWriter writer)
            {
                writer.Write(DateTime.Now.ToShortDateString());
                _writer.Write(writer);
            }
        }
    }
}
