using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using HumanResourcesManagementBackend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services
{
    public class UserService : IUserService
    {
        public List<UserDto.User> GetUsers(UserDto.Search search)
        {
            using(var db = new HRM())
            {
                var query = from user in db.Users
                            where user.Status != DataStatus.Deleted
                            select user;
                //条件过滤
                if (!string.IsNullOrEmpty(search.LoginName))
                {
                    query = query.Where(u => u.LoginName.Contains(search.LoginName));
                }
                if (search.Status > 0)
                {
                    query = query.Where(u => u.Status == search.Status);
                }
                //分页并将数据库实体映射为dto对象
                var list = query.Pageing(search).MapToList<UserDto.User>();
                //状态处理
                list.ForEach(u =>
                {
                    u.StatusStr = u.Status.Description();
                });
                return list;
            }
        }
    }
}
