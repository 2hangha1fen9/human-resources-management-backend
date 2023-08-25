using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Dto
{
    public class AuthDto
    {
        /// <summary>
        /// 用户角色绑定
        /// </summary>
        public class UserRoleBind
        {
            /// <summary>
            /// 用户ID
            /// </summary>
            public long userId;
            /// <summary>
            /// 角色ID
            /// </summary>
            public List<long> roleIds;
        }

        /// <summary>
        /// 角色权限绑定
        /// </summary>
        public class RolePermissionBind
        {
            /// <summary>
            /// 角色ID
            /// </summary>
            public long roleId;
            /// <summary>
            /// 权限ID
            /// </summary>
            public List<long> permissionIds;
        }
    }
}
