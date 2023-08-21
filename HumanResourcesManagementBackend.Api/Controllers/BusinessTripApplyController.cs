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
            businessTripApply.EmployeeId = CurrentUser.EmployeeId;
            _businessTripApplyService.BusinessTripApply(businessTripApply);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
