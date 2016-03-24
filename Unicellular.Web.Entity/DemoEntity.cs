using System;
using System.Text;
using System.Collections.Generic;

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

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public virtual List<BookReview> Reviews { get; set; }
        public Book()
        {
            //Reviews = new List<BookReview>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat( "[{0}]------《{1}》", Id, Name ).Append(Environment.NewLine);
            //foreach(BookReview br in Reviews )
            //{
            //    sb.Append( "content:"+br.ToString() ).Append(Environment.NewLine);
            //}
            
            return sb.ToString();
        }
    }

    public class BookReview
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual string Content { get; set; }
        public virtual Book AssoicationWithBook { get; set; }
        public override string ToString()
        {
            return string.Format( "{0})--[{1}]\t\"{2}\"", Id, BookId, Content );
        }
    }

}
