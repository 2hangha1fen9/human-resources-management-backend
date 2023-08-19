using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    public enum PositionLevel
    {
        /// <summary>
        /// 基层员工
        /// </summary>
        [Description("基层员工")]
        GrassrootsStaff,
        /// <summary>
        /// 基层职员
        /// </summary>
        [Description("基层职员")]
        JuniorStaff,
        /// <summary>
        /// 中层管理
        /// </summary>
        [Description("基层职员")]
        MiddleManager,
        /// <summary>
        /// 储备干部
        /// </summary>
        [Description("储备干部")]
        ManagementTrainee
    }
}
