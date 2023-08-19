using HumanResourcesManagementBackend.Api.Exceptions;
using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Services;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        [HttpPost]
        public PageResponse<UserDto.User> QueryUserByPage(UserDto.Search search)
        {
            var users = userService.GetUsers(search);

            //返回响应结果
            return new PageResponse<UserDto.User>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = users ?? throw new ServiceException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }
    }
}
