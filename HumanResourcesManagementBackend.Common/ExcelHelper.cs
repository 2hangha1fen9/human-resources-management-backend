using MiniExcelLibs;
using MiniExcelLibs.Attributes;
using MiniExcelLibs.OpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Common
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 集合导出到excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async static Task<MemoryStream> ToExcel<T>(this List<T> list)
        {
            //获取数据列表元素类型
            var rowType = typeof(T);
            //获取类型的成员
            var members = rowType.GetProperties();
            //生成表头
            var header = new List<DynamicExcelColumn>();
            foreach (var member in members)
            {
                if (member.GetCustomAttributes<ExcelIgnoreAttribute>().Any())
                {
                    continue;
                }
                ExcelColumnAttribute attr = Attribute.GetCustomAttribute(member, typeof(ExcelColumnAttribute), false) as ExcelColumnAttribute;
                if (attr != null)
                {
                    header.Add(new DynamicExcelColumn(member.Name) { 
                        Name = attr.Name,
                        Width = attr.Width,
                        Format = attr.Format
                    });
                }
            }
            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(list,configuration: new OpenXmlConfiguration()
            {
                AutoFilter = false,
                DynamicColumns = header.ToArray(),
            });
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        /// <summary>
        /// Excel文件转集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<T>> ExcelToList<T>(this Stream stream) where T : class,new()
        {
            return await MiniExcel.QueryAsync<T>(stream);
        }
    }
}
