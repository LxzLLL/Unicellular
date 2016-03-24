using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicellular.DataAccess;
using Unicellular.Web.Entity;
using XLToolLibrary.Utilities;
using DapperExtensions;

namespace Unicellular.Test
{
    public class Program
    {
        public static IDbConnection dbconnection = SqlConnectionFactory.CreateSqlConnection(DatabaseType.Sqlite,"SqliteConnection");
        public static void Main( string[ ] args )
        {

            //Book book = dbconnection.Get<Book>(1,null,null,DatabaseType.Sqlite);
            //Book b = new Book {Name="Java" };
            //int id = dbconnection.Insert( b, databaseType:DatabaseType.Sqlite );
            //Book b = new Book {Name="Java" };
            //Book b = new Book {Name="Java" };

            var predicate = Predicates.Field<Book>(f=>f.Id,Operator.Eq,1);
            IEnumerable<Book> list = dbconnection.GetList<Book>(predicate,databaseType:DatabaseType.Sqlite);
            dbconnection.GetList<Book>( databaseType: DatabaseType.Sqlite );
            dbconnection.Close();
            foreach(Book b in list )
            {
                Console.WriteLine( b.ToString() );
            }
            //Console.WriteLine( id );
            Console.ReadKey();
        }
    }
}
