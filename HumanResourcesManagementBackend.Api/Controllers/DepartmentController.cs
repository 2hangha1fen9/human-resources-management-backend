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
    public class DepartmentController : BaseApiController
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController()
        {
            this.departmentService = new DepartmentService();
        }

        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<DepartmentDto.Department> QueryDepartmentByPage(DepartmentDto.Search search)
        {
            var departments = departmentService.GetDepartments(search);
            //返回响应结果
            return new PageResponse<DepartmentDto.Department>()
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = departments ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }

        /// <summary>
        /// 根据ID查询部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<DepartmentDto.Department> GetDepartmentById(long id)
        {
            var department = departmentService.GetDepartmentById(id);
            return new DataResponse<DepartmentDto.Department>
            {
                Data = department,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
            };
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        [HttpPost]
        public Response AddDepartment(DepartmentDto.Save department)
        {
            departmentService.AddDepartment(department);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        public Response EditDepartment(DepartmentDto.Save department)
        {
            departmentService.EditDepartment(department);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Response DeleteDepartment(long id)
        {
            departmentService.DeleteDepartment(id);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}