using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Core.Lib.Decorator.Abstractions;

namespace Core.Lib.Decorator.Tests
{

    public class DecoratorIntergrateTests
    {
        [Fact]
        public void common_use_case()
        {
            var services = new ServiceCollection()
                .AddDecorator<IWriter>()
                    .Add(typeof(WriterA), typeof(WriterB), typeof(WriterC))
                .Services;

            var provider = services.BuildServiceProvider();
            var writer = provider.GetRequiredService<IWriter>();
            var textwriter = new StringWriter();

            writer.Write(textwriter);

            Assert.Equal(nameof(WriterC) + nameof(WriterB) + nameof(WriterA), textwriter.ToString());
        }


        public interface IWriter : IDecorator<IWriter>
        {
            void Write(TextWriter writer);
        }

        public class WriterA : IWriter
        {
            public void Write(TextWriter writer) => writer.Write(nameof(WriterA));
        }

        public class WriterB : IWriter
        {
            private readonly IWriter _writer;

            public WriterB(IWriter writer)
            {
                _writer = writer;
            }
            public void Write(TextWriter writer)
            {
                writer.Write(nameof(WriterB));
                _writer.Write(writer);
            }
        }
        public class WriterC : IWriter
        {
            private readonly IWriter _writer;

            public WriterC(IWriter writer)
            {
                _writer = writer;
            }
            public void Write(TextWriter writer)
            {
                writer.Write(nameof(WriterC));
                _writer.Write(writer);
            }
        }
    }
}
