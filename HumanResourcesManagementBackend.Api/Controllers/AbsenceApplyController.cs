using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Services.Interface;
using HumanResourcesManagementBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using HumanResourcesManagementBackend.Common;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class AbsenceApplyController : BaseApiController
    {
        private readonly IAbsenceApplyService _iabsenceapplyService;
        public AbsenceApplyController()
        {
            _iabsenceapplyService = new AbsenceApplyService();
        }
        /// <summary>
        /// 缺勤申请
        /// </summary>
        /// <param name="absenceApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response AbsenceApply(AbsenceApplyDto absenceApply)
        {
            absenceApply.EmployeeId = CurrentUser.EmployeeId;
            _iabsenceapplyService.AbsenceApply(absenceApply);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
