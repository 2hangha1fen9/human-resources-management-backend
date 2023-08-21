using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HumanResourcesManagementBackend.Models.UserDto;

namespace HumanResourcesManagementBackend.Services
{
    public class VacationApplyService : IVacationApplyService
    {
        public void VacationApply(VacationApplyDto.VacationApply vacationApply, long id)
        {
            using (var db = new HRM())
            {
                if (vacationApply.Reason == "" || vacationApply.Reason.Length < 10)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请假原因不能为空且字数要在10字以上",
                        Status = ResponseStatus.ParameterError
                    };
                }
                if(vacationApply.BeginDate==null||vacationApply.EndDate==null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "请选择具体的请假时间",
                        Status = ResponseStatus.ParameterError
                    };
                }
                var vacationapplyR = vacationApply.MapTo<R_VacationApply>();
                vacationapplyR.EmployeeId = id;
                db.VacationApplies.Add(vacationapplyR);
                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "休假申请失败，正在维护",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }

        public List<VacationApplyDto.VacationApply> QueryMyVacationListByPage(VacationApplyDto.VacationApplySearch search)
        {
            using(var db = new HRM())
            {
                var query = from vacationapply in db.VacationApplies
                            where vacationapply.Status != DataStatus.Deleted
                            select vacationapply;

                query = query.Where(u => u.EmployeeId==search.EmployeeId);
                //分页并将数据库实体映射为dto对象(OrderBy必须调用)
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<VacationApplyDto.VacationApply>();
                //状态处理
                list.ForEach(u =>
                {
                    u.StatusStr = u.Status.Description();
                });
                return list;
            }
        }
    }
}
