using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiboWeb.Models;
using OfficeOpenXml;
using System.IO;

namespace DiboWeb.Helper
{
    public sealed class TemplateConfig
    {
        /// <summary>
        /// 从Excel读取模版文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns>属性列表</returns>
        public static List<GxProperty> ReadDefaultTemplate(string path = @"i:\VSProject\DiboPipeLine\DiboWeb\wwwroot\PropertyTemplate\defaultTemplate.xlsx")
        {
            //string path = @"i:\VSProject\DiboPipeLine\DiboWeb\wwwroot\defaultTemplate.xlsx";
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) throw new FileNotFoundException(path + " does not exist!");

            var gxProperties = new List<GxProperty>(64);
            using (var exfile = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = exfile.Workbook.Worksheets[0];
                int row = 2;
                while (worksheet.Cells[row, 1].GetValue<int?>() != null)
                {
                    var property = new GxProperty
                    {
                        Sequence = worksheet.Cells[row, 1].GetValue<int>(),
                        Label = worksheet.Cells[row, 3].GetValue<string>(),
                        Name = worksheet.Cells[row, 4].GetValue<string>(),
                        AlternativeString = worksheet.Cells[row, 7].GetValue<string>(),
                        MainType = GetMainType(worksheet.Cells[row, 8].GetValue<string>()),
                        GeometryType = GetGeometryType(worksheet.Cells[row, 9].GetValue<string>()),
                        Required = worksheet.Cells[row, 10].GetValue<bool?>() ?? false
                    };

                    gxProperties.Add(property);
                    row++;
                }
            }

            return gxProperties;
        }
        /// <summary>
        /// 从字符串转为枚举的几何类型
        /// </summary>
        /// <param name="typename"></param>
        /// <returns></returns>
        public static GeometryTypeEnum GetGeometryType(string typename)
        {
            if (typename == null) throw new ArgumentNullException("Geometry type can't be null.");
            GeometryTypeEnum gemtype;

            switch (typename.ToUpper())
            {
                case "P1":
                    gemtype = GeometryTypeEnum.Point;
                    break;
                case "P2":
                    gemtype = GeometryTypeEnum.Point;
                    break;
                case "LINE":
                    gemtype = GeometryTypeEnum.Line;
                    break;
                case "ALL":
                    gemtype = GeometryTypeEnum.All;
                    break;
                default:
                    gemtype = GeometryTypeEnum.None;
                    break;
            }
            return gemtype;
        }

        /// <summary>
        /// 几何类型转换
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static string GeometryTypeToString(GeometryTypeEnum geometry)
        {
            switch (geometry)
            {
                case GeometryTypeEnum.None:
                    return "NONE";
                case GeometryTypeEnum.Point:
                    return "P1";
                case GeometryTypeEnum.Line:
                    return "LINE";
                default:
                    return GeometryTypeEnum.PolyLine.ToString().ToUpper(); ;
            }
        }

         /// <summary>
        /// 属性的参数类型转换
        /// </summary>
        /// <param name="typename"></param>
        /// <returns></returns>
        public static MainType GetMainType(string typename)
        {
            if (typename == null) throw new ArgumentNullException("Maintype can't be null.");
            MainType mainType = MainType.None;
            typename = typename.ToUpper();

            if (typename == "ALL") return MainType.ALL;

            char[] types = typename.ToCharArray();
            foreach (var type in types)
            {
                switch (type)
                {
                    case 'J':
                        mainType = MainType.JS | mainType;
                        break;
                    case 'G':
                        mainType = MainType.GY | mainType;
                        break;
                    case 'R':
                        mainType = MainType.RL | mainType;
                        break;
                    case 'Q':
                        mainType = MainType.RQ | mainType;
                        break;
                    case 'P':
                        mainType = MainType.PS | mainType;
                        break;
                    case 'Y':
                        mainType = MainType.YS | mainType;
                        break;
                    case 'W':
                        mainType = MainType.WS | mainType;
                        break;
                    case 'L':
                        mainType = MainType.DL | mainType;
                        break;
                    case 'D':
                        mainType = MainType.DL | mainType;
                        break;
                    case 'X':
                        mainType = MainType.DX | mainType;
                        break;
                    case 'H':
                        mainType = MainType.DL | mainType;
                        break;
                    case 'Z':
                        mainType = MainType.ZH | mainType;
                        break;
                    case 'F':   //消防
                        mainType = MainType.JS | mainType;
                        break;
                    case 'B':
                        mainType = MainType.None | mainType;
                        break;
                    default:
                        mainType = MainType.None | mainType;
                        break;
                }
            }
            return mainType;
        }
        
