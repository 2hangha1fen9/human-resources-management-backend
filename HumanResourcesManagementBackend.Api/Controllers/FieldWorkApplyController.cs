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
            fieldWorkApply.EmployeeId = CurrentUser.EmployeeId;
            _fieldWorkApplyService.FieldWorkApply(fieldWorkApply);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
