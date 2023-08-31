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
        public Response Apply(AbsenceApplyDto.AbsenceApply absenceApply)
        {
            absenceApply.EmployeeId = CurrentUser.EmployeeId;
            _iabsenceapplyService.AbsenceApply(absenceApply);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 查询当前员工的缺勤申请记录
        /// </summary>
        /// <param name="SeleAbsenceApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<AbsenceApplyDto.AbsenceApply> QueryMyAbsenceListByPage(AbsenceApplyDto.Search search)
        {
            search.EmployeeId = CurrentUser.EmployeeId;
            var vacationapply = _iabsenceapplyService.GetMyAbsenceApplyList(search);
            return new PageResponse<AbsenceApplyDto.AbsenceApply>()
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
        /// 查询缺勤申请记录
        /// </summary>
        /// <param name="SeleAbsenceApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<AbsenceApplyDto.AbsenceApply> QueryAbsenceListByPage(AbsenceApplyDto.Search search)
        {
            var vacationapply = _iabsenceapplyService.GetAbsenceApplyList(search,CurrentUser);
            return new PageResponse<AbsenceApplyDto.AbsenceApply>()
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
        /// 查询缺勤申请记录详情
        /// </summary>
        /// <param name="GetAbsenceById"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<AbsenceApplyDto.AbsenceApply> GetAbsenceById(long id)
        {
            var absence = _iabsenceapplyService.GetAbsenceById(id);
            return new DataResponse<AbsenceApplyDto.AbsenceApply>
            {
                Data = absence,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }
        /// <summary>
        /// 审核员工的缺勤申请记录
        /// </summary>
        /// <param name="ExamineAbsenceApply"></param>
        /// <returns></returns>
        [HttpPut]
        public Response ExamineAbsenceApply(AbsenceApplyDto.Examine examine)
        {
            _iabsenceapplyService.ExamineAbsenceApply(examine, CurrentUser);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
