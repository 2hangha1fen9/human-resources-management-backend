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

        /// <summary>
        /// 休假申请
        /// </summary>
        /// <param name="VacationApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response VacationApply(VacationApplyDto.VacationApply vacationApply)
        {
            vacationApply.EmployeeId = 1;
            _vacationapplyService.VacationApply(vacationApply);
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
        public PageResponse<VacationApplyDto.VacationApply> QueryMyVacationListByPage(VacationApplyDto.Search search)
        {
            search.EmployeeId = CurrentUser.EmployeeId;
            var vacationapply = _vacationapplyService.GetVacationApplyList(search);
            return new PageResponse<VacationApplyDto.VacationApply>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = vacationapply ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData,
                    ErrorMessage = ResponseStatus.NoData.Description()
                }
            };
        }

        /// <summary>
        /// 查询休假申请记录
        /// </summary>
        /// <param name="SeleVacationApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<VacationApplyDto.VacationApply> QueryVacationListByPage(VacationApplyDto.Search search)
        {
            var vacationapply = _vacationapplyService.GetVacationApplyList(search);
            return new PageResponse<VacationApplyDto.VacationApply>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = vacationapply ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData,
                    ErrorMessage = ResponseStatus.NoData.Description()
                }
            };
        }
        /// <summary>
        /// 审核员工的休假申请记录
        /// </summary>
        /// <param name="ExamineVacationApply"></param>
        /// <returns></returns>
        [HttpPut]
        public Response ExamineVacationApply(VacationApplyDto.Examine examine)
        {
            _vacationapplyService.ExamineVacationApply(examine);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
