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
        public Response Apply(BusinessTripApplyDto.BusinessTripApply businessTripApply)
        {
            businessTripApply.EmployeeId = CurrentUser.EmployeeId;
            _businessTripApplyService.BusinessTripApply(businessTripApply);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询当前员工出差申请记录
        /// </summary>
        /// <param name="SeleBusinessTripApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<BusinessTripApplyDto.BusinessTripApply> QueryMyBusinessTripListByPage(BusinessTripApplyDto.Search search)
        {
            search.EmployeeId = CurrentUser.EmployeeId;
            var vacationapply = _businessTripApplyService.QueryMyBusinessTripListByPage(search);
            return new PageResponse<BusinessTripApplyDto.BusinessTripApply>()
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
        /// 查询出差申请记录
        /// </summary>
        /// <param name="SeleBusinessTripApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<BusinessTripApplyDto.BusinessTripApply> QueryBusinessTripListByPage(BusinessTripApplyDto.Search search)
        {
            var vacationapply = _businessTripApplyService.QueryMyBusinessTripListByPage(search);
            return new PageResponse<BusinessTripApplyDto.BusinessTripApply>()
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
        /// 查询出差申请记录详情
        /// </summary>
        /// <param name="GetBusinessTripById"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<BusinessTripApplyDto.BusinessTripApply> GetBusinessTripById(long id)
        {
            var businesstrip = _businessTripApplyService.GetBusinessTripById(id);
            return new DataResponse<BusinessTripApplyDto.BusinessTripApply>
            {
                Data = businesstrip,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }
        /// <summary>
        /// 审核员工的出差申请记录
        /// </summary>
        /// <param name="ExamineBusinessTripApply"></param>
        /// <returns></returns>
        [HttpPut]
        public Response ExamineBusinessTripApply(BusinessTripApplyDto.Examine examine)
        {
            _businessTripApplyService.ExamineBusinessTripApply(examine);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}
