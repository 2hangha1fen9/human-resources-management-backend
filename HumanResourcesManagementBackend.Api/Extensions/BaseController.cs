using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HumanResourcesManagementBackend.Api.Extensions
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 当前请求用户ID
        /// </summary>
        public UserDto.User CurrentUser { get; set; }
    }
}