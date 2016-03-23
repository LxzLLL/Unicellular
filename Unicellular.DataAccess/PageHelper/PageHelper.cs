using System;
using System.Text;

namespace Unicellular.DataAccess
{
    /// <summary>
    /// 功能描述：分页帮助类
    /// 创 建 人：Arvin
    /// 创建日期：2015-04-07
    /// </summary>
    public class PageHelper
    {
        #region SqlServer分页

        /// <summary>
        /// 用于 sqlserver
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selectSql"></param>
        /// <param name="SqlCount"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static string GetPagingSql(int pageIndex, int pageSize, string selectSql, string SqlCount, string orderBy)
        {
            if (pageIndex == 0)
                pageIndex = 1;
            if (pageSize == 0)
                pageSize = int.MaxValue;
            StringBuilder sbSql = new StringBuilder("DECLARE @pageIndex int,@pageSize int\n");
            sbSql.AppendFormat("SET @pageIndex = {0}\n", pageIndex);
            sbSql.AppendFormat("SET @pageSize = {0}\n", pageSize);
            sbSql.AppendFormat("SELECT * FROM (SELECT *, ROW_NUMBER() OVER({0}) AS RankNumber from (\n", orderBy);
            sbSql.AppendFormat("{0}\n", selectSql);
            sbSql.Append(") as topT) AS subT\n");
            sbSql.Append(" WHERE rankNumber BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize\n");

            sbSql.AppendFormat("{0}\n", SqlCount);
            return sbSql.ToString();

        }
        #endregion

        #region Oracle分页
        /// <summary>
        /// 用于Oracle
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selectSql"></param>
        /// <param name="SqlCount"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static string GetOraclePagingSql(int pageIndex, int pageSize, string selectSql, string SqlCount, string orderBy) {
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            var toSkip = (pageIndex - 1) * pageSize;
            var topLimit = toSkip + pageSize;
            var sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM (");
            sb.AppendLine("SELECT _ss_data_1_.*, ROWNUM RNUM FROM (");
            sb.Append(selectSql.Trim().TrimEnd(';'));
            sb.AppendFormat( " {0}", orderBy );
            sb.AppendLine(") _ss_data_1_ )");
            sb.AppendFormat( "WHERE ROWNUM > {0} AND ROWNUM <= {1} ) ", toSkip,topLimit );
            return sb.ToString();
        }
        #endregion

        #region Mysql分页
        /// <summary>
        /// MySql
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selectSql"></param>
        /// <param name="SqlCount"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static string GetMySqlPagingSql(int pageIndex, int pageSize, string selectSql, string SqlCount, string orderBy)
        {

            return "";
        }
        #endregion
    }
}
