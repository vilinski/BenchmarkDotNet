using System;
using System.Globalization;
using System.Text;

namespace BenchmarkDotNet.Configs
{
    public class FormatStyle : IEquatable<FormatStyle>
    {
        public static readonly CultureInfo DefaultCultureInfo;
        public static readonly Encoding DefaultEncoding = Encoding.ASCII;
        public static readonly FormatStyle DefaultStyle;

        public CultureInfo CultureInfo { get; }
        public Encoding Encoding { get; }

        static FormatStyle()
        {
            DefaultCultureInfo = (CultureInfo) CultureInfo.InvariantCulture.Clone();
            DefaultCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            DefaultStyle = new FormatStyle(DefaultCultureInfo, DefaultEncoding);
        }

        public FormatStyle(CultureInfo cultureInfo, Encoding encoding)
        {
            CultureInfo = cultureInfo;
            Encoding = encoding;
        }

        public FormatStyle(CultureInfo cultureInfo) : this(cultureInfo, DefaultEncoding) { }

        public FormatStyle(Encoding encoding) : this(DefaultCultureInfo, encoding) { }

        public bool Equals(FormatStyle other) => Equals(CultureInfo, other.CultureInfo) && Equals(Encoding, other.Encoding);

        public override bool Equals(object obj) => obj is FormatStyle other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return ((CultureInfo != null ? CultureInfo.GetHashCode() : 0) * 397) ^ (Encoding != null ? Encoding.GetHashCode() : 0);
            }
        }
    }
}