using System.Text;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Mathematics.Histograms;
using JetBrains.Annotations;

namespace BenchmarkDotNet.Extensions
{
    public static class StatisticsExtensions
    {
        private const string NullSummaryMessage = "<Empty statistic (N=0)>";

        [PublicAPI]
        public static string ToTimeStr(this Statistics s, FormatStyle formatStyle, TimeUnit unit = null, bool calcHistogram = false)
        {
            if (s == null)
                return NullSummaryMessage;
            if (unit == null)
                unit = TimeUnit.GetBestTimeUnit(s.Mean);
            var builder = new StringBuilder();
            string errorPercent = (s.StandardError / s.Mean * 100).ToStr(formatStyle, "0.00");
            var ci = s.ConfidenceInterval;
            string ciMarginPercent = (ci.Margin / s.Mean * 100).ToStr(formatStyle, "0.00");
            double mValue = MathHelper.CalculateMValue(s);
            builder.AppendLine($"Mean = {s.Mean.ToTimeStr(unit, formatStyle)}, StdErr = {s.StandardError.ToTimeStr(unit, formatStyle)} ({errorPercent}%); N = {s.N}, StdDev = {s.StandardDeviation.ToTimeStr(unit, formatStyle)}");
            builder.AppendLine($"Min = {s.Min.ToTimeStr(unit, formatStyle)}, Q1 = {s.Q1.ToTimeStr(unit, formatStyle)}, Median = {s.Median.ToTimeStr(unit, formatStyle)}, Q3 = {s.Q3.ToTimeStr(unit, formatStyle)}, Max = {s.Max.ToTimeStr(unit, formatStyle)}");
            builder.AppendLine($"IQR = {s.InterquartileRange.ToTimeStr(unit, formatStyle)}, LowerFence = {s.LowerFence.ToTimeStr(unit, formatStyle)}, UpperFence = {s.UpperFence.ToTimeStr(unit, formatStyle)}");
            builder.AppendLine($"ConfidenceInterval = {s.ConfidenceInterval.ToTimeStr(formatStyle, unit)}, Margin = {ci.Margin.ToTimeStr(unit, formatStyle)} ({ciMarginPercent}% of Mean)");
            builder.AppendLine($"Skewness = {s.Skewness.ToStr(formatStyle)}, Kurtosis = {s.Kurtosis.ToStr(formatStyle)}, MValue = {mValue.ToStr(formatStyle)}");
            if (calcHistogram)
            {
                var histogram = HistogramBuilder.Adaptive.Build(s);
                builder.AppendLine("-------------------- Histogram --------------------");
                builder.AppendLine(histogram.ToTimeStr(formatStyle: formatStyle));
                builder.AppendLine("---------------------------------------------------");
            }
            return builder.ToString().Trim();
        }
    }
}