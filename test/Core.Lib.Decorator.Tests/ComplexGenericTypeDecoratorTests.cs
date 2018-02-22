using Core.Lib.Decorator.Internal;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Core.Lib.Decorator.Abstractions;

namespace Core.Lib.Decorator.Tests
{

    public class ComplexGenericTypeDecoratorTests
    {
        [Fact]
        public void ComplexGenericTypeDecoratorTest()
        {
            var services = new ServiceCollection()
                .AddDecorator(typeof(IWriter<>))
                    .Add(typeof(StringWriter<>), typeof(TypeNameWriter<>), typeof(DatetimeWriter<>))
                .Services
                .AddDecorator(typeof(IWriter<,>))
                    .Add(typeof(RootWriter<,>),typeof(PipeWriter<,>))
                .Services
                .AddSingleton<StringStore>()
                .AddSingleton<DatetimeStore>()
                .AddSingleton<TypeStore>();

            var provider = services.BuildServiceProvider();
            var builder = provider.GetRequiredService<IDecorator<IWriter<TextWriter,IWriter<TextWriter>>>>();
            var nbuilder = provider.GetRequiredService<IDecorator<IWriter<TextWriter>>>();
            var writer = builder.Value;
            var nwriter = nbuilder.Value;
            var textwriter = new StringWriter();

            writer.Write(textwriter,nwriter);
            var expect = $"{new DatetimeStore().Value.ToShortDateString()}{new TypeStore().Value}{new StringStore().Value}";
            Assert.Equal(expect + expect, textwriter.ToString());
        }

        public class StringStore
        {
            public string Value => " is Ok";
        }

        public class DatetimeStore
        {
            public DateTime Value => DateTime.Now.Date;
        }

        public class TypeStore
        {
            public Type Value = typeof(TypeStore);
        }

        public interface IWriter<TWriter>
            where TWriter : TextWriter
        {
            void Write(TWriter writer);
        }

        public interface IWriter<TWriter,TWriterNext>
            where TWriterNext : class,IWriter<TWriter>
            where TWriter : TextWriter
        {
            void Write(TWriter writer,TWriterNext next);
        }

        public class RootWriter<TWriter, TWriterNext> : IWriter<TWriter, TWriterNext>
            where TWriterNext : class, IWriter<TWriter>
            where TWriter : TextWriter
        {
            public void Write(TWriter writer, TWriterNext next)
            {
                next.Write(writer);
            }
        }

        public class PipeWriter<TWriter, TWriterNext> : IWriter<TWriter, TWriterNext>
            where TWriterNext : class, IWriter<TWriter>
            where TWriter : TextWriter
        {
            private readonly IWriter<TWriter, TWriterNext> _writer;

            public PipeWriter(IWriter<TWriter, TWriterNext> writer)
            {
                _writer = writer;
            }
            public void Write(TWriter writer, TWriterNext next)
            {
                next.Write(writer);
                _writer.Write(writer, next);
            }
        }

        public class StringWriter<TWriter> : IWriter<TWriter> where TWriter : TextWriter
        {
            private readonly StringStore _store;

            public StringWriter(StringStore store)
            {
                _store = store;
            }
            public void Write(TWriter writer)
                => writer.Write(_store.Value);
        }

        public class TypeNameWriter<TWriter> : IWriter<TWriter> where TWriter : TextWriter
        {
            private readonly IWriter<TWriter> _writer;
            private readonly TypeStore _store;

            public TypeNameWriter(IWriter<TWriter> writer,TypeStore store)
            {
                _writer = writer;
                _store = store;
            }
            public void Write(TWriter writer)
            {
                writer.Write(_store.Value);
                _writer.Write(writer);
            }
        }

        public class DatetimeWriter<TWriter> : IWriter<TWriter> where TWriter : TextWriter
        {
            private readonly IWriter<TWriter> _writer;
            private readonly DatetimeStore _store;

            public DatetimeWriter(IWriter<TWriter> writer,DatetimeStore store)
            {
                _writer = writer;
                _store = store;
            }
            public void Write(TWriter writer)
            {
                writer.Write(_store.Value.ToShortDateString());
                _writer.Write(writer);
            }
        }
    }
}
