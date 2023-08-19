using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Common
{
    public static class DateHelper
    {
        /// <summary>
        /// 获取时间跨度xx月xx天xx小时xx分钟xx秒
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static string GetDateLength(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return "0秒";
            }

            TimeSpan duration = endDate.Value - startDate.Value;

            int months = duration.Days / 30;
            int days = duration.Days % 30;
            int hours = duration.Hours;
            int minutes = duration.Minutes;
            int seconds = duration.Seconds;

            var dateStr = new StringBuilder();

            if (months > 0)
            {
                dateStr.Append($"{months}个月");
            }
            if (days > 0)
            {
                dateStr.Append($"{days}天");
            }
            if (hours > 0)
            {
                dateStr.Append($"{hours}小时");
            }
            if (minutes > 0)
            {
                dateStr.Append($"{minutes}分钟");
            }
            if (seconds > 0)
            {
                dateStr.Append($"{seconds}秒");
            }
            return dateStr.ToString();
        }

        /// <summary>
        /// 获取时间跨度单位  0全部 1今日 2本周 3本月 4全年
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetDateRangeUnit(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(1);
            if (startDate == null || endDate == null)
            {
                return 0;
            }

            TimeSpan duration = endDate - startDate;

            int years = duration.Days / 365;
            int months = (duration.Days % 365) / 30;
            int weeks = ((duration.Days % 365) % 30) / 7;
            int days = ((duration.Days % 365) % 30) % 7;

            if (years > 0)
            {
                return 4;
            }
            else if (months > 0)
            {
                return 3;
            }
            else if (weeks > 0 || days > 1)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}
