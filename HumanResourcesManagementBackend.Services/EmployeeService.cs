using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data;

using System.Linq;
using HumanResourcesManagementBackend.Repository.Extensions;
using static HumanResourcesManagementBackend.Models.UserDto;
using static HumanResourcesManagementBackend.Models.EmployeeDto;
using EnumsNET;
using System.Reflection.Emit;
using NPOI.SS.Formula.Functions;
using static HumanResourcesManagementBackend.Models.Dto.RoleDto;

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
                if (!string.IsNullOrEmpty(search.SearchKey))
                {
                    query = query.Where(u => u.IdCard.Contains(search.SearchKey) || 
                                                    u.Name.Contains(search.SearchKey) || 
                                                    u.WorkNum.Contains(search.SearchKey) || 
                                                    u.Native.Contains(search.SearchKey) || 
                                                    u.Phone.Contains(search.SearchKey) || 
                                                    u.Email.Contains(search.SearchKey));
                }
                if (search.Status > 0)
                {
                    query = query.Where(u => u.Status == search.Status);
                }
                if (search.EmployeeId > 0)
                {
                    query = query.Where(u => u.Id == search.EmployeeId);
                }
                if (search.WorkStatus > 0)
                {
                    query = query.Where(u => u.WorkStatus == search.WorkStatus);
                }
                if (search.Gender > 0)
                {
                    query = query.Where(u => u.Gender == search.Gender);
                }
                if (search.MaritalStatus > 0)
                {
                    query = query.Where(u => u.MaritalStatus == search.MaritalStatus);
                }
                if (search.AcademicDegree > 0)
                {
                    query = query.Where(u => u.AcademicDegree == search.AcademicDegree);
                }
                if (search.PositionLevel > 0)
                {
                    query = query.Where(u => u.PositionLevel == search.PositionLevel);
                }
                if (search.PositionId > 0)
                {
                    query = query.Where(u => u.PositionId == search.PositionId);
                }
                if (search.DepartmentId > 0)
                {
                    query = query.Where(u => u.DepartmentId == search.DepartmentId);
                }
                //分页并将数据库实体映射为dto对象(OrderBy必须调用)
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<EmployeeDto.Employee>();
                //查询部门,职级数据
                var deps = db.Departmentes.Where(d => d.Status == DataStatus.Enable).ToList();
                var position = db.Positiones.Where(d => d.Status == DataStatus.Enable).ToList();
                //状态处理
                list.ForEach(u =>
                {
                    u.StatusStr = u.Status.Description();
                    u.GenderStr = u.Gender.Description();
                    u.PositionLevelStr = u.PositionLevel.Description();
                    u.WorkStatusStr= u.WorkStatus.Description();
                    u.AcademicDegreeStr= u.AcademicDegree.Description();
                    u.MaritalStatusStr = u.MaritalStatus.Description();
                    u.DepartmentName = deps?.FirstOrDefault(d => d.Id == u.DepartmentId)?.DepartmentName ?? "";
                    u.PositionName = position?.FirstOrDefault(d => d.Id == u.PositionId)?.PositionName ?? "";
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
                employ.GenderStr = employ.Gender.Description();
                employ.PositionLevelStr = employ.PositionLevel.Description();
                employ.WorkStatusStr = employ.WorkStatus.Description();
                employ.AcademicDegreeStr = employ.AcademicDegree.Description();
                employ.MaritalStatusStr = employ.MaritalStatus.Description();
                employ.DepartmentName = db.Departmentes.FirstOrDefault(d => d.Id == employ.DepartmentId)?.DepartmentName ?? "";
                employ.PositionName = db.Positiones.FirstOrDefault(d => d.Id == employ.PositionId)?.PositionName ?? "";
                return employ;
            }
        }
        public void AddEmploysee(EmployeeDto.Employee employee)
        {
            using (var db = new HRM())
            {
                var flag = db.Transaction(() =>
                {
                    var employse = db.Employees.FirstOrDefault(p => p.WorkNum == employee.WorkNum);
                    if (employse != null)
                    {
                        return false;
                    }
                    if (employee.WorkNum == "" || employee.Name == "" || employee.HireDate == null || employee.IdCard == "")
                    {
                        return false;
                    }
                    var employeeR = employee.MapTo<R_Employee>();
                    employeeR.CreateTime = DateTime.Now;
                    employeeR.UpdateTime = DateTime.Now;
                    db.Employees.Add(employeeR);
                    db.SaveChanges();
                    if (employeeR == null || employeeR.Id == 0)
                    {
                        return false;
                    }

                    //创建默认账号
                    if (employee.CreateUser)
                    {
                        var userR = new R_User
                        {
                            LoginName = employeeR.WorkNum,
                            Password = employeeR.WorkNum.Encrypt(),
                            Question = "我的工号是什么",
                            Answer = employeeR.WorkNum,
                            EmployeeId = employeeR.Id,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                        };

                        //查询是否存在
                        var userEx = db.Users.FirstOrDefault(u => u.LoginName == userR.LoginName);
                        if (userEx != null)
                        {
                            return false;
                        }

                        userR = db.Users.Add(userR);
                        db.SaveChanges();
                        if (userR == null || userR.Id == 0)
                        {
                            return false;
                        }
                        //绑定默认角色
                        var defaultRoles = db.Roles.Where(r => r.IsDefault == YesOrNo.Yes && r.Status == DataStatus.Enable).ToList();
                        if (defaultRoles != null || defaultRoles.Count > 0)
                        {
                            var bindList = defaultRoles.Select(r => new R_UserRoleRef
                            {
                                UserId = userR.Id,
                                RoleId = r.Id,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                            }).ToList();
                            var bindResult = db.UserRoleRefs.AddRange(bindList).ToList();
                            db.SaveChanges();
                            if (bindResult == null || bindList.Count != bindResult.Count)
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                });

                if (!flag)
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
                employEx.Gender = edit.Gender;
                employEx.WorkStatus = edit.WorkStatus;
                employEx.IdCard = edit.IdCard;
                employEx.Native= edit.Native;
                employEx.AcademicDegree=edit.AcademicDegree;
                employEx.MaritalStatus = edit.MaritalStatus;
                employEx.PositionId = edit.PositionId;
                employEx.DepartmentId= edit.DepartmentId;
                employEx.PositionLevel=edit.PositionLevel;
                employEx.Phone = edit.Phone;
                employEx.Email=edit.Email;
                employEx.BirthDay = edit.BirthDay;
                employEx.Status = edit.Status;
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
        public List<EmployeeDto.Summary> GetSenioritySummary()
        {
            using (var db = new HRM())
            {
                var senioritylist = new List<EmployeeDto.Summary>();
                var seniority = new EmployeeDto.Summary();
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
                    seniority = new EmployeeDto.Summary();                 
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
        public List<EmployeeDto.Summary> GetGradeSummary()
        {
            using (var db = new HRM())
            {
                var gradelist = new List<EmployeeDto.Summary>();
                var grade = new EmployeeDto.Summary();
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number = query.Count();
                double percent;
                var enums = EnumHelper.ToList<PositionLevel>();
                foreach (var item in enums)
                {
                    var count = query.Count(p => p.PositionLevel.ToString() == item.EnumName);
                    grade.Proportion = "0.00%";
                    grade.Category = item.Desction;
                    grade.Number = count;
                    percent = Convert.ToDouble(grade.Number) / Convert.ToDouble(number);
                    grade.Proportion = percent.ToString("0.00%");
                    gradelist.Add(grade);
                    grade = new EmployeeDto.Summary();
                }
                return gradelist;
            }
        }
        public List<EmployeeDto.Summary> GetAgeSummary()
        {
            using (var db = new HRM())
            {
                var agelist = new List<EmployeeDto.Summary>();
                var age = new EmployeeDto.Summary();
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
                    age = new EmployeeDto.Summary();
                }
                foreach(var item in query)
                {
                    if (!item.BirthDay.HasValue)
                    {
                        continue;
                    }
                    int years = DateTime.Now.Year - item.BirthDay.Value.Year;
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
        public List<EmployeeDto.Summary> GetEducationSummary()
        {
            using (var db = new HRM())
            {
                var educationlist = new List<EmployeeDto.Summary>();
                var education = new EmployeeDto.Summary();
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number = query.Count();
                double percent;
                var enums = EnumHelper.ToList<AcademicDegree>();
                foreach(var item in enums)
                {
                    var count = query.Count(p => p.AcademicDegree.ToString()==item.EnumName);
                    education.Proportion = "0.00%";
                    education.Category = item.Desction;
                    education.Number = count;
                    percent = Convert.ToDouble(education.Number) / Convert.ToDouble(number);
                    education.Proportion = percent.ToString("0.00%");
                    educationlist.Add(education);
                    education = new EmployeeDto.Summary();
                }
                return educationlist;
            }
        }
        public List<EmployeeDto.Summary> GetDepartmentSummary()
        {
            using (var db = new HRM())
            {
                var departmentlist = new List<EmployeeDto.Summary>();
                var department = new EmployeeDto.Summary();
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number = query.Count();
                double percent;
                var enums =db.Departmentes.Where(p=>p.Status==DataStatus.Enable).ToList();
                foreach (var item in enums)
                {
                    var count = query.Count(p => p.DepartmentId == item.Id);
                    department.Proportion = "0.00%";
                    department.Category = item.DepartmentName;
                    department.Number = count;
                    percent = Convert.ToDouble(department.Number) / Convert.ToDouble(number);
                    department.Proportion = percent.ToString("0.00%");
                    departmentlist.Add(department);
                    department = new EmployeeDto.Summary();
                }
                return departmentlist;
            }
        }
        public List<EmployeeDto.Summary> GetGenderSummary()
        {
            using (var db = new HRM())
            {
                var genderlist = new List<EmployeeDto.Summary>();
                var gender = new EmployeeDto.Summary();
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number = query.Count();
                double percent;
                var enums = EnumHelper.ToList<Gender>();
                foreach (var item in enums)
                {
                    var count = query.Count(p => p.Gender.ToString() == item.EnumName);
                    gender.Proportion = "0.00%";
                    gender.Category = item.Desction;
                    gender.Number = count;
                    percent = Convert.ToDouble(gender.Number) / Convert.ToDouble(number);
                    gender.Proportion = percent.ToString("0.00%");
                    genderlist.Add(gender);
                    gender = new EmployeeDto.Summary();
                }
                return genderlist;
            }
        }
        public List<EmployeeDto.Summary> GetMaritalSummary()
        {
            using (var db = new HRM())
            {
                var maritallist = new List<EmployeeDto.Summary>();
                var marital = new EmployeeDto.Summary();
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number = query.Count();
                double percent;
                var enums = EnumHelper.ToList<MaritalStatus>();
                foreach (var item in enums)
                {
                    var count = query.Count(p => p.MaritalStatus.ToString() == item.EnumName);
                    marital.Proportion = "0.00%";
                    marital.Category = item.Desction;
                    marital.Number = count;
                    percent = Convert.ToDouble(marital.Number) / Convert.ToDouble(number);
                    marital.Proportion = percent.ToString("0.00%");
                    maritallist.Add(marital);
                    marital = new EmployeeDto.Summary();
                }
                return maritallist;
            }
        }
        public List<EmployeeDto.BirthdaySummary> GetBirthdaySummary()
        {
            using (var db = new HRM())
            {
                var birthdaylist = new List<EmployeeDto.BirthdaySummary>();
                var birthday = new EmployeeDto.BirthdaySummary();
                var query = from employ in db.Employees
                            where employ.Status != DataStatus.Deleted
                            select employ;
                //总人数
                long number = query.Count();
                double percent;
                for(int i=1;i<=12;i++)
                {
                    birthday.Category = i.ToString() + "月";
                    birthday.BirthdayMonth = i;
                    birthdaylist.Add(birthday);
                    birthday= new EmployeeDto.BirthdaySummary();
                }
                foreach (var item in birthdaylist)
                {
                    var count = query.Count(p => (p.BirthDay.HasValue ? p.BirthDay.Value.Month : 0) == item.BirthdayMonth);
                    item.Proportion = "0.00%";
                    item.Number = count;
                    percent = Convert.ToDouble(item.Number) / Convert.ToDouble(number);
                    item.Proportion = percent.ToString("0.00%");
                }
                return birthdaylist;
            }
        }
        public dynamic BatchSaveEmployee(List<EmployeeDto.Employee> employees,bool createUser)
        {
            using(var db = new HRM())
            {
                var employeeSuccessCount = 0;
                var employeeErrorCount = 0;
                var userSuccessCount = 0;
                var userErrorCount = 0;
                var bindSuccessCount = 0;
                var bindErrorCount = 0;
                var flag = db.Transaction(() =>
                {
                    var employeesEx = db.Employees.ToList();
                    var positions = db.Positiones.ToList();
                    var departments = db.Departmentes.ToList();
                    var workStatus = EnumHelper.ToList<WorkStatus>();
                    var gender = EnumHelper.ToList<Gender>();
                    var maritalStatus = EnumHelper.ToList<MaritalStatus>();
                    var academicDegree = EnumHelper.ToList<AcademicDegree>();
                    var positionLevel = EnumHelper.ToList<PositionLevel>();
                    //字段处理
                    List<R_Employee> employeesR = new List<R_Employee>();
                    foreach (var employee in employees)
                    {
                        employee.Status = DataStatus.Enable;
                        employee.CreateTime = DateTime.Now;
                        employee.UpdateTime = DateTime.Now;
                        employee.WorkStatus = (WorkStatus)(workStatus.FirstOrDefault(e => e.Desction == employee.WorkStatusStr)?.EnumValue ?? 0);
                        employee.Gender = (Gender)(gender.FirstOrDefault(e => e.Desction == employee.GenderStr)?.EnumValue ?? (int)Gender.None);
                        employee.MaritalStatus = (MaritalStatus)(maritalStatus.FirstOrDefault(e => e.Desction == employee.MaritalStatusStr)?.EnumValue ?? (int)MaritalStatus.None);
                        employee.AcademicDegree = (AcademicDegree)(academicDegree.FirstOrDefault(e => e.Desction == employee.AcademicDegreeStr)?.EnumValue ?? (int)AcademicDegree.None);
                        employee.PositionId = positions.FirstOrDefault(p => p.PositionName == employee.PositionName)?.Id ?? 0L;
                        employee.DepartmentId = departments.FirstOrDefault(p => p.DepartmentName == employee.DepartmentName)?.Id ?? 0L;
                        employee.PositionLevel = (PositionLevel)(positionLevel.FirstOrDefault(e => e.Desction == employee.PositionLevelStr)?.EnumValue ?? 0);

                        //查询是否存在
                        var employeeEx = db.Employees.FirstOrDefault(u => u.WorkNum == employee.WorkNum);
                        if (employeeEx != null || 
                        string.IsNullOrEmpty(employee.WorkNum) || 
                        string.IsNullOrEmpty(employee.Name) ||
                        string.IsNullOrEmpty(employee.WorkStatusStr) ||
                        string.IsNullOrEmpty(employee.IdCard) ||
                        string.IsNullOrEmpty(employee.Phone))
                        {
                            employeeErrorCount++;
                            continue;
                        }

                        employeesR.Add(employee.MapTo<R_Employee>());
                    }
                    //保存到数据库
                    var savedEmployees = db.Employees.AddRange(employeesR);
                    db.SaveChanges();
                    employeeSuccessCount = savedEmployees.Count();
                    if (createUser)
                    {
                        //创建默认登录账号
                        List<R_User> usersR = new List<R_User>();
                        foreach(var employee in savedEmployees)
                        {
                            //查询是否存在
                            var userEx = db.Users.FirstOrDefault(u => u.LoginName == employee.WorkNum);
                            if (employee.Id == 0 || userEx != null)
                            {
                                userErrorCount++;
                                continue;
                            }
                            usersR.Add(new R_User
                            {
                                LoginName = employee.WorkNum,
                                Password = employee.WorkNum.Encrypt(),
                                Question = "我的工号是？",
                                Answer = employee.WorkNum,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                EmployeeId = employee.Id,
                            });
                        }
                        var savedUsers = db.Users.AddRange(usersR);
                        db.SaveChanges();
                        userSuccessCount = savedUsers.Count();
                        //绑定默认角色
                        var defaultRoles = db.Roles.Where(r => r.IsDefault == YesOrNo.Yes && r.Status == DataStatus.Enable).ToList();
                        if (defaultRoles != null || defaultRoles.Count > 0)
                        {
                            List<R_UserRoleRef> refsR = new List<R_UserRoleRef>();
                            foreach (var user in savedUsers)
                            {
                                if (user.Id == 0)
                                {
                                    bindErrorCount++;
                                    continue;
                                }
                                var bindList = defaultRoles.Select(r => new R_UserRoleRef
                                {
                                    UserId = user.Id,
                                    RoleId = r.Id,
                                    CreateTime = DateTime.Now,
                                    UpdateTime = DateTime.Now,
                                }).ToList();
                                refsR.AddRange(bindList);
                            }
                            var savedRefs = db.UserRoleRefs.AddRange(refsR);
                            db.SaveChanges();
                            bindSuccessCount = savedRefs.Count();
                        }         
                    }

                    return true;
                });

                return new
                {
                    employeeSuccessCount,
                    employeeErrorCount,
                    userSuccessCount,
                    userErrorCount,
                    bindSuccessCount,
                    bindErrorCount
                };
            }
        }

    }
}