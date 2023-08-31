using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IBusinessTripApplyService
    {
        /// <summary>
        /// 出差申请
        /// </summary>
        /// <param name="BusinessTripApply"></param>
        /// <returns></returns>
        void BusinessTripApply(BusinessTripApplyDto.BusinessTripApply businessTripApply);
        /// <summary>
        /// 查询出差申请记录
        /// </summary>
        /// <param name="SeleBusinessTripApply"></param>
        /// <returns></returns>
        List<BusinessTripApplyDto.BusinessTripApply> QueryBusinessTripListByPage(BusinessTripApplyDto.Search search, UserDto.User currentuser);
        /// <summary>
        /// 查询出差申请记录
        /// </summary>
        /// <param name="SeleBusinessTripApply"></param>
        /// <returns></returns>
        List<BusinessTripApplyDto.BusinessTripApply> QueryMyBusinessTripListByPage(BusinessTripApplyDto.Search search);
        /// <summary>
        /// 查询出差申请记录详情
        /// </summary>
        /// <param name="GetBusinessTripById"></param>
        /// <returns></returns>
        BusinessTripApplyDto.BusinessTripApply GetBusinessTripById(long id);
        /// <summary>
        /// 审核员工的出差申请记录
        /// </summary>
        /// <param name="ExamineBusinessTripApply"></param>
        /// <returns></returns>
        void ExamineBusinessTripApply(BusinessTripApplyDto.Examine examine, UserDto.User currentusere);
    }
}
