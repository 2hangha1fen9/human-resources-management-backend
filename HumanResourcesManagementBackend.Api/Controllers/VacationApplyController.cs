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
using System.Security.Cryptography;
using System.Web.Http;
using static HumanResourcesManagementBackend.Models.UserDto;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class VacationApplyController : BaseApiController
    {
        private readonly IVacationApplyService _vacationapplyService;
        public VacationApplyController()
        {
            _vacationapplyService = new VacationApplyService();
        }
        long uid;
        public void PermissionDenied()
        {
            uid= CurrentUser.EmployeeId;
            if (uid == 0)
            {
                throw new BusinessException
                {
                    ErrorMessage = "没有权限",
                    Status = ResponseStatus.ParameterError
                };
            }
        }
        /// <summary>
        /// 休假申请
        /// </summary>
        /// <param name="VacationApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response VacationApply(VacationApplyDto.VacationApply vacationApply)
        {
            PermissionDenied();
            _vacationapplyService.VacationApply(vacationApply,uid);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };  
        }
        /// <summary>
        /// 查询当前员工的休假申请记录
        /// </summary>
        /// <param name="SeleVacationApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<VacationApplyDto.VacationApply> QueryMyVacationListByPage(VacationApplyDto.VacationApplySearch search)
        {
            PermissionDenied();
            search.EmployeeId = uid;
            var vacationapply = _vacationapplyService.QueryMyVacationListByPage(search);
            return new PageResponse<VacationApplyDto.VacationApply>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = vacationapply ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }
    }
}
