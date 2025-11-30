using Refit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RefitDemo.Common.Services
{
    /// <summary>
    /// 实现自定义日期格式化器
    /// </summary>
    public class CustomDateUrlParameterFormatter : IUrlParameterFormatter
    {
        public string Format(object value, ICustomAttributeProvider attributeProvider, Type type)
        {
            if (value is DateTime dt)
            {
                return dt.ToString("yyyyMMdd"); // 格式化为 yyyyMMdd
            }
            return value?.ToString();
        }
    }
}
