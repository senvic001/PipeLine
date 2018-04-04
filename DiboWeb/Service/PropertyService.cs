using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiboWeb.Models;

namespace DiboWeb.Service
{
    public class PropertyService
    {
        private  GxDbContext gxDb;

        public PropertyService(GxDbContext dbContext)
        {
           gxDb = dbContext;
        }
        public void  AddProperty(GxProperty gxProperty)
        {
            gxDb.GxProperties.Add(gxProperty);
            gxDb.SaveChanges();
        }

        public void AddProperties(IEnumerable<GxProperty> gxProperties)
        {
            gxDb.GxProperties.AddRange(gxProperties);
            gxDb.SaveChanges();
        }

        public void DeleteProperty(GxProperty gxProperty)
        {
            //GxProperty which is used can't be deleted.
            if (gxDb.Template_Properties.Any(u => u.GxPropertyId == gxProperty.Id)) return;
            gxDb.GxProperties.Remove(gxProperty);
            gxDb.SaveChanges();
        }
        public void DeleteProperties(IEnumerable<GxProperty> gxProperties)
        {
            foreach (var item in gxProperties)
            {
                if (! gxDb.Template_Properties.Any(u => u.GxPropertyId == item.Id))  gxDb.GxProperties.Remove(item);
            }
            gxDb.SaveChanges();
        }
       
    }
}
