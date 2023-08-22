using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface ICompensatoryApplyService
    {
        /// <summary>
        /// 调休申请
        /// </summary>
        /// <param name="CompensatoryApply"></param>
        /// <returns></returns>
        void CompensatoryApply(CompensatoryApplyDto.CompensatoryApply compensatoryApply);
        /// <summary>
        /// 获取调休申请记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<CompensatoryApplyDto.CompensatoryApply> QueryMyCompensatoryListByPage(CompensatoryApplyDto.Search search);
    }
}
