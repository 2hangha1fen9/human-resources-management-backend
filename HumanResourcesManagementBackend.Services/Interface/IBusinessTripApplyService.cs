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
        void BusinessTripApply(BusinessTripApplyDto businessTripApply, long id);
    }
}
