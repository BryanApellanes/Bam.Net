/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Naizari.Extensions
{
    public static class SqlParser
    {
        static ISQLParser sqlParser = null;

        [ComImport, Guid("8F6C7662-E8A1-11D0-B9B3-2A92D0000000")]
        internal class COMSQLParser
        {
        }

        [Guid("8F6C7661-E8A1-11D0-B9B3-2A92D0000000"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        internal interface ISQLParser
        {
            [DispId(1)]
            string ParseSQLSyntax(string sql, SqlSyntaxConstants syntax);
        }

        //public SqlParser()
        //{
        //    sqlParser = (ISQLParser)new COMSQLParser();
        //}

        public static string Parse(string sql, SqlSyntaxConstants syntax)
        {
            if (sqlParser == null)
                sqlParser = (ISQLParser)new COMSQLParser();

            return sqlParser.ParseSQLSyntax(sql, syntax);
        }

        public static SqlParseResult TryGetRtf(string sql)
        {
            return TryGetRtf(sql, new SqlSyntaxHighlightMap());
        }

        public static SqlParseResult TryGetRtf(string sql, SqlSyntaxHighlightMap map)
        {
            try
            {
                SqlParseResult result = new SqlParseResult();
                result.Text = sql;
                result.RTF = GetRtf(sql, map);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                SqlParseResult result = new SqlParseResult();
                result.Exception = ex;
                result.Text = sql;
                result.Success = false;
                return result;
            }
        }

        public static string GetRtf(string sql)
        {
            return GetRtf(sql, new SqlSyntaxHighlightMap());
        }

        public static string GetRtf(string sql, SqlSyntaxHighlightMap map)
        {
            if (map == null)
                map = new SqlSyntaxHighlightMap();

            string rtf = Parse(sql, SqlSyntaxConstants.SqlServer);

            // 1: Keyword (blue)
            // 3: System Function (green)
            // 5: operator (75,75,75)
            // 6: Text (red)
            // 7: Number (cyan)
            // 8: Comment (magenta)
            // 10: Standard Text (black)

            string colorTable = @"{\colortbl;" +
                ColorToRtf(map.Keyword) +
                ColorToRtf(System.Drawing.Color.Black) +
                ColorToRtf(map.SystemFunction) +
                ColorToRtf(System.Drawing.Color.Black) +
                ColorToRtf(map.Operator) +
                ColorToRtf(map.Text) +
                ColorToRtf(map.Number) +
                ColorToRtf(map.Comment) +
                ColorToRtf(System.Drawing.Color.Black) +
                ColorToRtf(map.StandardText) +
                @"}";

            return @"{\rtf1" + colorTable + @rtf + @"}";	
        }

        private static string ColorToRtf(System.Drawing.Color color)
        {
            return @"\red" + color.R + @"\green" + color.G + @"\blue" + color.B + ";";
        }
    }

    [Guid("969F3D60-F816-11D0-B9C3-0020AFC2CD36")]
    public enum SqlSyntaxConstants
    {
        SqlServer = 1,
        Oracle = 4
    }

    public class SqlParseResult
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string Text { get; set; }
        public string RTF { get; set; }
    }
}
