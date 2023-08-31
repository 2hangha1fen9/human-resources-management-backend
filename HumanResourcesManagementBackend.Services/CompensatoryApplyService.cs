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
                if (search.CreateTime != DateTime.Parse("0001 / 1 / 1 0:00:00"))
                {
                    query = query.Where(u => u.CreateTime.Year == search.CreateTime.Year
                    && u.CreateTime.Month == search.CreateTime.Month && u.CreateTime.Day == search.CreateTime.Day);
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
                return compensatory;
            }
        }
        public void ExamineCompensatoryApply(CompensatoryApplyDto.Examine examine)
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
                compensatoryEx.AuditStatus = examine.AuditStatus;
                compensatoryEx.AuditResult = examine.AuditResult;
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
