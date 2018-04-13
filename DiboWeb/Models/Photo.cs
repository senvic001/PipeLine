using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        //图片只属于某个管线点
        public long GxPointId { get; set; }
        public GxPoint GxPoint { get; set; }

        public DateTime CreateTime { get; set; }

        public int UserId{get;set;}
    }
}
