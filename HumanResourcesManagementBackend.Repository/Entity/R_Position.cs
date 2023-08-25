using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository
{
    public class R_Position:BaseEntity
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }
    }
}
