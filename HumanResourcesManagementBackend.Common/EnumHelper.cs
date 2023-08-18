using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HumanResourcesManagementBackend.Models.Data;
namespace HumanResourcesManagementBackend.Common
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 枚举文本池
        /// </summary>
        private static Dictionary<string, string> description = new Dictionary<string, string>();

        private static string SetEnumDescription(Enum e)
        {
            var t = e.GetType();
            var name = Enum.GetName(t, e);
            FieldInfo fieldInfo = t.GetField(name);
            if (fieldInfo != null)
            {
                // 获取描述的属性。
                DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                    typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null)
                {
                    string dictKey = string.Format("{0}.{1}", t.Name, name);
                    if (!description.ContainsKey(dictKey))
                    {
                        description.Add(dictKey, attr.Description);
                        return attr.Description;
                    }
                }
            }
            return name;
        }

        public static List<Enumber> EnumToList<T>()
        {
            List<Enumber> list = new List<Enumber>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                Enumber m = new Enumber();
                object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    m.Desction = da.Description;
                }
                m.EnumValue = Convert.ToInt32(e);
                m.EnumName = e.ToString();
                list.Add(m);
            }
            return list;
        }

        /// <summary>
        /// 获取枚举的描述文本
        /// </summary>
        /// <param name="e">枚举成员</param>
        /// <returns></returns>
        public static string EnumDescription(this Enum e)
        {
            try
            {
                var name = Enum.GetName(e.GetType(), e);
                var type = e.GetType();
                string dictKey = string.Format("{0}.{1}", type.Name, name);
                if (!description.ContainsKey(dictKey))
                {
                    return SetEnumDescription(e);
                }
                return description[dictKey];
            }
            catch { }
            return null;
        }
        /// <summary>
        /// 获取枚举描述文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string EnumDescription<T>(this int enumValue) where T : struct
        {
            var result = string.Empty;
            try
            {
                var value = Enum.Parse(typeof(T), enumValue.ToString(), true) as Enum;
                result = EnumDescription(value);
            }
            catch {}
            return result;
        }
    }
}
