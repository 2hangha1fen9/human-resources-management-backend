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
        void FieldWorkApply(FieldWorkApplyDto fieldWorkApply);
    }
}
