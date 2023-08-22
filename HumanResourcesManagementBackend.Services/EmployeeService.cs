using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HumanResourcesManagementBackend.Models.UserDto;

namespace HumanResourcesManagementBackend.Services
{
    public class EmployeeService:IEmployeeService
    {
        public List<EmployeeDto.Employee> GetEmploysees(EmployeeDto.Search search)
        {
            using(var db = new HRM())
            {
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //条件过滤
                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(u => u.Name.Contains(search.Name));
                }
                if (!string.IsNullOrEmpty(search.WorkNum))
                {
                    query = query.Where(u => u.WorkNum.Contains(search.WorkNum));
                }
                if (search.Status > 0)
                {
                    query = query.Where(u => u.Status == search.Status);
                }
                //分页并将数据库实体映射为dto对象(OrderBy必须调用)
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<EmployeeDto.Employee>();
                //状态处理
                list.ForEach(u =>
                {
                    u.StatusStr = u.Status.Description();
                    u.PositionLevelStr = u.PositionLevel.Description();
                    u.WorkStatusStr= u.WorkStatus.Description();
                    u.AcademicDegreeStr= u.AcademicDegree.Description();
                });
                return list;
            }
        }
        public EmployeeDto.Employee GetEmployseeById(long id )
        {
            using (var db = new HRM())
            {
                var employR = db.Employees.FirstOrDefault(u => u.Id == id && u.Status != DataStatus.Deleted);
                if (employR == null)
                {
                    throw new BusinessException
                    {
                        Status = ResponseStatus.NoData,
                        ErrorMessage = ResponseStatus.NoData.Description()
                    };
                }
                var employ = employR.MapTo<EmployeeDto.Employee>();
                employ.StatusStr = employ.Status.Description();
                employ.PositionLevelStr =employ.PositionLevel.Description();
                employ.WorkStatusStr = employ.WorkStatus.Description();
                employ.AcademicDegreeStr = employ.AcademicDegree.Description();
                return employ;
            }
        }
        public void AddEmploysee(EmployeeDto.Employee employee)
        {
            using (var db = new HRM())
            {
                var employse = db.Employees.FirstOrDefault(p => p.WorkNum == employee.WorkNum);
                if (employse != null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "员工已经存在",
                        Status = ResponseStatus.ParameterError
                    };
                }
                if(employee.WorkNum==""||employee.Name==""||employee.HireDate==null||employee.IdCard=="")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请输入对应的参数",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var employeeR = employee.MapTo<R_Employee>();
                employeeR.CreateTime = DateTime.Now;
                employeeR.UpdateTime = DateTime.Now;
                db.Employees.Add(employeeR);
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "员工添加失败",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }
        public void EditEmploysee(EmployeeDto.Edit edit)
        {
            using (var db = new HRM())
            {
                //查询是否存在
                var employEx = db.Employees.FirstOrDefault(u => u.WorkNum == edit.WorkNum);
                if (employEx == null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "员工不存在",
                        Status = ResponseStatus.ParameterError
                    };
                }
                employEx.WorkStatus = edit.WorkStatus;
                employEx.IdCard = edit.IdCard;
                employEx.Native= edit.Native;
                employEx.AcademicDegree=edit.AcademicDegree;
                employEx.PositionId = edit.PositionId;
                employEx.DepartmentId= edit.DepartmentId;
                employEx.PositionLevel=edit.PositionLevel;
                employEx.Phone = edit.Phone;
                employEx.Email=edit.Email;
                employEx.UpdateTime = DateTime.Now;
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "员工修改失败",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }
        public void DeleEmploysee(long eid)
        {
            using (var db = new HRM())
            {
                var employ = db.Employees.FirstOrDefault(u => u.Id == eid) ?? throw new BusinessException
                {
                    ErrorMessage = "员工不存在",
                    Status = ResponseStatus.ParameterError
                };
                employ.Status = DataStatus.Deleted;
                employ.UpdateTime = DateTime.Now;
                db.SaveChanges();
            }
        }
    }
}
