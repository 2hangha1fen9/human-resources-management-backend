using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Enum
{
    /// <summary>
    /// 学历枚举
    /// </summary>
    public enum AcademicDegree
    {
        /// <summary>
        /// 无学历
        /// </summary>
        [Description("无学历")]
        None = 0,
        /// <summary>
        /// 小学
        /// </summary>
        [Description("小学")]
        Primary = 1,
        /// <summary>
        /// 初中
        /// </summary>
        [Description("初中")]
        Middle = 2,
        /// <summary>
        /// 高中
        /// </summary>
        [Description("高中")] 
        HighSchool = 3,
        /// <summary>
        /// 中职
        /// </summary>
        [Description("中职")]
        Vocational = 4,
        /// <summary>
        /// 大专
        /// </summary>
        [Description("大专")]
        College = 5,
        /// <summary>
        /// 本科
        /// </summary>
        [Description("本科")]
        Bachelor = 6,
        /// <summary>
        /// 硕士
        /// </summary>
        [Description("硕士")]
        Master = 7,
        /// <summary>
        /// 博士
        /// </summary>
        [Description("博士")]
        Doctorate = 8
    }
}
