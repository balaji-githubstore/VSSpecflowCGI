using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenEmrApplication.Utilities
{
    class ExcelUtils
    {
        public static DataTable SheetToDataTable(string path,string sheetname)
        {
            XLWorkbook book = new XLWorkbook(path);
            IXLWorksheet sheet=  book.Worksheet(sheetname);
            Console.WriteLine(sheet.RangeUsed().RowCount());
            DataTable dt = new DataTable();
            for(int c=1;c<=sheet.RangeUsed().ColumnCount();c++)
            {
                dt.Columns.Add(sheet.Row(1).Cell(c).GetString());
            }
      
            for(int r=2;r<=sheet.RangeUsed().RowCount();r++)
            {
                DataRow row = dt.NewRow();
                for (int c = 1; c <= sheet.RangeUsed().ColumnCount(); c++)
                {
                    row[c-1] = sheet.Row(r).Cell(c).GetString();
                }
                dt.Rows.Add(row);
            }
            book.Dispose();
            return dt;
        }
    }
}
