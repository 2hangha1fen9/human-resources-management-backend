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
    public class AbsenceApplyService:IAbsenceApplyService
    {
        public void AbsenceApply(AbsenceApplyDto absenceApply, long id)
        {
            using (var db = new HRM())
            {
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
    }
}
