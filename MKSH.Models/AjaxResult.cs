using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKSH.Models
{
    public class AjaxResult
    { 
        public string State { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public Object resultData { get; set; }

    }
}
