using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    seniority.Proportion = "0.00%";
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
        public List<SummaryDto> GetGradeSummary()
        {
            using (var db = new HRM())
            {
                var gradelist = new List<SummaryDto>();
                var grade = new SummaryDto();
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number = query.Count();
                double percent;
                for (int i = 0; i < 4; i++)
                {
                    grade.Proportion = "0.00%";
                    switch (i)
                    {
                        case 0: { grade.Category = "基层员工"; gradelist.Add(grade); break; };
                        case 1: { grade.Category = "基层职员"; gradelist.Add(grade); break; };
                        case 2: { grade.Category = "中层管理"; gradelist.Add(grade); break; };
                        case 3: { grade.Category = "储备干部"; gradelist.Add(grade); break; };
                        default: break;
                    }
                    grade = new SummaryDto();
                }
                foreach(var item in query)
                {
                    foreach(var ga in gradelist)
                    {
                        if (item.PositionLevel== PositionLevel.GrassrootsStaff && ga.Category== "基层员工")
                        {
                            ga.Number++;
                            percent = Convert.ToDouble(ga.Number) / Convert.ToDouble(number);
                            ga.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if(item.PositionLevel == PositionLevel.JuniorStaff && ga.Category == "基层职员")
                        {
                            ga.Number++;
                            percent = Convert.ToDouble(ga.Number) / Convert.ToDouble(number);
                            ga.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (item.PositionLevel == PositionLevel.MiddleManager && ga.Category == "中层管理")
                        {
                            ga.Number++;
                            percent = Convert.ToDouble(ga.Number) / Convert.ToDouble(number);
                            ga.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (item.PositionLevel == PositionLevel.ManagementTrainee && ga.Category == "储备干部")
                        {
                            ga.Number++;
                            percent = Convert.ToDouble(ga.Number) / Convert.ToDouble(number);
                            ga.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else{continue;}
                    }
                }
                return gradelist;
            }
        }
        public List<SummaryDto> GetAgeSummary()
        {
            using (var db = new HRM())
            {
                var agelist = new List<SummaryDto>();
                var age = new SummaryDto();
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number = query.Count();
                double percent;
                for (int i = 0; i < 8; i++)
                {
                    age.Proportion = "0.00%";
                    switch (i)
                    {
                        case 0: { age.Category = "18周岁以下"; agelist.Add(age); break; };
                        case 1: { age.Category = "18-25周岁"; agelist.Add(age); break; };
                        case 2: { age.Category = "26-35周岁"; agelist.Add(age); break; };
                        case 3: { age.Category = "36-45周岁"; agelist.Add(age); break; };
                        case 4: { age.Category = "46-50周岁"; agelist.Add(age); break; };
                        case 5: { age.Category = "51-55周岁"; agelist.Add(age); break; };
                        case 6: { age.Category = "56-60周岁"; agelist.Add(age); break; };
                        case 7: { age.Category = "60周岁以上"; agelist.Add(age); break; };
                        default: break;
                    }
                    age = new SummaryDto();
                }
                foreach(var item in query)
                {
                    int years = DateTime.Now.Year - item.BirthDay.Year;
                    foreach (var ag in agelist)
                    {
                        if (ag.Category == "18周岁以下" && years < 18)
                        {
                            ag.Number++;
                            percent = Convert.ToDouble(ag.Number) / Convert.ToDouble(number);
                            ag.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (ag.Category == "18-25周岁" && years < 26)
                        {
                            ag.Number++;
                            percent = Convert.ToDouble(ag.Number) / Convert.ToDouble(number);
                            ag.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (ag.Category == "26-35周岁" && years < 36)
                        {
                            ag.Number++;
                            percent = Convert.ToDouble(ag.Number) / Convert.ToDouble(number);
                            ag.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (ag.Category == "36-45周岁" && years < 46)
                        {
                            ag.Number++;
                            percent = Convert.ToDouble(ag.Number) / Convert.ToDouble(number);
                            ag.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (ag.Category == "46-50周岁" && years < 51)
                        {
                            ag.Number++;
                            percent = Convert.ToDouble(ag.Number) / Convert.ToDouble(number);
                            ag.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (ag.Category == "51-55周岁" && years < 56)
                        {
                            ag.Number++;
                            percent = Convert.ToDouble(ag.Number) / Convert.ToDouble(number);
                            ag.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (ag.Category == "56-60周岁" && years < 61)
                        {
                            ag.Number++;
                            percent = Convert.ToDouble(ag.Number) / Convert.ToDouble(number);
                            ag.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else if (ag.Category == "60周岁以上")
                        {
                            ag.Number++;
                            percent = Convert.ToDouble(ag.Number) / Convert.ToDouble(number);
                            ag.Proportion = percent.ToString("0.00%");
                            break;
                        }
                        else { continue; }
                    }
                }
                return agelist;
            }
        }
    }
}
