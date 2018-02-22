# Decorator of .Net Core DI 

這是以 Microsoft.Extensions.DependencyInjection 為基礎 DI 容器所建立的專案

目的在於解決其建構子注入時無法實現注入與自己相同實作型別的參數

因而造成難以實現建構子注入作法的裝飾模式

## How To Use

```csharp

    var services = new ServiceCollection();

    // 註冊實作對象
    var serviceProvider = services.AddDecorator<IWriter>()
    // 依序加入實作
        .Add<WriterA>() 
        .Add<WriterB>()
        .Add<WriterC>();

    // 取得實作對象
    var writer = serviceProvider.GetService<IDecorator<IWriter>>().Value;

```

## 裝飾模式的類別模型 

以下介紹建構子注入的裝飾模式的模型樣板

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

更多介紹請查閱 Wiki (待建製)

## Rule

基於型別系統複雜度的考量，所以目前暫時不打算加入建置容器時期的判斷

僅仰賴原始DI容器建構與產出服務時的規範與錯誤訊息

## NuGet Feed

基於尚未穩定的緣故，所以暫時沒有規劃上到NuGet.org，如果想直接參考Package可加入下列 NuGet Feed Url

https://www.myget.org/F/vsts-tw/api/v3/index.json

## Issue Or Contribute

請使用 Issue 進行回報以及 Push Request 以提供您的貢獻

## License

MIT