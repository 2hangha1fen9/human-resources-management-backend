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
    public class ApplyService : IApplyService
    {
        public void VacationApply(VacationApplyDto vacationApply, long id)
        {
            using (var db = new HRM())
            {
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
                vacationapplyR.EmployeeId = id;
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
        public void AbsenceApply(AbsenceApplyDto absenceApply, long id)
        {
            using (var db = new HRM())
            {
                if(absenceApply.AbsenceDateTime==null||absenceApply.CheckInType==0||
                    absenceApply.Reason==""||absenceApply.Prover=="")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填入具体的参数",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var absenceapplyR = absenceApply.MapTo<R_AbsenceApply>();
                absenceapplyR.EmployeeId = id;
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

       public void CompensatoryApply(CompensatoryApplyDto compensatoryApply, long id)
       {
            using(var db = new HRM())
            {
                if(compensatoryApply.WorkDate==null||compensatoryApply.RestDate==null||compensatoryApply.WorkPlan=="")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填入具体的参数",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var compensatoryapplyR = compensatoryApply.MapTo<R_CompensatoryApply>();
                compensatoryapplyR.EmployeeId = id;
                db.CompensatoryApplies.Add(compensatoryapplyR);
                if(db.SaveChanges()==0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "调休申请失败，正在维护",
                        Status = ResponseStatus.AddError
                    };
                }
            }
       }
    }
}
