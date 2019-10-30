using System.Globalization;
using System.Text;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Reports;
using Xunit;

namespace BenchmarkDotNet.Tests
{
    public class SummaryStyleTests
    {
        [Fact]
        public void UserCanDefineCustomSummaryStyle()
        {
            var summaryStyle = new SummaryStyle
            (
                formatStyle: new FormatStyle(CultureInfo.InvariantCulture, Encoding.UTF7), 
                printUnitsInHeader: true,
                printUnitsInContent: false,
                printZeroValuesInContent: true,
                sizeUnit: SizeUnit.B,
                timeUnit: TimeUnit.Millisecond
            );

            var config = ManualConfig.CreateEmpty().With(summaryStyle);

            Assert.Equal(CultureInfo.InvariantCulture, config.SummaryStyle.FormatStyle.CultureInfo);
            Assert.Equal(Encoding.UTF7, config.SummaryStyle.FormatStyle.Encoding);
            Assert.True(config.SummaryStyle.PrintUnitsInHeader);
            Assert.False(config.SummaryStyle.PrintUnitsInContent);
            Assert.True(config.SummaryStyle.PrintZeroValuesInContent);
            Assert.Equal(SizeUnit.B, config.SummaryStyle.SizeUnit);
            Assert.Equal(TimeUnit.Millisecond, config.SummaryStyle.TimeUnit);
        }
    }
}