using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 用户返回
    /// </summary>
    public class UserDto 
    {
        /// <summary>
        /// 用户对象
        /// </summary>
        public class User
        {
            /// <summary>
            /// ID
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 登录名
            /// </summary>
            public string LoginName { get; set; }
            /// <summary>
            /// 密保问题
            /// </summary>
            public string Question { get; set; }
            /// <summary>
            /// 员工ID
            /// </summary>
            public long EmployeeId { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public DateTime UpdateTime { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public string StatusStr { get; set; } 
        }
        
        /// <summary>
        /// 用户编辑
        /// </summary>
        public class Save
        {
            /// <summary>
            /// ID
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 登录名
            /// </summary>
            public string LoginName { get; set; }
            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }
            /// <summary>
            /// 密保问题
            /// </summary>
            public string Question { get; set; }
            /// <summary>
            /// 密保答案
            /// </summary>
            public string Answer { get; set; }
            /// <summary>
            /// 员工ID
            /// </summary>
            public long EmployeeId { get; set; }

        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public class Login
        {
            /// <summary>
            /// 登录名
            /// </summary>
            public string LoginName { get; set; }
            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }
        }

        /// <summary>
        /// 用户搜索请求
        /// </summary>
        public class Search : PageRequest
        {
            /// <summary>
            /// 用户名
            /// </summary>
            public string LoginName { get; set; }
            /// <summary>
            /// 数据状态
            /// </summary>
            public DataStatus Status { get; set; }
        }
    }
}
