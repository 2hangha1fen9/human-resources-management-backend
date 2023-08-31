using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HumanResourcesManagementBackend.Models.VacationApplyDto;

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
                //填充审核列表
                var roles = db.Roles.ToList();
                var audioList = new List<AbsenceApplyDto.Examine>
                {
                    new AbsenceApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "部门主管").Id,
                        RoleName = roles.FirstOrDefault(r => r.Name == "部门主管").Name,
                        AuditStatus = AuditStatus.Pending,
                    },
                };
                absenceapplyR.AuditNodeJson = audioList.ToJson();
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
        public List<AbsenceApplyDto.AbsenceApply> GetAbsenceApplyList(AbsenceApplyDto.Search search, UserDto.User currentusere)
        {
            using(var db = new HRM())
            {
                var query = from absenceapply in db.AbsenceApplies
                            where absenceapply.Status != DataStatus.Deleted
                            select absenceapply;

                if (search.DepartmentId > 0)
                {
                    query = from absenceapply in db.AbsenceApplies
                            join employees in db.Employees
                            on absenceapply.EmployeeId equals employees.Id
                            where absenceapply.Status != DataStatus.Deleted && employees.DepartmentId == search.DepartmentId
                            select absenceapply;
                }
                if (!string.IsNullOrEmpty(search.EmployeeName))
                {
                    query = query.Where(u => u.EmployeeId == (db.Employees.FirstOrDefault(p => p.Name == search.EmployeeName).Id));
                }
                if (search.EmployeeId > 0)
                {
                    query = query.Where(u => u.EmployeeId == search.EmployeeId);
                }
                if (search.CheckInType>0)
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
                    u.DepartmentName = db.Departmentes.FirstOrDefault(p => p.Id == (db.Employees.FirstOrDefault(x => x.Id == u.EmployeeId).DepartmentId)).DepartmentName;
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.CheckInTypeStr = u.CheckInType.Description();
                    u.AuditNode = u.AuditNodeJson.ToObject<List<AbsenceApplyDto.Examine>>();
                    u.AuditNode?.ForEach(a =>
                    {
                        a.AuditStatusStr = a.AuditStatus.Description();
                    });
                });

                //过滤能审核的
                var refs = db.UserRoleRefs.Where(r => r.UserId == currentusere.Id).ToList();
                list = list.Where(l =>
                {
                    var firstNode = l.AuditNode.Where(c => c.AuditStatus == AuditStatus.Pending).FirstOrDefault();
                    if (firstNode == null || refs.FirstOrDefault(r => r.RoleId == firstNode.RoleId) == null)
                    {
                        return false;
                    }

                    return true;
                }).ToList();
                return list;
            }
        }

        public List<AbsenceApplyDto.AbsenceApply> GetMyAbsenceApplyList(AbsenceApplyDto.Search search)
        {
            using (var db = new HRM())
            {
                var query = from absenceapply in db.AbsenceApplies
                            where absenceapply.Status != DataStatus.Deleted
                            select absenceapply;
                if (search.EmployeeId > 0)
                {
                    query = query.Where(u => u.EmployeeId == search.EmployeeId);
                }
                if (search.CheckInType > 0)
                {
                    query = query.Where(u => u.CheckInType == search.CheckInType);
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
                    u.AuditNode = u.AuditNodeJson.ToObject<List<AbsenceApplyDto.Examine>>();
                    u.AuditNode?.ForEach(a =>
                    {
                        a.AuditStatusStr = a.AuditStatus.Description();
                    });
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
                absence.AuditNode = absence.AuditNodeJson.ToObject<List<AbsenceApplyDto.Examine>>();
                absence.AuditNode?.ForEach(a =>
                {
                    a.AuditStatusStr = a.AuditStatus.Description();
                });
                absence.StatusStr = absence.Status.Description();
                absence.AuditStatusStr = absence.AuditStatus.Description();
                absence.AuditTypeStr = absence.AuditType.Description();
                absence.CheckInTypeStr = absence.CheckInType.Description();
                return absence;
            }
        }
        public void ExamineAbsenceApply(AbsenceApplyDto.Examine examine, UserDto.User currentusere)
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
                //获取审核列表
                var auditNodeList = absenceEx.AuditNodeJson.ToObject<List<AbsenceApplyDto.Examine>>().ToList();
                //开始审核
                if (auditNodeList != null)
                {
                    var audit = auditNodeList.FirstOrDefault(u => u.AuditStatus == AuditStatus.Pending);
                    if (audit == null)
                    {
                        throw new BusinessException
                        {
                            ErrorMessage = "审核出现错误,请联系管理员",
                            Status = ResponseStatus.NoPermission
                        };
                    }

                    //查询当前用户能不能审核
                    var canAudit = db.UserRoleRefs.FirstOrDefault(u => u.UserId == currentusere.Id && u.RoleId == audit.RoleId);
                    if (canAudit == null)
                    {
                        throw new BusinessException
                        {
                            ErrorMessage = "当前用户不能审核",
                            Status = ResponseStatus.NoPermission
                        };
                    }
                    if (absenceEx.AuditStatus != AuditStatus.Pending)
                    {
                        throw new BusinessException
                        {
                            ErrorMessage = "该申请已审核",
                            Status = ResponseStatus.ParameterError
                        };
                    }
                    //填入审核结果
                    audit.UserId = currentusere.Id;
                    audit.UserName = currentusere.LoginName;
                    audit.AuditStatus = examine.AuditStatus;
                    audit.AuditResult = examine.AuditResult;
                    audit.AuditTime = DateTime.Now;
                    //更新审核节点列表
                    absenceEx.AuditNodeJson = auditNodeList.ToJson();
                    //如果是最后一个节点,或者拒绝结束整个流程
                    if (auditNodeList.LastOrDefault().RoleId == audit.RoleId || audit.AuditStatus == AuditStatus.Reject)
                    {
                        absenceEx.AuditStatus = examine.AuditStatus;
                        absenceEx.AuditResult = examine.AuditResult;
                    }
                }
                else
                {
                    //流程为空直接通过
                    absenceEx.AuditStatus = examine.AuditStatus;
                    absenceEx.AuditResult = examine.AuditResult;
                }

                absenceEx.UpdateTime = DateTime.Now;
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
