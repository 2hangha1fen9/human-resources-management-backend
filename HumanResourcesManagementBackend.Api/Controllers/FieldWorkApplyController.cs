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
    public class FieldWorkApplyController : BaseApiController
    {
        private readonly IFieldWorkApplyService _fieldWorkApplyService;
        public FieldWorkApplyController()
        {
            _fieldWorkApplyService = new FieldWorkApplyService();
        }

        /// <summary>
        /// 外勤申请
        /// </summary>
        /// <param name="FieldWorkApply"></param>
        /// <returns></returns>
        [HttpPost]
        public Response FieldWorkApply(FieldWorkApplyDto.FieldWorkApply fieldWorkApply)
        {
            fieldWorkApply.EmployeeId = CurrentUser.EmployeeId;
            _fieldWorkApplyService.FieldWorkApply(fieldWorkApply);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询当前员工的外勤申请记录
        /// </summary>
        /// <param name="SeleFieldWorkApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<FieldWorkApplyDto.FieldWorkApply> QueryMyFieldWorkListByPage(FieldWorkApplyDto.Search search)
        {
            search.EmployeeId = CurrentUser.EmployeeId;
            var fieldwork = _fieldWorkApplyService.QueryMyFieldWorkListByPage(search);
            return new PageResponse<FieldWorkApplyDto.FieldWorkApply>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = fieldwork ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData,
                    ErrorMessage = ResponseStatus.NoData.Description()
                }
            };
        }
        /// <summary>
        /// 查询外勤申请记录
        /// </summary>
        /// <param name="SeleFieldWorkApply"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<FieldWorkApplyDto.FieldWorkApply> QueryFieldWorkListByPage(FieldWorkApplyDto.Search search)
        {
            var fieldwork = _fieldWorkApplyService.QueryMyFieldWorkListByPage(search);
            return new PageResponse<FieldWorkApplyDto.FieldWorkApply>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = fieldwork ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData,
                    ErrorMessage = ResponseStatus.NoData.Description()
                }
            };
        }
    }
}
