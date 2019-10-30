using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Extensions;

namespace BenchmarkDotNet.Mathematics.StatisticalTesting
{
    public class AbsoluteThreshold : Threshold
    {
        private readonly double value;

        public AbsoluteThreshold(double value)
        {
            this.value = value;
        }

        public override double GetValue(Statistics x) => value;

        public override bool IsZero() => Math.Abs(value) < 1e-9;
        public override string ToString() => value.ToStr(FormatStyle.DefaultStyle, format: "0.##");
    }
}