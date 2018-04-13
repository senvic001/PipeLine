using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class Avatar
    {
        public int Id { get; set; }
        public byte[] data { get; set; }
        public AvatarType Type { get; set; }
    }

    public enum AvatarType
    {
        USER,   //用户头像
        PROJECT,    //项目头像
        SYSTEM,     //系统头像
        USERDEFINE, //用户自定义头像
        PROJECTDEFINE,  //项目自定义头像
        SUBSID  //附属物图标
    }
}
