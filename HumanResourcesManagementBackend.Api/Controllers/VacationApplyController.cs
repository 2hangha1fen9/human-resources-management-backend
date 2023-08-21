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
    public class VacationApplyController : BaseApiController
    {
        private readonly IVacationApplyService _vacationapplyService;
        public VacationApplyController()
        {
            _vacationapplyService = new VacationApplyService();
        }

        /// <summary>
        /// 休假申请
        /// </summary>
        /// <param name="VacationApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response VacationApply(VacationApplyDto vacationApply)
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
            _vacationapplyService.VacationApply(vacationApply,uid);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };  
        }

    }
}
