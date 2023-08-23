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
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                });
                return list;
            }
        }
        public void ExamineBusinessTripApply(BusinessTripApplyDto.Examine examine)
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
                businesstripEx.AuditStatus = examine.AuditStatus;
                businesstripEx.AuditResult = examine.AuditResult;
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
