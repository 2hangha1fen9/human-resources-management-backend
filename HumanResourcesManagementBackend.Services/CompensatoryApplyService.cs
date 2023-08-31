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
    public class CompensatoryApplyService:ICompensatoryApplyService
    {
        public void CompensatoryApply(CompensatoryApplyDto.CompensatoryApply compensatoryApply)
        {
            using (var db = new HRM())
            {
                if (compensatoryApply.EmployeeId == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "当前无法申请",
                        Status = ResponseStatus.NoPermission
                    };
                }

                if (compensatoryApply.WorkDate == null || compensatoryApply.RestDate == null || compensatoryApply.WorkPlan == "")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填入具体的参数",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var compensatoryapplyR = compensatoryApply.MapTo<R_CompensatoryApply>();
                compensatoryapplyR.CreateTime = DateTime.Now;
                compensatoryapplyR.UpdateTime = DateTime.Now;
                compensatoryapplyR.Status = DataStatus.Enable;
                compensatoryapplyR.AuditStatus = AuditStatus.Pending;
                compensatoryapplyR.AuditType = AuditType.DepartmentManager;
                //填充审核列表
                var roles = db.Roles.ToList();
                var audioList = new List<CompensatoryApplyDto.Examine>
                {
                    new CompensatoryApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "部门主管").Id,
                        AuditStatus = AuditStatus.Pending,
                        RoleName = roles.FirstOrDefault(r => r.Name == "部门主管").Name,
                    },
                };
                compensatoryapplyR.AuditNodeJson = audioList.ToJson();
                db.CompensatoryApplies.Add(compensatoryapplyR);
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "调休申请失败，正在维护",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }
        public List<CompensatoryApplyDto.CompensatoryApply> QueryCompensatoryListByPage(CompensatoryApplyDto.Search search, UserDto.User currentUser)
        {
            using (var db = new HRM())
            {
                var query = from compnesatoryapply in db.CompensatoryApplies
                            where compnesatoryapply.Status != DataStatus.Deleted
                            select compnesatoryapply;
                if (search.DepartmentId > 0)
                {
                    query = from compnesatoryapply in db.CompensatoryApplies
                            join employees in db.Employees
                            on compnesatoryapply.EmployeeId equals employees.Id
                            where compnesatoryapply.Status != DataStatus.Deleted && employees.DepartmentId == search.DepartmentId
                            select compnesatoryapply;
                }
                if (!string.IsNullOrEmpty(search.EmployeeName))
                {
                    query = query.Where(u => u.EmployeeId == (db.Employees.FirstOrDefault(p => p.Name == search.EmployeeName).Id));
                }
                if (search.EmployeeId > 0)
                {
                    query = query.Where(u => u.EmployeeId == search.EmployeeId);
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
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<CompensatoryApplyDto.CompensatoryApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.EmployeeName = db.Employees.FirstOrDefault(p => p.Id == u.EmployeeId).Name;
                    u.DepartmentName = db.Departmentes.FirstOrDefault(p => p.Id == (db.Employees.FirstOrDefault(x => x.Id == u.EmployeeId).DepartmentId)).DepartmentName;
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.AuditNode = u.AuditNodeJson.ToObject<List<CompensatoryApplyDto.Examine>>();
                    u.AuditNode?.ForEach(a =>
                    {
                        a.AuditStatusStr = a.AuditStatus.Description();
                    });
                });
                //过滤能审核的
                var refs = db.UserRoleRefs.Where(r => r.UserId == currentUser.Id).ToList();
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
        public List<CompensatoryApplyDto.CompensatoryApply> QueryMyCompensatoryListByPage(CompensatoryApplyDto.Search search)
        {
            using(var db = new HRM())
            {
                var query = from compnesatoryapply in db.CompensatoryApplies
                            where compnesatoryapply.Status != DataStatus.Deleted
                            select compnesatoryapply;
                if (search.EmployeeId > 0)
                {
                    query = query.Where(u => u.EmployeeId == search.EmployeeId);
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
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<CompensatoryApplyDto.CompensatoryApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.AuditNode = u.AuditNodeJson.ToObject<List<CompensatoryApplyDto.Examine>>();
                    u.AuditNode?.ForEach(a =>
                    {
                        a.AuditStatusStr = a.AuditStatus.Description();
                    });
                });
                return list;
            }
        }
        public CompensatoryApplyDto.CompensatoryApply GetCompensatoryById(long id)
        {
            using (var db = new HRM())
            {
                var compensatoryR = db.CompensatoryApplies.FirstOrDefault(u => u.Id == id && u.Status != DataStatus.Deleted);
                if (compensatoryR == null)
                {
                    throw new BusinessException
                    {
                        Status = ResponseStatus.NoData,
                        ErrorMessage = ResponseStatus.NoData.Description()
                    };
                }
                var compensatory = compensatoryR.MapTo<CompensatoryApplyDto.CompensatoryApply>();
                compensatory.StatusStr = compensatory.Status.Description();
                compensatory.AuditStatusStr = compensatory.AuditStatus.Description();
                compensatory.AuditTypeStr = compensatory.AuditType.Description();
                compensatory.AuditNode = compensatory.AuditNodeJson.ToObject<List<CompensatoryApplyDto.Examine>>();
                compensatory.AuditNode?.ForEach(a =>
                {
                    a.AuditStatusStr = a.AuditStatus.Description();
                });
                return compensatory;
            }
        }
        public void ExamineCompensatoryApply(CompensatoryApplyDto.Examine examine, UserDto.User currentuser)
        {
            using (var db = new HRM())
            {
                //查询是否存在
                var compensatoryEx = db.CompensatoryApplies.FirstOrDefault(u => u.Id == examine.Id);
                if (compensatoryEx == null)
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
                var auditNodeList = compensatoryEx.AuditNodeJson.ToObject<List<CompensatoryApplyDto.Examine>>().ToList();
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
                    var canAudit = db.UserRoleRefs.FirstOrDefault(u => u.UserId == currentuser.Id && u.RoleId == audit.RoleId);
                    if (canAudit == null)
                    {
                        throw new BusinessException
                        {
                            ErrorMessage = "当前用户不能审核",
                            Status = ResponseStatus.NoPermission
                        };
                    }
                    //填入审核结果
                    audit.UserId = currentuser.Id;
                    audit.UserName = currentuser.LoginName;
                    audit.AuditStatus = examine.AuditStatus;
                    audit.AuditResult = examine.AuditResult;
                    audit.AuditTime = DateTime.Now;
                    //更新审核节点列表
                    compensatoryEx.AuditNodeJson = auditNodeList.ToJson();
                    //如果是最后一个节点结束整个流程
                    if (auditNodeList.LastOrDefault().RoleId == audit.RoleId)
                    {
                        compensatoryEx.AuditStatus = examine.AuditStatus;
                        compensatoryEx.AuditResult = examine.AuditResult;
                    }
                }
                else
                {
                    //流程为空直接通过
                    compensatoryEx.AuditStatus = examine.AuditStatus;
                    compensatoryEx.AuditResult = examine.AuditResult;
                }

                compensatoryEx.UpdateTime = DateTime.Now;
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
