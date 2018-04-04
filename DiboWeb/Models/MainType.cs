using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    [Flags]
    public enum MainType
    {
        None=0,   //未知类型
        JS = 0x0001, //排水
            WS = 0x0002, //污水
            YS = 0x0004, //雨水
            HS = 0x0008,//雨污合流
        PS=MainType.WS|MainType.YS|MainType.HS, //排水
            MQ = 0x0010, //燃气
            YH = 0x0020, //液化气
            TR = 0x0040, //天然气
        RQ=MainType.MQ|MainType.YH|MainType.TR, //燃气
            ZQ = 0x0080, //蒸汽
            RS = 0x0100, //热水
        RL=MainType.ZQ|MainType.RS, //热力
            Q = 0x0200,  //氢气
            Y = 0x0400,  //氧气
            YQ = 0x0800, //乙炔
            SY = 0x1000, //石油
        GY=MainType.Q|MainType.Y|MainType.YQ|MainType.SY,
            GD = 0x2000, //供电
            LD = 0x4000, //路灯
            DC = 0x8000, //电车
            XH = 0x10000, //交通信号
        DL=MainType.GD|MainType.LD|MainType.DC|MainType.XH, //电力
            DX = 0x20000, //电话
            GB = 0x40000, //广播
            DS = 0x80000, //有线电视
        X=MainType.DX|MainType.GB|MainType.DS,  //电信,规范是DX与电话代码重复
        ZH = 0x100000,  //综合管沟
        ALL = 0x1FFFFF
    }
}
