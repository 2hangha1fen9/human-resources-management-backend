using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Services.Interface;
using HumanResourcesManagementBackend.Services;
using HumanResourcesManagementBackend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class PositionController : BaseApiController
    {
        private readonly IPositionService positionService;

        public PositionController()
        {
            this.positionService = new PositionService();
        }

        /// <summary>
        /// 查询岗位列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<PositionDto.Position> QueryPositionByPage(PositionDto.Search search)
        {
            var positions = positionService.GetPositions(search);
            //返回响应结果
            return new PageResponse<PositionDto.Position>()
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = positions ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }

        /// <summary>
        /// 根据ID查询岗位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<PositionDto.Position> GetPositionById(long id)
        {
            var position = positionService.GetPositionById(id);
            return new DataResponse<PositionDto.Position>
            {
                Data = position,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }

        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        [HttpPost]
        public Response AddPosition(PositionDto.Save position)
        {
            positionService.AddPosition(position);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [HttpPut]
        public Response EditPosition(PositionDto.Save position)
        {
            positionService.EditPosition(position);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Response DeletePosition(long id)
        {
            positionService.DeletePosition(id);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}