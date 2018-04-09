using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class BaseEntity<U>
    {
        public virtual U Id { get; set; }
    }
}
