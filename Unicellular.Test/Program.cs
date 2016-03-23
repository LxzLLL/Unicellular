using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XLToolLibrary.Utilities;

namespace Unicellular.Test
{
    public class Program
    {
        public static void Main( string[ ] args )
        {
            string sResult = RandomHelper.GenerateRandomNumber( 16 );
            Console.WriteLine( sResult );
            Console.ReadKey();
        }
    }
}
