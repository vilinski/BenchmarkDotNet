using System.Globalization;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace BenchmarkDotNet.Samples
{
    [Config(typeof(Config))]
    [ShortRunJob]
    public class IntroCultureInfo
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                var cultureInfo = (CultureInfo) CultureInfo.InvariantCulture.Clone();
                cultureInfo.NumberFormat.NumberDecimalSeparator = "@";
                FormatStyle = new FormatStyle(cultureInfo);
            }
        }
        
        [Benchmark]
        public void Foo() => Thread.Sleep(100);
    }
}