using System;

namespace Digihail.Controls
{
    public class ChartUtil
    {
        /// <summary>
        /// 获取基础类型
        /// </summary>
        /// <param name="type">嵌套类型</param>
        /// <returns>基础类型</returns>
        public static Type GetUnderlyingType(Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericArguments()[0];
            }
            else
            {
                return type.GetElementType();
            }
        }
    }
}
