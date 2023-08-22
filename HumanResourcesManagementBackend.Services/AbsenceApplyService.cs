﻿using HumanResourcesManagementBackend.Common;
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
                    u.StatusStr = u.Status.Description();
                    u.AuditStatusStr = u.AuditStatus.Description();
                    u.AuditTypeStr = u.AuditType.Description();
                    u.CheckInTypeStr = u.CheckInType.Description();
                });
                return list;
            }
        }
    }
}
