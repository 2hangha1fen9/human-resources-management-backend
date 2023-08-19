using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesManagementBackend.Api.Exceptions
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// 异常状态码
        /// </summary>
        public ResponseStatus? Status { get; set; }
    }
}