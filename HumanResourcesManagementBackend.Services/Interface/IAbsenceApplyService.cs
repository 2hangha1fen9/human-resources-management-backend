using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IAbsenceApplyService
    {
        /// <summary>
        /// 缺勤申请
        /// </summary>
        /// <param name="vacationapply"></param>
        void AbsenceApply(AbsenceApplyDto absenceApply, long id);
    }
}
