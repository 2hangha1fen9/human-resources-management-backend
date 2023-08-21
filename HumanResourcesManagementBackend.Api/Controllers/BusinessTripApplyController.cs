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
    public class BusinessTripApplyController : BaseApiController
    {
        private readonly IBusinessTripApplyService _businessTripApplyService;
        public BusinessTripApplyController()
        {
            _businessTripApplyService = new BusinessTripApplyService();
        }
        /// <summary>
        /// 出差申请
        /// </summary>
        /// <param name="BusinessTripApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response BusinessTripApply(BusinessTripApplyDto businessTripApply)
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
            _businessTripApplyService.BusinessTripApply(businessTripApply, uid);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
