using EticaretApp.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.DTO_s.Configuration
{
    public class Action
    {
        public ActionType ActionTypes { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
    }
}
