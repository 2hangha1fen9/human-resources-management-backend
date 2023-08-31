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
    public class BusinessTripApplyService:IBusinessTripApplyService
    {
        public void BusinessTripApply(BusinessTripApplyDto.BusinessTripApply businessTripApply)
        {
            using (var db = new HRM())
            {
                if (businessTripApply.EmployeeId == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "当前无法申请",
                        Status = ResponseStatus.NoPermission
                    };
                }

                if (businessTripApply.Address == "" || businessTripApply.Reason == "" || businessTripApply.Result == "" ||
                    businessTripApply.BeginDate == null || businessTripApply.EndDate == null || businessTripApply.Support == "")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填入具体的参数",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var businesstripapplyR = businessTripApply.MapTo<R_BusinessTripApply>();
                businesstripapplyR.CreateTime = DateTime.Now;
                businesstripapplyR.UpdateTime = DateTime.Now;
                businesstripapplyR.Status = DataStatus.Enable;
                businesstripapplyR.AuditStatus = AuditStatus.Pending;
                businesstripapplyR.AuditType = AuditType.DepartmentManager;
                //填充审核列表
                var roles = db.Roles.ToList();
                var audioList = new List<BusinessTripApplyDto.Examine>
                {
                    new BusinessTripApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "部门主管").Id,
                        AuditStatus = AuditStatus.Pending,
                        RoleName = roles.FirstOrDefault(r => r.Name == "部门主管").Name,
                    },
                    new BusinessTripApplyDto.Examine
                    {
                        RoleId = roles.FirstOrDefault(r => r.Name == "校区主任").Id,
                        AuditStatus = AuditStatus.Pending,
                        RoleName = roles.FirstOrDefault(r => r.Name == "校区主任").Name,
                    }
                };
                businesstripapplyR.AuditNodeJson = audioList.ToJson();
                db.BusinessTripApplies.Add(businesstripapplyR);
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "出差申请失败，正在维护",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }
        public List<BusinessTripApplyDto.BusinessTripApply> QueryBusinessTripListByPage(BusinessTripApplyDto.Search search, UserDto.User currentUser)
        {
            using (var db = new HRM())
            {
                var query = from businesstripapply in db.BusinessTripApplies
                            where businesstripapply.Status != DataStatus.Deleted
                            select businesstripapply;
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
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<BusinessTripApplyDto.BusinessTripApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.Duration = DateHelper.GetDateLength(u.BeginDate, u.EndDate);
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.AuditNode = u.AuditNodeJson.ToObject<List<BusinessTripApplyDto.Examine>>();
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
        public List<BusinessTripApplyDto.BusinessTripApply> QueryMyBusinessTripListByPage(BusinessTripApplyDto.Search search)
        {
            using (var db = new HRM())
            {
                var query = from businesstripapply in db.BusinessTripApplies
                            where businesstripapply.Status != DataStatus.Deleted
                            select businesstripapply;
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
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<BusinessTripApplyDto.BusinessTripApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.Duration = DateHelper.GetDateLength(u.BeginDate,u.EndDate);
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.AuditNode = u.AuditNodeJson.ToObject<List<BusinessTripApplyDto.Examine>>();
                    u.AuditNode?.ForEach(a =>
                    {
                        a.AuditStatusStr = a.AuditStatus.Description();
                    });
                });
                return list;
            }
        }
        public BusinessTripApplyDto.BusinessTripApply GetBusinessTripById(long id)
        {
            using (var db = new HRM())
            {
                var businesstripR = db.BusinessTripApplies.FirstOrDefault(u => u.Id == id && u.Status != DataStatus.Deleted);
                if (businesstripR == null)
                {
                    throw new BusinessException
                    {
                        Status = ResponseStatus.NoData,
                        ErrorMessage = ResponseStatus.NoData.Description()
                    };
                }
                var businesstrip = businesstripR.MapTo<BusinessTripApplyDto.BusinessTripApply>();
                businesstrip.Duration = DateHelper.GetDateLength(businesstrip.BeginDate, businesstrip.EndDate);
                businesstrip.StatusStr = businesstrip.Status.Description();
                businesstrip.AuditStatusStr = businesstrip.AuditStatus.Description();
                businesstrip.AuditTypeStr = businesstrip.AuditType.Description();
                return businesstrip;
            }
        }
        public void ExamineBusinessTripApply(BusinessTripApplyDto.Examine examine, UserDto.User currentuser)
        {
            using (var db = new HRM())
            {
                //查询是否存在
                var businesstripEx = db.BusinessTripApplies.FirstOrDefault(u => u.Id == examine.Id);
                if (businesstripEx == null)
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
                var auditNodeList = businesstripEx.AuditNodeJson.ToObject<List<BusinessTripApplyDto.Examine>>().Where(a => a.AuditStatus == AuditStatus.Pending).ToList();
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
                    //更新审核节点列表
                    businesstripEx.AuditNodeJson = auditNodeList.ToJson();
                    //如果是最后一个节点结束整个流程
                    if (auditNodeList.LastOrDefault().RoleId == audit.RoleId)
                    {
                        businesstripEx.AuditStatus = examine.AuditStatus;
                        businesstripEx.AuditResult = examine.AuditResult;
                    }
                }
                else
                {
                    //流程为空直接通过
                    businesstripEx.AuditStatus = examine.AuditStatus;
                    businesstripEx.AuditResult = examine.AuditResult;
                }

                businesstripEx.UpdateTime = DateTime.Now;
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
