using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<UserDto.User> GetUsers(UserDto.Search search);
    }
}
