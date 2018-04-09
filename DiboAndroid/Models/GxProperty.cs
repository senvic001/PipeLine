using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class GxProperty :BaseEntity<int>
    {
        public override int Id { get; set; }

        //数据库中的字段名
        public string Name { get; set; }
        //用户界面中的名字
        public string Label { get; set; }
        //可选参数
        private List<string> _alternatevalues;
        public List<string> AlternativeValues { get
            {
                if (_alternatevalues != null) return this._alternatevalues;
                _alternatevalues = new List<string>();
                if(AlternativeString != null && AlternativeString.Length>0)
                {
                    var items = AlternativeString
                        .TrimStart(new char[] { ' ', '(' })
                        .TrimEnd(new char[] { ' ', ')' })
                        .Split(new char[] { ' ', ',', '\t' });
                    _alternatevalues.AddRange(items);
                }
                return _alternatevalues;
            } }
        public string AlternativeString { get; set; }
        //几何类型
        public GeometryTypeEnum GeometryType { get; set; }
        //数据类型
        public DataTypeEnum DataType { get; set; }

        //适用管线类型
        public MainType MainType { get; set; }

        //四位数编码
        public int Code { get; set; }
        //序号(权重)
        public int Sequence { get; set; }
        //默认值
        public string DefaultValue { get; set; }
        //是否必填
        public bool Required { get; set; }

        //public Project Project { get; set; }
        //public int ProjectId { get; set; }
        public List<Template_Property> Template_Properties { get; set; }

        public GxProperty()
        {
           // AlternativeValues = new List<string>();
        }

    }

    //几何类型
    public enum GeometryTypeEnum
    {
        None,
        Point,
        Line,
        PolyLine,
        Region,
        All
    }
    public enum DataTypeEnum
    {
        None,
        Int,
        Float,
        String
    }

}
