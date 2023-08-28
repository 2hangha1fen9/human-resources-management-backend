using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Services;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using HumanResourcesManagementBackend.Common;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class PermissionController : BaseApiController
    {
        private readonly IPermissionService permissionService;

        public PermissionController()
        {
            this.permissionService = new PermissionService();
        }

        /// <summary>
        /// 查询权限列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<PermissionDto.Permission> QueryPermissionByPage(PermissionDto.Search search)
        {
            var permissions = permissionService.GetPermissions(search);
            //返回响应结果
            return new PageResponse<PermissionDto.Permission>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = permissions ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }

        /// <summary>
        /// 根据ID查询权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<PermissionDto.Permission> GetPermissionById(long id)
        {
            var permission = permissionService.GetPermissionById(id);
            return new DataResponse<PermissionDto.Permission>
            {
                Data = permission,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }

        /// <summary>
        /// 根据角色ID查询权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<PermissionDto.Permission>> GetPermissionsByRoleId(long id)
        {
            var permission = permissionService.GetPermissionsByRoleId(id);
            return new DataResponse<List<PermissionDto.Permission>>
            {
                Data = permission,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }

        /// <summary>
        /// 根据用户ID查询权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<PermissionDto.Permission>> GetPermissionsByUserId(long id, [FromUri] PermissionType type)
        {
            var permission = permissionService.GetPermissionsByUserId(id,type);
            return new DataResponse<List<PermissionDto.Permission>>
            {
                Data = permission,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        [HttpPost]
        public Response AddPermission(PermissionDto.Save permission)
        {
            permissionService.AddPermission(permission);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPut]
        public Response EditPermission(PermissionDto.Save permission)
        {
            permissionService.EditPermission(permission);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Response DeletePermission(long id)
        {
            permissionService.DeletePermission(id);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}