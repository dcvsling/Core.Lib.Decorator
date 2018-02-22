# Decorator of .Net Core DI 

�o�O�H Microsoft.Extensions.DependencyInjection ����¦ DI �e���ҫإߪ��M��

�ت��b��ѨM��غc�l�`�J�ɵL�k��{�`�J�P�ۤv�ۦP��@���O���Ѽ�

�]�ӳy�����H��{�غc�l�`�J�@�k���˹��Ҧ�

## How To Use

```csharp

    var services = new ServiceCollection();

    // ���U��@��H
    var serviceProvider = services.AddDecorator<IWriter>()
    // �̧ǥ[�J��@
        .Add<WriterA>() 
        .Add<WriterB>()
        .Add<WriterC>();

    // ���o��@��H
    var writer = serviceProvider.GetService<IDecorator<IWriter>>().Value;

```

## �˹��Ҧ������O�ҫ� 

�H�U���Ыغc�l�`�J���˹��Ҧ����ҫ��˪O

```csharp

    public interface IWriter
    {
        void Write(string msg);
    }

    public class WriterImpl : IWriter 
    {
        private readonly IWriter _writer;
        public WriterA(IWriter writer)
        {
            this._writer = writer;
        }

        public void Write(string msg) 
        {
            // do something before Write;
            _writer.Write(msg);
            // do something after Write;
        }
    }

```

��h���нЬd�\ Wiki (�ݫػs)

## Rule

��󫬧O�t�ν����ת��Ҷq�A�ҥH�ثe�Ȯɤ�����[�J�ظm�e���ɴ����P�_

�ȥ����lDI�e���غc�P���X�A�Ȯɪ��W�d�P���~�T��

## NuGet Feed

���|��í�w���t�G�A�ҥH�ȮɨS���W���W��NuGet.org�A�p�G�Q�����Ѧ�Package�i�[�J�U�C NuGet Feed Url

https://www.myget.org/F/vsts-tw/api/v3/index.json

## Issue Or Contribute

�Шϥ� Issue �i��^���H�� Push Request �H���ѱz���^�m

## License

MIT