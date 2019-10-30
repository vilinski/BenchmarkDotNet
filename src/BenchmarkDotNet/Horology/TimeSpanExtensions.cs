using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Extensions;

namespace BenchmarkDotNet.Horology
{
    internal static class TimeSpanExtensions
    {
        /// <summary>
        /// Time in the following format: {th}:{mm}:{ss} ({ts} sec)
        ///
        /// where
        ///   {th}: total hours (two digits)
        ///   {mm}: minutes (two digits)
        ///   {ss}: seconds (two digits)
        ///   {ts}: total seconds
        /// </summary>
        /// <example>TimeSpan.FromSeconds(2362) -> "00:39:22 (2362 sec)"</example>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToFormattedTotalTime(this TimeSpan time, FormatStyle formatStyle)
        {
            long totalHours = time.Ticks / TimeSpan.TicksPerHour;
            string hhMmSs = $"{totalHours:00}:{time:mm\\:ss}";
            string totalSecs = $"{time.TotalSeconds.ToStr(formatStyle)} sec";
            return $"{hhMmSs} ({totalSecs})";
        }
    }
}