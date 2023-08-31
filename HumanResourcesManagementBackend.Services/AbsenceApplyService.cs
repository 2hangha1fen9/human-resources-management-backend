using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services
{
    public class AbsenceApplyService:IAbsenceApplyService
    {
        public void AbsenceApply(AbsenceApplyDto.AbsenceApply absenceApply)
        {
            using (var db = new HRM())
            {
                if(absenceApply.EmployeeId == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "当前无法申请",
                        Status = ResponseStatus.NoPermission
                    };
                }

                if (absenceApply.AbsenceDateTime == null || absenceApply.CheckInType == 0 ||
                    absenceApply.Reason == "" || absenceApply.Prover == "")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填入具体的参数",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var absenceapplyR = absenceApply.MapTo<R_AbsenceApply>();
                absenceapplyR.CreateTime = DateTime.Now;
                absenceapplyR.UpdateTime = DateTime.Now;
                absenceapplyR.Status = DataStatus.Enable;
                absenceapplyR.AuditStatus = AuditStatus.Pending;
                absenceapplyR.AuditType = AuditType.DepartmentManager;
                db.AbsenceApplies.Add(absenceapplyR);
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "缺勤申请失败，正在维护",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }
        public List<AbsenceApplyDto.AbsenceApply> GetAbsenceApplyList(AbsenceApplyDto.Search search)
        {
            using(var db = new HRM())
            {
                var query = from absenceapply in db.AbsenceApplies
                            where absenceapply.Status != DataStatus.Deleted
                            select absenceapply;
                if (search.EmployeeId > 0)
                {
                    query = query.Where(u => u.EmployeeId == search.EmployeeId);
                }
                if(search.CreateTime !=DateTime.Parse("0001 / 1 / 1 0:00:00"))
                {
                    query = query.Where(u => u.CreateTime.Year == search.CreateTime.Year 
                    && u.CreateTime.Month == search.CreateTime.Month && u.CreateTime.Day == search.CreateTime.Day);
                }
                if(search.CheckInType>0)
                {
                    query=query.Where(u=>u.CheckInType== search.CheckInType);
                }
                if (search.AuditType > 0)
                {
                    query = query.Where(u => u.AuditType == search.AuditType);
                }
                if (search.AuditStatus > 0)
                {
                    query = query.Where(u => u.AuditStatus == search.AuditStatus);
                }

                //分页并将数据库实体映射为dto对象(OrderBy必须调用)
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<AbsenceApplyDto.AbsenceApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.EmployeeName = db.Employees.FirstOrDefault(p => p.Id == u.EmployeeId).Name;
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.CheckInTypeStr = u.CheckInType.Description();
                });
                return list;
            }
        }

        public AbsenceApplyDto.AbsenceApply GetAbsenceById(long id)
        {
            using (var db = new HRM())
            {
                var absenceR = db.AbsenceApplies.FirstOrDefault(u => u.Id == id && u.Status != DataStatus.Deleted);
                if (absenceR == null)
                {
                    throw new BusinessException
                    {
                        Status = ResponseStatus.NoData,
                        ErrorMessage = ResponseStatus.NoData.Description()
                    };
                }
                var absence = absenceR.MapTo<AbsenceApplyDto.AbsenceApply>();
                absence.StatusStr = absence.Status.Description();
                absence.AuditStatusStr = absence.AuditStatus.Description();
                absence.AuditTypeStr = absence.AuditType.Description();
                absence.CheckInTypeStr = absence.CheckInType.Description();
                return absence;
            }
        }
        public void ExamineAbsenceApply(AbsenceApplyDto.Examine examine)
        {
            using (var db = new HRM())
            {
                //查询是否存在
                var absenceEx = db.AbsenceApplies.FirstOrDefault(u => u.Id == examine.Id);
                if (absenceEx == null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "记录存取有误,请重新选择",
                        Status = ResponseStatus.ParameterError
                    };
                }
                if (examine.AuditResult == "")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填写意见",
                        Status = ResponseStatus.ParameterError
                    };
                }
                absenceEx.AuditStatus = examine.AuditStatus;
                absenceEx.AuditResult = examine.AuditResult;
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "审核出现错误,请联系管理员",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }
    }
}
