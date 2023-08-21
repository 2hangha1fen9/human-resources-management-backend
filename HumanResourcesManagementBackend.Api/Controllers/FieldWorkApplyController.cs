using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Services.Interface;
using HumanResourcesManagementBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HumanResourcesManagementBackend.Common;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class FieldWorkApplyController : BaseApiController
    {
        private readonly IFieldWorkApplyService _fieldWorkApplyService;
        public FieldWorkApplyController()
        {
            _fieldWorkApplyService = new FieldWorkApplyService();
        }

        /// <summary>
        /// 外勤申请
        /// </summary>
        /// <param name="FieldWorkApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response FieldWorkApply(FieldWorkApplyDto fieldWorkApply)
        {
            long uid = CurrentUser.EmployeeId;
            if (uid == 0)
            {
                throw new BusinessException
                {
                    ErrorMessage = "没有权限",
                    Status = ResponseStatus.ParameterError
                };
            }
            _fieldWorkApplyService.FieldWorkApply(fieldWorkApply, uid);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
