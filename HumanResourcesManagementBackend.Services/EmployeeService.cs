using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Repository.Migrations;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using static HumanResourcesManagementBackend.Models.UserDto;
using static System.Data.Entity.Migrations.Model.UpdateDatabaseOperation;

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
        public List<EmployeeDto.SummaryDto> GetSenioritySummary()
        {
            using (var db = new HRM())
            {
                var senioritylist = new List<EmployeeDto.SummaryDto>();
                var seniority = new EmployeeDto.SummaryDto();
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
                    seniority = new EmployeeDto.SummaryDto();                 
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
        public List<EmployeeDto.SummaryDto> GetGradeSummary()
        {
            using (var db = new HRM())
            {
                var gradelist = new List<EmployeeDto.SummaryDto>();
                var grade = new EmployeeDto.SummaryDto();
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
                    grade = new EmployeeDto.SummaryDto();
                }
                return gradelist;
            }
        }
        public List<EmployeeDto.SummaryDto> GetAgeSummary()
        {
            using (var db = new HRM())
            {
                var agelist = new List<EmployeeDto.SummaryDto>();
                var age = new EmployeeDto.SummaryDto();
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
                    age = new EmployeeDto.SummaryDto();
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
        public List<EmployeeDto.SummaryDto> GetEducationSummary()
        {
            using (var db = new HRM())
            {
                var educationlist = new List<EmployeeDto.SummaryDto>();
                var education = new EmployeeDto.SummaryDto();
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
                    education = new EmployeeDto.SummaryDto();
                }
                return educationlist;
            }
        }
        public List<EmployeeDto.SummaryDto> GetDepartmentSummary()
        {
            using (var db = new HRM())
            {
                var departmentlist = new List<EmployeeDto.SummaryDto>();
                var department = new EmployeeDto.SummaryDto();
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
                    department = new EmployeeDto.SummaryDto();
                }
                return departmentlist;
            }
        }
        public List<EmployeeDto.SummaryDto> GetGenderSummary()
        {
            using (var db = new HRM())
            {
                var genderlist = new List<EmployeeDto.SummaryDto>();
                var gender = new EmployeeDto.SummaryDto();
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
                    gender = new EmployeeDto.SummaryDto();
                }
                return genderlist;
            }
        }
        public List<EmployeeDto.SummaryDto> GetMaritalSummary()
        {
            using (var db = new HRM())
            {
                var maritallist = new List<EmployeeDto.SummaryDto>();
                var marital = new EmployeeDto.SummaryDto();
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
                    marital = new EmployeeDto.SummaryDto();
                }
                return maritallist;
            }
        }
        public List<EmployeeDto.BirthdaySummaryDto> GetBirthdaySummary()
        {
            using (var db = new HRM())
            {
                var birthdaylist = new List<EmployeeDto.BirthdaySummaryDto>();
                var birthday = new EmployeeDto.BirthdaySummaryDto();
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
                    birthday= new EmployeeDto.BirthdaySummaryDto();
                }
                foreach (var item in birthdaylist)
                {
                    var count = query.Count(p => p.BirthDay.Month == item.BirthdayMonth);
                    item.Proportion = "0.00%";
                    item.Number = count;
                    percent = Convert.ToDouble(item.Number) / Convert.ToDouble(number);
                    item.Proportion = percent.ToString("0.00%");
                }
                return birthdaylist;
            }
        }


        //public void ReadExcel(string fileName, string sheetName, bool isFirstRowColumn)
        //{
        //    DataTable dt = ExcelToDatatable(fileName, sheetName, isFirstRowColumn);
        //    //将excel表格数据存入list集合中
        //    //EachdayTX定义的类，字段值对应excel表中的每一列
        //    List<EmployeeDto.Employee> employTX = new List<EmployeeDto.Employee>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        EmployeeDto.Employee employ = new EmployeeDto.Employee
        //        {
        //            Id = (long)dr[0],
        //            WorkNum = dr[1].ToString(),
        //            WorkStatus = (WorkStatus)dr[2],
        //            Name = dr[3].ToString(),
        //            Gender = (Gender)dr[4],
        //            MaritalStatus = (MaritalStatus)dr[5],
        //            BirthDay = (DateTime)dr[6],
        //            IdCard = dr[7].ToString(),
        //            Native = dr[8].ToString(),
        //            Phone = dr[9].ToString(),
        //            Email = dr[10].ToString(),
        //            AcademicDegree = (AcademicDegree)dr[11],
        //            HireDate= (DateTime)dr[12],
        //            PositionId = (long)dr[13],
        //            Status = (DataStatus)dr[1],
        //            CreateTime = DateTime.Now,
        //            UpdateTime= DateTime.Now,


        //        };
        //        employTX.Add(employ);
        //    }
        //}
        //public static DataTable ExcelToDatatable(string fileName, string sheetName, bool isFirstRowColumn)
        //{
        //    ISheet sheet = null;
        //    DataTable data = new DataTable();
        //    int startRow = 0;
        //    FileStream fs;
        //    IWorkbook workbook = null;
        //    int cellCount = 0;//列数
        //    int rowCount = 0;//行数
        //    try
        //    {
        //        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        //        if (fileName.IndexOf(".xlsx") > 0) // 2007版本
        //        {
        //            workbook = new XSSFWorkbook(fs);
        //        }
        //        else if (fileName.IndexOf(".xls") > 0) // 2003版本
        //        {
        //            workbook = new HSSFWorkbook(fs);
        //        }
        //        if (sheetName != null)
        //        {
        //            sheet = workbook.GetSheet(sheetName);//根据给定的sheet名称获取数据
        //        }
        //        else
        //        {
        //            //也可以根据sheet编号来获取数据
        //            sheet = workbook.GetSheetAt(0);//获取第几个sheet表（此处表示如果没有给定sheet名称，默认是第一个sheet表）  
        //        }
        //        if (sheet != null)
        //        {
        //            IRow firstRow = sheet.GetRow(0);
        //            cellCount = firstRow.LastCellNum; //第一行最后一个cell的编号 即总的列数
        //            if (isFirstRowColumn)//如果第一行是标题行
        //            {
        //                for (int i = firstRow.FirstCellNum; i < cellCount; ++i)//第一行列数循环
        //                {
        //                    DataColumn column = new DataColumn(firstRow.GetCell(i).StringCellValue);//获取标题
        //                    data.Columns.Add(column);//添加列
        //                }
        //                startRow = sheet.FirstRowNum + 1;//1（即第二行，第一行0从开始）
        //            }
        //            else
        //            {
        //                startRow = sheet.FirstRowNum;//0
        //            }
        //            //最后一行的标号
        //            rowCount = sheet.LastRowNum;
        //            for (int i = startRow; i <= rowCount; ++i)//循环遍历所有行
        //            {
        //                IRow row = sheet.GetRow(i);//第几行
        //                if (row == null)
        //                {
        //                    continue; //没有数据的行默认是null;
        //                }
        //                //将excel表每一行的数据添加到datatable的行中
        //                DataRow dataRow = data.NewRow();
        //                for (int j = row.FirstCellNum; j < cellCount; ++j)
        //                {
        //                    if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
        //                    {
        //                        dataRow[j] = row.GetCell(j).ToString();
        //                    }
        //                }
        //                data.Rows.Add(dataRow);
        //            }
        //        }
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception: " + ex.Message);
        //        return null;
        //    }
        //}
    }
}