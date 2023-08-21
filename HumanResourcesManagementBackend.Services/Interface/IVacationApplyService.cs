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
        void VacationApply(VacationApplyDto.VacationApply vacationApply, long id);

        /// <summary>
        /// 查询当前员工的休假申请记录
        /// </summary>
        /// <param name="SeleVacationApply"></param>
        /// <returns></returns>
        List<VacationApplyDto.VacationApply> QueryMyVacationListByPage(VacationApplyDto.VacationApplySearch search);
    }
}
