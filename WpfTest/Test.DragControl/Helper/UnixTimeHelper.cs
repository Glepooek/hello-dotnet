using System;

namespace Test.DragControl.Helper
{
    /// <summary>
    /// Unix时间戳转换
    /// </summary>
    public static class UnixTimeHelper
    {
        /// <summary>
        /// DateTime 转换为 Timestamp（ms）
        /// </summary>
        /// <param name="dateTime">DataTime</param>
        /// <returns>Timestamp（ms）</returns>
        public static long ToUnixTime(this DateTime dateTime)
        {
            return (dateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        /// <summary>
        /// Timestamp（ms）转换为 DataTime
        /// </summary>
        /// <param name="unixTime">Timestamp（ms）</param>
        /// <returns>DataTime</returns>
        public static DateTime ToDateTime(this long unixTime)
        {
            return new DateTime(unixTime * 10000 + 621355968000000000, DateTimeKind.Local).AddHours(8);
        }
    }
}
