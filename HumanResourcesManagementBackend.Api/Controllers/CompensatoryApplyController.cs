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
    public class CompensatoryApplyController : BaseApiController
    {
        private readonly ICompensatoryApplyService _compensatoryApplyService;
        public CompensatoryApplyController()
        {
            _compensatoryApplyService = new CompensatoryApplyService();
        }
        /// <summary>
        /// 调休申请
        /// </summary>
        /// <param name="CompensatoryApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response CompensatoryApply(CompensatoryApplyDto compensatoryApply)
        {
            compensatoryApply.EmployeeId = CurrentUser.EmployeeId;
            _compensatoryApplyService.CompensatoryApply(compensatoryApply);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
