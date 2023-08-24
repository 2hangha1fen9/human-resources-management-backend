using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
        public int TotalPeople()
        {
            using (var db = new HRM())
            {
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                int number = query.Count();
                return number;
            }
        }
        public List<SummaryDto> GetSenioritySummary()
        {
            using (var db = new HRM())
            {
                var senioritylist = new List<SummaryDto>();
                var seniority = new SummaryDto();
                
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number =query.Count();
                double percent;
                for (int i = 0; i < 8;i++)
                {
                    switch (i)
                    {
                        case 0: { seniority.Category = "1-3月"; senioritylist.Add(seniority);break; };
                        case 1: { seniority.Category = "3-6月"; senioritylist.Add(seniority); break; };
                        case 2: { seniority.Category = "6-12月"; senioritylist.Add(seniority); break; };
                        case 3: { seniority.Category = "1-2年"; senioritylist.Add(seniority); break; };
                        case 4: { seniority.Category = "2-3年"; senioritylist.Add(seniority); break; };
                        case 5: { seniority.Category = "3-5年"; senioritylist.Add(seniority); break; };
                        case 6: { seniority.Category = "5-8年"; senioritylist.Add(seniority); break; };
                        case 7: { seniority.Category = "8年以上"; senioritylist.Add(seniority); break; };
                        default:break;
                    }
                    seniority = new SummaryDto();
                    
                }
                foreach (var item in query)
                {
                    int month = ((DateTime.Now.Year - item.HireDate.Year) * 12) + DateTime.Now.Month - item.HireDate.Month;
                    foreach (var se in senioritylist)
                    {
                        se.Proportion = "0.00%";
                        if (se.Category == "1-3月"&&month < 3)
                        {
                            se.Number++;
                            percent = Convert.ToDouble(se.Number) / Convert.ToDouble(number);
                            se.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (se.Category == "3-6月" && month < 6)
                        {
                            se.Number++;
                            percent = Convert.ToDouble(se.Number) / Convert.ToDouble(number);
                            se.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (se.Category == "6-12月" && month < 12)
                        {
                            se.Number++;
                            percent = Convert.ToDouble(se.Number) / Convert.ToDouble(number);
                            se.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (se.Category == "1-2年" && month < 24)
                        {
                            se.Number++;
                            percent = Convert.ToDouble(se.Number) / Convert.ToDouble(number);
                            se.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (se.Category == "2-3年" && month < 36)
                        {
                            se.Number++;
                            percent = Convert.ToDouble(se.Number) / Convert.ToDouble(number);
                            se.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (se.Category == "3-5年" && month < 60)
                        {
                            se.Number++;
                            percent = Convert.ToDouble(se.Number) / Convert.ToDouble(number);
                            se.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (se.Category == "5-8年" && month < 96)
                        {
                            se.Number++;
                            percent = Convert.ToDouble(se.Number) / Convert.ToDouble(number);
                            se.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (se.Category == "8年以上")
                        {
                            se.Number++;
                            percent = Convert.ToDouble(se.Number) / Convert.ToDouble(number);
                            se.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else { continue; }
                    }

                }

                return senioritylist;
            }
        }
    }
}
