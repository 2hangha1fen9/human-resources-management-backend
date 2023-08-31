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
    public class FieldWorkApplyService:IFieldWorkApplyService
    {
        public void FieldWorkApply(FieldWorkApplyDto.FieldWorkApply fieldWorkApply)
        {
            using (var db = new HRM())
            {
                if (fieldWorkApply.EmployeeId == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "当前无法申请",
                        Status = ResponseStatus.NoPermission
                    };
                }

                if (fieldWorkApply.BeginDate == null || fieldWorkApply.EndDate == null ||
                    fieldWorkApply.Address == "" || fieldWorkApply.Reason == "")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填入具体的参数",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var fieldworkapplyR = fieldWorkApply.MapTo<R_FieldWorkApply>();
                fieldworkapplyR.CreateTime = DateTime.Now;
                fieldworkapplyR.UpdateTime = DateTime.Now;
                fieldworkapplyR.Status = DataStatus.Enable;
                fieldworkapplyR.AuditStatus = AuditStatus.Pending;
                fieldworkapplyR.AuditType = AuditType.DepartmentManager;
                //填充审核列表
                var roles = db.Roles.ToList();
                var audioList = new List<FieldWorkApplyDto.Examine>
                {
                    new FieldWorkApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "部门主管").Id,
                        AuditStatus = AuditStatus.Pending,
                        RoleName = roles.FirstOrDefault(r => r.Name == "部门主管").Name,
                    },
                    new FieldWorkApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "校区人事专员").Id,
                        AuditStatus = AuditStatus.Pending,
                        RoleName = roles.FirstOrDefault(r => r.Name == "校区人事专员").Name,
                    },
                    new FieldWorkApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "校区主任").Id,
                        AuditStatus = AuditStatus.Pending,
                        RoleName = roles.FirstOrDefault(r => r.Name == "校区主任").Name,
                    }
                };
                fieldworkapplyR.AuditNodeJson = audioList.ToJson();
                db.FieldWorkApplies.Add(fieldworkapplyR);
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "外勤申请失败，正在维护",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }
        public List<FieldWorkApplyDto.FieldWorkApply> QueryFieldWorkListByPage(FieldWorkApplyDto.Search search, UserDto.User currentUser)
        {
            using (var db = new HRM())
            {
                var query = from fieldworkapply in db.FieldWorkApplies
                            where fieldworkapply.Status != DataStatus.Deleted
                            select fieldworkapply;
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
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<FieldWorkApplyDto.FieldWorkApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.Duration = DateHelper.GetDateLength(u.BeginDate, u.EndDate);
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.AuditNode = u.AuditNodeJson.ToObject<List<FieldWorkApplyDto.Examine>>();
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
                    if (refs.FirstOrDefault(r => r.RoleId == firstNode.RoleId) == null)
                    {
                        return false;
                    }

                    return true;
                }).ToList();
                return list;
            }
        }
        public List<FieldWorkApplyDto.FieldWorkApply> QueryMyFieldWorkListByPage(FieldWorkApplyDto.Search search)
        {
            using (var db = new HRM())
            {
                var query = from fieldworkapply in db.FieldWorkApplies
                            where fieldworkapply.Status != DataStatus.Deleted
                            select fieldworkapply;
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
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<FieldWorkApplyDto.FieldWorkApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.Duration = DateHelper.GetDateLength(u.BeginDate, u.EndDate);
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.AuditNode = u.AuditNodeJson.ToObject<List<FieldWorkApplyDto.Examine>>();
                    u.AuditNode?.ForEach(a =>
                    {
                        a.AuditStatusStr = a.AuditStatus.Description();
                    });
                });
                return list;
            }
        }
        public FieldWorkApplyDto.FieldWorkApply GetFieldWorkById(long id)
        {
            using (var db = new HRM())
            {
                var fieldworkR = db.FieldWorkApplies.FirstOrDefault(u => u.Id == id && u.Status != DataStatus.Deleted);
                if (fieldworkR == null)
                {
                    throw new BusinessException
                    {
                        Status = ResponseStatus.NoData,
                        ErrorMessage = ResponseStatus.NoData.Description()
                    };
                }
                var fieldwork = fieldworkR.MapTo<FieldWorkApplyDto.FieldWorkApply>();
                fieldwork.Duration = DateHelper.GetDateLength(fieldwork.BeginDate, fieldwork.EndDate);
                fieldwork.StatusStr = fieldwork.Status.Description();
                fieldwork.AuditStatusStr = fieldwork.AuditStatus.Description();
                fieldwork.AuditTypeStr = fieldwork.AuditType.Description();
                fieldwork.AuditNode = fieldwork.AuditNodeJson.ToObject<List<FieldWorkApplyDto.Examine>>();
                fieldwork.AuditNode?.ForEach(a =>
                {
                    a.AuditStatusStr = a.AuditStatus.Description();
                });
                return fieldwork;
            }
        }
        public void ExamineFieldWorkApply(FieldWorkApplyDto.Examine examine, UserDto.User currentuser)
        {
            using (var db = new HRM())
            {
                //查询是否存在
                var fieldworkEx = db.FieldWorkApplies.FirstOrDefault(u => u.Id == examine.Id);
                if (fieldworkEx == null)
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
                var auditNodeList = fieldworkEx.AuditNodeJson.ToObject<List<FieldWorkApplyDto.Examine>>().Where(a => a.AuditStatus == AuditStatus.Pending).ToList();
                //开始审核
                if (auditNodeList != null)
                {
                    var audit = auditNodeList.FirstOrDefault();
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
                    fieldworkEx.AuditNodeJson = auditNodeList.ToJson();
                    //如果是最后一个节点结束整个流程
                    if (auditNodeList.LastOrDefault().RoleId == audit.RoleId)
                    {
                        fieldworkEx.AuditStatus = examine.AuditStatus;
                        fieldworkEx.AuditResult = examine.AuditResult;
                    }
                }
                else
                {
                    //流程为空直接通过
                    fieldworkEx.AuditStatus = examine.AuditStatus;
                    fieldworkEx.AuditResult = examine.AuditResult;
                }

                fieldworkEx.UpdateTime = DateTime.Now;
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
