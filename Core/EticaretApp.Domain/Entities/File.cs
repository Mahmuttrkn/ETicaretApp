using EticaretApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Domain.Entities
{
    public class File: BaseEntity
    {
        public String FileName { get; set; }
        public String Path { get; set; }
        public String Storage { get; set; }
        [NotMapped]
        public override DateTime UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
