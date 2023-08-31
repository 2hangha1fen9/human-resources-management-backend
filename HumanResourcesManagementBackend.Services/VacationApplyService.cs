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
                vacationapplyR.AuditType = AuditType.GeneralManager;
                //填充审核列表
                var roles = db.Roles.ToList();
                var audioList = new List<VacationApplyDto.Examine>
                {
                    new VacationApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "部门主管").Id,
                        AuditStatus = AuditStatus.Pending,
                    },
                    new VacationApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "校区主任").Id,
                        AuditStatus = AuditStatus.Pending,
                    }
                };
                vacationapplyR.AuditNodeJson= audioList.ToJson();

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

        public List<VacationApplyDto.VacationApply> GetVacationApplyList(VacationApplyDto.Search search, UserDto.User currentUser)
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
                    u.AuditNode = u.AuditNodeJson.ToObject<List<VacationApplyDto.Examine>>();
                    u.AuditNode.ForEach(a =>
                    {
                        a.AuditStatusStr = a.AuditStatus.Description();
                    });
                });
                //过滤能审核的
                var refs = db.UserRoleRefs.Where(r => r.UserId == currentUser.Id).ToList();
                list = list.Where(l =>
                {
                    var firstNode = l.AuditNode.FirstOrDefault();
                    if(refs.FirstOrDefault(r => r.RoleId == firstNode.RoleId) == null)
                    {
                        return false;
                    }

                    return true;
                }).ToList();

                return list;
            }
        }

        public void ExamineVacationApply(VacationApplyDto.Examine examine,UserDto.User currentUser)
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
                //获取审核列表
                var auditNodeList = vacationEx.AuditNodeJson.ToObject<List<VacationApplyDto.Examine>>().Where(a => a.AuditStatus == AuditStatus.Pending);
                var audit = auditNodeList.FirstOrDefault();
                //开始审核
                if (audit != null)
                {
                    //查询当前用户能不能审核
                    var canAudit = db.UserRoleRefs.FirstOrDefault(u => u.UserId == currentUser.Id && u.RoleId == audit.RoleId);
                    if (canAudit == null)
                    {
                        throw new BusinessException
                        {
                            ErrorMessage = "当前用户不能审核",
                            Status = ResponseStatus.NoPermission
                        };
                    }
                    //填入审核结果
                    audit.UserId = currentUser.Id;
                    audit.UserName = currentUser.LoginName;
                    audit.AuditStatus = examine.AuditStatus;
                    audit.AuditResult = examine.AuditResult;
                    //更新审核节点列表
                    vacationEx.AuditNodeJson = auditNodeList.ToJson();
                    //如果是最后一个节点结束整个流程
                    if(auditNodeList.LastOrDefault()?.RoleId == audit.RoleId)
                    {
                        vacationEx.AuditStatus = examine.AuditStatus;
                        vacationEx.AuditResult = examine.AuditResult;
                    }
                }
                else
                {
                    //流程为空直接通过
                    vacationEx.AuditStatus = examine.AuditStatus;
                    vacationEx.AuditResult = examine.AuditResult;
                }

                vacationEx.UpdateTime = DateTime.Now;
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "审核出现错误,请联系管理员",
                        Status = ResponseStatus.UpdateError
                    };
                }
            }
        }
    }
}
