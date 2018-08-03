using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKSH.Models
{
    /// <summary>
    /// 表示jwt的payload
    /// </summary>
    public class AuthInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色列表，可以用于记录该用户的角色,相当于claims的概念(如不清楚什么事claim，请google一下"基于声明的权限控制")
        /// </summary>
        public List<string> Roles { get; set; }
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime timeout { get; set; }
        
    }
}
