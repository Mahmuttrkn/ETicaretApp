﻿using EticaretApp.Domain.Entities.Common;
using EticaretApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Domain.Entities
{
    public class Endpoint: BaseEntity
    {
        public Endpoint()
        {
            Roles = new HashSet<AppRole>();
        }
        public string  ActionType { get; set; }
        public string  HttpType { get; set; }
        public string  Definition { get; set; }
        public string  Code { get; set; }
        public ICollection<AppRole> Roles { get; set; }
        public Menu Menu { get; set; }
    }
}