        /// <summary>
        /// 管线类型转换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string MainTypeToString(MainType type)
        {
            string result = string.Empty;
            if (type.HasFlag(MainType.ALL)) return "ALL";
            if (type.HasFlag(MainType.None))   return  "B";
            if (type.HasFlag(MainType.JS)) result = "JF";

            if (type.HasFlag(MainType.PS)) result = (result.IndexOf("P")>0)? result:result+ "P";
            if (type.HasFlag(MainType.YS)) result = (result.IndexOf("Y") > 0) ? result : result + "Y";
            if (type.HasFlag(MainType.WS)) result = (result.IndexOf("W") > 0) ? result : result + "W";
            if (type.HasFlag(MainType.HS)) result = (result.IndexOf("P") > 0) ? result : result + "P";

            if (type.HasFlag(MainType.RQ)) result = (result.IndexOf("Q") > 0) ? result : result + "Q";
            if (type.HasFlag(MainType.MQ)) result = (result.IndexOf("Q") > 0) ? result : result + "Q";
            if (type.HasFlag(MainType.YH)) result = (result.IndexOf("Q") > 0) ? result : result + "Q";
            if (type.HasFlag(MainType.TR)) result = (result.IndexOf("Q") > 0) ? result : result + "Q";

            if (type.HasFlag(MainType.ZQ)) result = (result.IndexOf("R") > 0) ? result : result + "R";
            if (type.HasFlag(MainType.RS)) result = (result.IndexOf("R") > 0) ? result : result + "R";
            if (type.HasFlag(MainType.RL)) result = (result.IndexOf("R") > 0) ? result : result + "R";

            if (type.HasFlag(MainType.Q)) result = (result.IndexOf("G") > 0) ? result : result + "G";
            if (type.HasFlag(MainType.Y)) result = (result.IndexOf("G") > 0) ? result : result + "G";
            if (type.HasFlag(MainType.YQ)) result = (result.IndexOf("G") > 0) ? result : result + "G";
            if (type.HasFlag(MainType.SY)) result = (result.IndexOf("G") > 0) ? result : result + "G";
            if (type.HasFlag(MainType.GY)) result = (result.IndexOf("G") > 0) ? result : result + "G";

            if (type.HasFlag(MainType.GD)) result = (result.IndexOf("L") > 0) ? result : result + "L";
            if (type.HasFlag(MainType.LD)) result = (result.IndexOf("D") > 0) ? result : result + "D";
            if (type.HasFlag(MainType.DC)) result = (result.IndexOf("L") > 0) ? result : result + "L";
            if (type.HasFlag(MainType.XH)) result = (result.IndexOf("H") > 0) ? result : result + "H";
            if (type.HasFlag(MainType.DL)) result = (result.IndexOf("L") > 0) ? result : result + "L";

            if (type.HasFlag(MainType.DX)) result = (result.IndexOf("X") > 0) ? result : result + "X";
            if (type.HasFlag(MainType.GB)) result = (result.IndexOf("X") > 0) ? result : result + "X";
            if (type.HasFlag(MainType.DS)) result = (result.IndexOf("X") > 0) ? result : result + "X";
            if (type.HasFlag(MainType.X)) result = (result.IndexOf("X") > 0) ? result : result + "X";

            return result;
        }
    }
}
