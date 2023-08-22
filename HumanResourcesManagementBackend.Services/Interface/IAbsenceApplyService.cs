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
        void AbsenceApply(AbsenceApplyDto.AbsenceApply absenceApply);

        /// <summary>
        /// 查询当前员工的缺勤申请记录
        /// </summary>
        /// <param name="SeleAbsenceApply"></param>
        /// <returns></returns>
        List<AbsenceApplyDto.AbsenceApply> GetAbsenceApplyList(AbsenceApplyDto.Search search);
    }
}
