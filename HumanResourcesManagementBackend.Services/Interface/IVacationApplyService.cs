using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IVacationApplyService
    {
        /// <summary>
        /// 休假申请
        /// </summary>
        /// <param name="vacationapply"></param>
        void VacationApply(VacationApplyDto.VacationApply vacationApply);

        /// <summary>
        /// 获取休假申请
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<VacationApplyDto.VacationApply> GetVacationApplyList(VacationApplyDto.Search search);
        /// <summary>
        /// 审核员工的休假申请记录
        /// </summary>
        /// <param name="ExamineVacationApply"></param>
        /// <returns></returns>
        void ExamineVacationApply(VacationApplyDto.Examine examine);
    }
}
