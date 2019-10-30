using System;
using System.Globalization;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Horology;
using JetBrains.Annotations;

// ReSharper disable MemberCanBePrivate.Global

namespace BenchmarkDotNet.Reports
{
    public class SummaryStyle : IEquatable<SummaryStyle>
    {
        public static readonly SummaryStyle Default = new SummaryStyle(FormatStyle.DefaultStyle, printUnitsInHeader: false, printUnitsInContent: true, printZeroValuesInContent: false, sizeUnit: null, timeUnit: null);
        internal const int DefaultMaxParameterColumnWidth = 15 + 5; // 5 is for postfix " [15]"

        public bool PrintUnitsInHeader { get; }
        public bool PrintUnitsInContent { get; }
        public bool PrintZeroValuesInContent { get; }
        public int MaxParameterColumnWidth { get; }
        public SizeUnit SizeUnit { get; }
        public TimeUnit TimeUnit { get; }
        [NotNull]
        public FormatStyle FormatStyle { get; }

        [NotNull]
        public CultureInfo CultureInfo => FormatStyle.CultureInfo ?? FormatStyle.DefaultCultureInfo;

        public SummaryStyle(FormatStyle formatStyle, bool printUnitsInHeader, SizeUnit sizeUnit, TimeUnit timeUnit, bool printUnitsInContent = true, bool printZeroValuesInContent = false, int maxParameterColumnWidth = DefaultMaxParameterColumnWidth)
        {
            if (maxParameterColumnWidth < DefaultMaxParameterColumnWidth)
                throw new ArgumentOutOfRangeException(nameof(maxParameterColumnWidth), $"{DefaultMaxParameterColumnWidth} is the minimum.");

            FormatStyle = formatStyle ?? FormatStyle.DefaultStyle;
            PrintUnitsInHeader = printUnitsInHeader;
            PrintUnitsInContent = printUnitsInContent;
            SizeUnit = sizeUnit;
            TimeUnit = timeUnit;
            PrintZeroValuesInContent = printZeroValuesInContent;
            MaxParameterColumnWidth = maxParameterColumnWidth;
        }

        public SummaryStyle WithTimeUnit(TimeUnit timeUnit)
            => new SummaryStyle(FormatStyle, PrintUnitsInHeader, SizeUnit, timeUnit, PrintUnitsInContent, PrintZeroValuesInContent);

        public SummaryStyle WithSizeUnit(SizeUnit sizeUnit)
            => new SummaryStyle(FormatStyle, PrintUnitsInHeader, sizeUnit, TimeUnit, PrintUnitsInContent, PrintZeroValuesInContent);

        public SummaryStyle WithZeroMetricValuesInContent()
            => new SummaryStyle(FormatStyle, PrintUnitsInHeader, SizeUnit, TimeUnit, PrintUnitsInContent, printZeroValuesInContent: true);

        public SummaryStyle WithMaxParameterColumnWidth(int maxParameterColumnWidth)
            => new SummaryStyle(FormatStyle, PrintUnitsInHeader, SizeUnit, TimeUnit, PrintUnitsInContent, PrintZeroValuesInContent, maxParameterColumnWidth);

        public SummaryStyle WithFormatStyle(FormatStyle formatStyle)
            => new SummaryStyle(formatStyle, PrintUnitsInHeader, SizeUnit, TimeUnit, PrintUnitsInContent, PrintZeroValuesInContent, MaxParameterColumnWidth);
        
        public bool Equals(SummaryStyle other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return FormatStyle == other.FormatStyle 
                && PrintUnitsInHeader == other.PrintUnitsInHeader
                && PrintUnitsInContent == other.PrintUnitsInContent
                && PrintZeroValuesInContent == other.PrintZeroValuesInContent
                && Equals(SizeUnit, other.SizeUnit)
                && Equals(TimeUnit, other.TimeUnit)
                && MaxParameterColumnWidth == other.MaxParameterColumnWidth;
        }

        public override bool Equals(object obj) => obj is SummaryStyle summary && Equals(summary);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = PrintUnitsInHeader.GetHashCode();
                hashCode = (hashCode * 397) ^ PrintUnitsInContent.GetHashCode();
                hashCode = (hashCode * 397) ^ PrintZeroValuesInContent.GetHashCode();
                hashCode = (hashCode * 397) ^ (SizeUnit != null ? SizeUnit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TimeUnit != null ? TimeUnit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ MaxParameterColumnWidth;
                return hashCode;
            }
        }

        public static bool operator ==(SummaryStyle left, SummaryStyle right) => Equals(left, right);

        public static bool operator !=(SummaryStyle left, SummaryStyle right) => !Equals(left, right);
    }
}
