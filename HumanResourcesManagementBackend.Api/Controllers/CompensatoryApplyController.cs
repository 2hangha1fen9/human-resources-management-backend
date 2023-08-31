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
        public Response Apply(CompensatoryApplyDto.CompensatoryApply compensatoryApply)
        {
            compensatoryApply.EmployeeId = CurrentUser.EmployeeId;
            _compensatoryApplyService.CompensatoryApply(compensatoryApply);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询当前员工的调休申请记录
        /// </summary>
        /// <param name="SeleCompensatoryApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<CompensatoryApplyDto.CompensatoryApply> QueryMyCompensatoryListByPage(CompensatoryApplyDto.Search search)
        {
            search.EmployeeId = CurrentUser.EmployeeId;
            var compensatory = _compensatoryApplyService.QueryMyCompensatoryListByPage(search);
            return new PageResponse<CompensatoryApplyDto.CompensatoryApply>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = compensatory ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData,
                    ErrorMessage = ResponseStatus.NoData.Description()
                }
            };
        }
        /// <summary>
        /// 查询调休申请记录
        /// </summary>
        /// <param name="SeleAllCompensatoryApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<CompensatoryApplyDto.CompensatoryApply> QueryCompensatoryListByPage(CompensatoryApplyDto.Search search)
        {
            var compensatory = _compensatoryApplyService.QueryMyCompensatoryListByPage(search);
            return new PageResponse<CompensatoryApplyDto.CompensatoryApply>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = compensatory ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData,
                    ErrorMessage = ResponseStatus.NoData.Description()
                }
            };
        }
        /// <summary>
        /// 查询调休申请记录详情
        /// </summary>
        /// <param name="GetBusinessTripById"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<CompensatoryApplyDto.CompensatoryApply> GetCompensatoryById(long id)
        {
            var compensatory = _compensatoryApplyService.GetCompensatoryById(id);
            return new DataResponse<CompensatoryApplyDto.CompensatoryApply>
            {
                Data = compensatory,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }
        /// <summary>
        /// 审核员工的调休申请记录
        /// </summary>
        /// <param name="ExamineCompensatoryApply"></param>
        /// <returns></returns>
        [HttpPut]
        public Response ExamineCompensatoryApply(CompensatoryApplyDto.Examine examine)
        {
            _compensatoryApplyService.ExamineCompensatoryApply(examine);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
