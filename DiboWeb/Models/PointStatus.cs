using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    [Flags]
    public enum GeometryStatus
    {
        LINKED=0x0001,//点被线连接
        DELETED=0x0002, //对象被删除
        SURVEYED=0x0004,    //点被复测
        SUBMITTED=0x0008,   //点被审核提交
        SHARED = 0x0010 //点被其他组引用
    }
}
