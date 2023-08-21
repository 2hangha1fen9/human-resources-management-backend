using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services
{
    public class BusinessTripApplyService:IBusinessTripApplyService
    {
        public void BusinessTripApply(BusinessTripApplyDto businessTripApply)
        {
            using (var db = new HRM())
            {
                if (businessTripApply.EmployeeId == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "当前无法申请",
                        Status = ResponseStatus.NoPermission
                    };
                }

                if (businessTripApply.Address == "" || businessTripApply.Reason == "" || businessTripApply.Result == "" ||
                    businessTripApply.BeginDate == null || businessTripApply.EndDate == null || businessTripApply.Support == "")
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请填入具体的参数",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var businesstripapplyR = businessTripApply.MapTo<R_BusinessTripApply>();
                db.BusinessTripApplies.Add(businesstripapplyR);
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "出差申请失败，正在维护",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }
    }
}
