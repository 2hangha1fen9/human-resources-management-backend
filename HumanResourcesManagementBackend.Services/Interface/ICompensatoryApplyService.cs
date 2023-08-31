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
        List<CompensatoryApplyDto.CompensatoryApply> QueryCompensatoryListByPage(CompensatoryApplyDto.Search search, UserDto.User currentuser);
        /// <summary>
        /// 获取调休申请记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<CompensatoryApplyDto.CompensatoryApply> QueryMyCompensatoryListByPage(CompensatoryApplyDto.Search search);
        /// <summary>
        /// 查询调休申请记录详情
        /// </summary>
        /// <param name="GetBusinessTripById"></param>
        /// <returns></returns>
        CompensatoryApplyDto.CompensatoryApply GetCompensatoryById(long id);
        /// <summary>
        /// 审核员工的调休申请记录
        /// </summary>
        /// <param name="ExamineCompensatoryApply"></param>
        /// <returns></returns>
        void ExamineCompensatoryApply(CompensatoryApplyDto.Examine examine, UserDto.User currentusere);
    }
}
