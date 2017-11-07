using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace QuestionNaireMVC4C
{
    public class ExcelHelper
    {
        public static string SplitChar = ",";
        public static string LineBreakStr = "\r\n";
        public static string AreaStr = "\"";
        public static string PrefixStr = "\t";

        public static DataTable GetExcelForm(string filename)
        {
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                string ext = Path.GetExtension(filename);
                IWorkbook workbook = null;
                if(ext==".xlsx")
                    workbook = new XSSFWorkbook(fs);
                else if (ext==".xls")
                    workbook = new HSSFWorkbook(fs);

                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    //if (isFirstRowColumn)
                    //{
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    //}
                    //else
                    //{
                    //    startRow = sheet.FirstRowNum;
                    //}

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

    }
}