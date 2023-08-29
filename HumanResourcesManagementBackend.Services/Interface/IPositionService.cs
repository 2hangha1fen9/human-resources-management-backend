using HumanResourcesManagementBackend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IPositionService
    {
        /// <summary>
        /// 获取岗位列表
        /// </summary>
        /// <returns></returns>
        List<PositionDto.Position> GetPositions(PositionDto.Search search);

        /// <summary>
        /// 根据ID获取岗位名称
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        PositionDto.Position GetPositionById(long roleId);

        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <param name="role"></param>
        void AddPosition(PositionDto.Save role);

        /// <summary>
        /// 编辑岗位
        /// </summary>
        /// <param name="role"></param>
        void EditPosition(PositionDto.Save role);

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="roleId"></param>
        void DeletePosition(long roleId);
    }
}
