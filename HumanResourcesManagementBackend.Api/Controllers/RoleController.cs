using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Services;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly IRoleService roleService;

        public RoleController()
        {
            this.roleService = new RoleService();
        }

        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<RoleDto.Role> QueryRoleByPage(RoleDto.Search search)
        {
            var roles = roleService.GetRoles(search);
            //返回响应结果
            return new PageResponse<RoleDto.Role>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = roles ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }

        /// <summary>
        /// 根据ID查询角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<RoleDto.Role> GetRoleById(long id)
        {
            var role = roleService.GetRoleById(id);
            return new DataResponse<RoleDto.Role>
            {
                Data = role,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }

        /// <summary>
        /// 根据用户ID查询角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<RoleDto.Role>> GetRolesByUserId(long id)
        {
            var role = roleService.GetRolesByUserId(id);
            return new DataResponse<List<RoleDto.Role>>
            {
                Data = role,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        [HttpPost]
        public Response AddRole(RoleDto.Save role)
        {
            roleService.AddRole(role);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        public Response EditRole(RoleDto.Save role)
        {
            roleService.EditRole(role);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Response DeleteRole(long id)
        {
            roleService.DeleteRole(id);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}