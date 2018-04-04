using System;
using System.Collections.Generic;
using System.Text;
using DiboWeb.Models;
using DiboWeb.Helper;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTestDiboWeb
{
    public class PropertyTest
    {
        private ITestOutputHelper output;
        public PropertyTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void JsonTest()
        {
            var p = new GxProperty()
            {
                Name = "附属物",
                Label = "Feature",
                DataType = DataTypeEnum.String,
                DefaultValue = string.Empty,
                MainType = MainType.ALL
            };
            p.AlternativeValues.AddRange( new string[] { "检查井", "手孔" });

            string jsonstr = JsonConvert.SerializeObject(p);
            output.WriteLine(jsonstr);
            GxProperty pd = JsonConvert.DeserializeObject<GxProperty>(jsonstr);
            Assert.NotNull(pd);
            Assert.Equal("Feature", pd.Label);
        }
        [Fact]
        public void LoadTemplateTest()
        {
            var data=TemplateConfig.ReadDefaultTemplate();
            //output.WriteLine(JsonConvert.SerializeObject(data));
            Assert.True(data.Count > 0);
            
        }
    }
}
