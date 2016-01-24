using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevExpress.SalesDemo.Model {
    public class SalesForecastMaker {
        public static decimal GetForecast(decimal currentSales, decimal elapsedPeriodPart) {
            return currentSales / elapsedPeriodPart;
        }
        public static decimal GetYtdForecast(decimal currentSales) {
            DateTimeRange ytdRange = DateTimeUtils.GetYtdRange();
            decimal wholeIntervalLenth = ytdRange.End.Ticks - ytdRange.Start.Ticks;
            decimal elapsedTimeIntervalLenth = DateTime.Today.Ticks - ytdRange.Start.Ticks;
            decimal elapsedPeriodPart = elapsedTimeIntervalLenth / wholeIntervalLenth;
            return GetForecast(currentSales, elapsedPeriodPart);
        }
    }
}
