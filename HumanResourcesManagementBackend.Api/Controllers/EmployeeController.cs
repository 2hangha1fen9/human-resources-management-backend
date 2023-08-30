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
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }
        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="seleemployee"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<EmployeeDto.Employee> QueryEmployeeByPage(EmployeeDto.Search search)
        {
            var employees =_employeeService.GetEmploysees(search);

            employees.ToExcel();
            //返回响应结果
            return new PageResponse<EmployeeDto.Employee>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = employees ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }

        /// <summary>
        /// 导出员工列表到excel
        /// </summary>
        /// <param name="seleemployee"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage ExportEmployeeToExcel(EmployeeDto.Search search)
        {
            var employees = _employeeService.GetEmploysees(search);
            var stream = employees.ToExcel();
            // 创建一个HttpResponseMessage实例
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
            };
            string fileName = $"员工列表-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.xlsx";
            string encodedFileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            // 设置响应的Content-Type
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            // 设置响应的Content-Disposition，以便在浏览器中触发文件下载
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = encodedFileName
            };
            return response;
        }

        /// <summary>
        /// 根据id查询员工
        /// </summary>
        /// <param name="seleemployeebyid"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<EmployeeDto.Employee> GetEmployeeById(long id)
        {
            var employee = _employeeService.GetEmployseeById(id);
            return new DataResponse<EmployeeDto.Employee>
            {
                Data = employee,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="employsee"></param>
        /// <returns></returns>
        [HttpPost]
        public Response AddEmploysee(EmployeeDto.Employee employee)
        {
            _employeeService.AddEmploysee(employee);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="editemploysee"></param>
        /// <returns></returns>
        [HttpPut]
        public Response EditEmployee(EmployeeDto.Edit edit)
        {
            _employeeService.EditEmploysee(edit);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="deleemploysee"></param>
        /// <returns></returns>
        [HttpDelete]
        public Response DeleteEmploy(long id)
        {
            _employeeService.DeleEmploysee(id);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 查询工龄汇总信息
        /// </summary>
        /// <param name="GetSenioritySummary"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<EmployeeDto.Summary>> GetSenioritySummary()
        {
            var  getseniority= _employeeService.GetSenioritySummary();
            return new DataResponse<List<EmployeeDto.Summary>>
            {
                Data = getseniority,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 查询职别汇总信息
        /// </summary>
        /// <param name="GetGradeSummary"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<EmployeeDto.Summary>> GetGradeSummary()
        {
            var getgrade= _employeeService.GetGradeSummary();
            return new DataResponse<List<EmployeeDto.Summary>>
            {
                Data = getgrade,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询年龄汇总信息
        /// </summary>
        /// <param name="GetAgeSummary"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<EmployeeDto.Summary>> GetAgeSummary()
        {
            var getage = _employeeService.GetAgeSummary();
            return new DataResponse<List<EmployeeDto.Summary>>
            {
                Data = getage,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询学历汇总信息
        /// </summary>
        /// <param name="GetEducationSummary"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<EmployeeDto.Summary>> GetEducationSummary()
        {
            var geteducation = _employeeService.GetEducationSummary();
            return new DataResponse<List<EmployeeDto.Summary>>
            {
                Data = geteducation,               
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询部门汇总信息
        /// </summary>
        /// <param name="GetDepartmentSummary"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<EmployeeDto.Summary>> GetDepartmentSummary()
        {
            var getdepartment = _employeeService.GetDepartmentSummary();
            return new DataResponse<List<EmployeeDto.Summary>>
            {
                Data = getdepartment,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询性别汇总信息
        /// </summary>
        /// <param name="GetGenderSummary"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<EmployeeDto.Summary>> GetGenderSummary()
        {
            var getgender = _employeeService.GetGenderSummary();
            return new DataResponse<List<EmployeeDto.Summary>>
            {
                Data = getgender,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询婚姻汇总信息
        /// </summary>
        /// <param name="GetMaritalSummary"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<EmployeeDto.Summary>> GetMaritalSummary()
        {
            var getmarital = _employeeService.GetMaritalSummary();
            return new DataResponse<List<EmployeeDto.Summary>>
            {
                Data = getmarital,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询生日汇总信息
        /// </summary>
        /// <param name="GetBirthdaySummary"></param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<List<EmployeeDto.BirthdaySummary>> GetBirthdaySummary()
        {
            var getbirthday = _employeeService.GetBirthdaySummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<List<EmployeeDto.BirthdaySummary>>
            {
                Data = getbirthday,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        //#region 读取Excel数据
        ///// <summary>
        ///// 读取excel数据
        ///// </summary>
        ///// <param name="ReadExcel"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public Response ReadExcel(string fileName, string sheetName, bool isFirstRowColumn)
        //{
        //    _employeeService.ReadExcel(fileName,sheetName,isFirstRowColumn);
        //    return new Response
        //    {
        //        Status = ResponseStatus.Success,
        //        Message = ResponseStatus.Success.Description()
        //    };
        //}
        //#endregion
    }
}
