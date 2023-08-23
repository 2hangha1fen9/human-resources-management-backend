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
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                });
                return list;
            }
        }
        public void ExamineFieldWorkApply(FieldWorkApplyDto.Examine examine)
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
                fieldworkEx.AuditStatus = examine.AuditStatus;
                fieldworkEx.AuditResult = examine.AuditResult;
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
