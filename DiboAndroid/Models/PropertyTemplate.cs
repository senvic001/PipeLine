using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class PropertyTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Template_Property> Template_Properties { get; set; }
    }

    public class Template_Property
    {
        public int PropertyTemplateId { get; set; }
        public PropertyTemplate PropertyTemplate { get; set; }

        public int GxPropertyId { get; set; }
        public GxProperty GxProperty { get; set; }
    }
}
