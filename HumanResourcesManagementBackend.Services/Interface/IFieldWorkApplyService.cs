using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IFieldWorkApplyService
    {
        /// <summary>
        /// 外勤申请
        /// </summary>
        /// <param name="FieldWorkApply"></param>
        /// <returns></returns>
        void FieldWorkApply(FieldWorkApplyDto.FieldWorkApply fieldWorkApply);

        /// <summary>
        /// 查询当前员工的外勤申请记录
        /// </summary>
        /// <param name="SeleFieldWorkApply"></param>
        /// <returns></returns>
        List<FieldWorkApplyDto.FieldWorkApply> QueryMyFieldWorkListByPage(FieldWorkApplyDto.Search search);
        /// <summary>
        /// 审核员工的外勤申请记录
        /// </summary>
        /// <param name="ExamineFieldWorkApply"></param>
        /// <returns></returns>
        void ExamineFieldWorkApply(FieldWorkApplyDto.Examine examine);
    }
}
