using System;

using DapperExtensions.Mapper;

namespace Unicellular.Web.Entity
{
    public class DemoEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }

    [Serializable]
    public class DomoEntityORMMapper : ClassMapper<DemoEntity>
    {
        public DomoEntityORMMapper()
        {
            base.Table( "Demo" );
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)           
            AutoMap();
        }
    }
}
