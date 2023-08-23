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
    public class VacationApplyService : IVacationApplyService
    {
        public void VacationApply(VacationApplyDto.VacationApply vacationApply)
        {
            using (var db = new HRM())
            {
                if (vacationApply.EmployeeId == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "当前无法申请",
                        Status = ResponseStatus.NoPermission
                    };
                }
                if (vacationApply.Reason == "" || vacationApply.Reason.Length < 10)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请假原因不能为空且字数要在10字以上",
                        Status = ResponseStatus.ParameterError
                    };
                }
                if(vacationApply.BeginDate==null||vacationApply.EndDate==null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请选择具体的请假时间",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var vacationapplyR = vacationApply.MapTo<R_VacationApply>();
                vacationapplyR.CreateTime = DateTime.Now;
                vacationapplyR.UpdateTime = DateTime.Now;
                vacationapplyR.Status = DataStatus.Enable;
                vacationapplyR.AuditStatus=AuditStatus.Pending;
                vacationapplyR.AuditType = AuditType.DepartmentManager;
                db.VacationApplies.Add(vacationapplyR);
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "休假申请失败，正在维护",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }

        public List<VacationApplyDto.VacationApply> GetVacationApplyList(VacationApplyDto.Search search)
        {
            using(var db = new HRM())
            {
                var query = from vacationapply in db.VacationApplies
                            where vacationapply.Status != DataStatus.Deleted
                            select vacationapply;

                if(search.EmployeeId > 0)
                {
                    query = query.Where(u => u.EmployeeId == search.EmployeeId);
                }
                if (search.VacationType > 0)
                {
                    query = query.Where(u => u.VacationType == search.VacationType);
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
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<VacationApplyDto.VacationApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.VacationTypeStr = u.VacationType.Description();
                });
                return list;
            }
        }

        public void ExamineVacationApply(VacationApplyDto.Examine examine)
        {
            using(var db = new HRM())
            {
                //查询是否存在
                var vacationEx = db.VacationApplies.FirstOrDefault(u => u.Id == examine.Id);
                if (vacationEx == null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "记录存取有误,请重新选择",
                        Status = ResponseStatus.ParameterError
                    };
                }
                if (examine.AuditResult=="")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填写意见",
                        Status = ResponseStatus.ParameterError
                    };
                }
                vacationEx.AuditStatus=examine.AuditStatus;
                vacationEx.AuditResult = examine.AuditResult;
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
