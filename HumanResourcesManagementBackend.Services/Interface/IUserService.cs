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

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns>用户ID</returns>
        long Login(UserDto.Login login);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        void AddUser(UserDto.Save user);

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="user"></param>
        void EditUser(UserDto.Save user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        void DeleteUser(long userId);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pwd"></param>
        void ChangePwd(UserDto.ChangePwd changePwd);

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="pwd"></param>
        void ForgotPassword(UserDto.ChangePwd changePwd);

        /// <summary>
        /// 修改密保问题
        /// </summary>
        /// <param name="question"></param>
        void ChangeQuestion(UserDto.ChangePwd changeQuestion);
    }
}
