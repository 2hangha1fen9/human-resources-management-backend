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
    public class ApplyController : BaseApiController
    {
        private readonly IApplyService _applyService;
        public ApplyController()
        {
            _applyService = new ApplyService();
        }
        /// <summary>
        /// 休假申请
        /// </summary>
        /// <param name="VacationApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response VacationApply(VacationApplyDto vacationApply)
        {
            long id = CurrentUser.EmployeeId;
            if (id == 0)
            {
                throw new BusinessException
                {
                    ErrorMessage = "没有权限",
                    Status = ResponseStatus.ParameterError
                };
            }
            _applyService.VacationApply(vacationApply,1);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };  
        }
    }
}
