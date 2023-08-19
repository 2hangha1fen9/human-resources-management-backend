using HumanResourcesManagementBackend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseEntity
    {
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
}
