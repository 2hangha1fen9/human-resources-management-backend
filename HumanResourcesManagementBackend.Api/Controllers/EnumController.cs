using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    /// <summary>
    /// 枚举控制器
    /// </summary>
    public class EnumController : BaseApiController
    {
        /// <summary>
        /// 获取指定枚举
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<Enumber>> Get(string id)
        {
            var result = new DataResponse<List<Enumber>>();

            //获取Models命名空间下的所有枚举
            List<Type> enums = Assembly.GetAssembly(typeof(DataStatus)).GetTypes()
              .Where(type => type.IsEnum)
              .ToList();
            //获取指定枚举
            var enumType = enums.FirstOrDefault(e => string.Compare(e.Name, id, true) == 0);
            if(enumType != null)
            {
                result.Data = EnumHelper.ToList(enumType).Where(e => e.EnumValue != 99).ToList();
                result.Status = ResponseStatus.Success;
                result.Message = ResponseStatus.Success.Description();
            }
            else
            {
                throw new BusinessException
                {
                    Status = ResponseStatus.NoData,
                    ErrorMessage = ResponseStatus.NoData.Description()
                };
            }

            return result;
        }
    }
}
