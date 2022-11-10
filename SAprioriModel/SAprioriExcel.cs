using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Linq;
using System.Drawing;

namespace SAprioriModel
{
    public class SAprioriExcel
    {
        public string FontName = "Times New Roman";
        public int FontSizeItem = 11;
        public int FontSizeHeader = 11;
        public int RowHeight = 20;
        /// <summary>
        /// Export strong rules to excel
        /// </summary>
        /// <param name="aprioriEngine">SAprioriEngine</param>
        /// <param name="locationExport">location to export, it is a path of excel file.
        /// <para/>You have to use SaveFile Dialog to get the location file to save
        /// </param>
        public bool ExportStrongRulesToExcel(SAprioriEngine aprioriEngine, string locationExport)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                xlApp.Visible = false;

                //Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                object misValue = System.Reflection.Missing.Value;

                Workbook wb = xlApp.Workbooks.Add(misValue);
                if (xlApp.ActiveWorkbook.Sheets.Count > 0)
                {
                    ((Excel.Worksheet)xlApp.ActiveWorkbook.Sheets[1]).Name = "<_>";

                }
                Object[] args = new Object[1];
                args[0] = 6;
                int step = 1;
                int row = 3;
                Worksheet ws = createWorkSheet(wb, "RULES");

                foreach (SAprioriRule result in aprioriEngine.AprioriResult.StrongRules.OrderByDescending(x => x.Confidence))
                {
                    string cf = String.Format("{0:0.00}", (result.Confidence * 100)) + "%";
                    Range aRange = ws.get_Range("A" + row, "E" + row);
                    aRange.RowHeight = RowHeight;
                    args = new Object[1];
                    args[0] = 18;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    object[] arrDatas = { step + "", result.X_Results_Description, "'=>", result.Y_Results_Description, cf };
                    //object[] arrDatas = { step + "", result.X, "=>", result.Y, cf };
                    aRange.Value2 = arrDatas;
                    aRange.Font.Name = FontName;
                    aRange.Font.Size = FontSizeItem;

                    step++;
                    row++;
                }
                BorderAround(ws.get_Range("A1", "E" + (row - 1)));
                wb.SaveAs(locationExport);
                wb.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(wb);
                releaseObject(xlApp);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }               
        }
        private Worksheet createWorkSheet(Workbook wb, string name)
        {
            string[] arrHeaders = { "#-RULES", "X Description", "=>", "Y Description", "Confident" };

            Worksheet ws = (Worksheet)wb.Worksheets.Add();
            ws.Name = name;
            if (ws == null)
            {
                Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
            }
            Range rowTitle = ws.get_Range("A1", "E1");
            rowTitle.Merge();
            rowTitle.Font.Name = FontName;
            rowTitle.Value2 = "List Details of RULES";
            rowTitle.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            rowTitle.Font.Color = ColorTranslator.ToOle(Color.Blue);
            rowTitle.Font.Size = FontSizeHeader;
            rowTitle.Font.Bold = true;

            // lấy dòng đầu tiên để làm tiêu đề
            Range aRange = ws.get_Range("A2", "E2");
            aRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            aRange.Interior.Color = ColorTranslator.ToOle(Color.DarkBlue);
            aRange.Font.Size = FontSizeItem;
            aRange.Font.Bold = true;
            aRange.Font.Color = ColorTranslator.ToOle(Color.White);
            aRange.Font.Name = FontName;

            if (aRange == null)
            {
                Console.WriteLine("Could not get a range. Check to be sure you have the correct versions of the office DLLs.");
            }

            // Fill the cells in the A1 to R1 range of the worksheet with the number 6.
            Object[] args = new Object[1];
            args[0] = 6;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            // Change the cells in the C1 to C7 range of the worksheet to the number 8.
            aRange.Value2 = arrHeaders;


            ws.get_Range("A1", "A1").ColumnWidth = 10;
            ws.get_Range("B1", "B1").ColumnWidth = 40;
            ws.get_Range("C1", "C1").ColumnWidth = 5;
            ws.get_Range("D1", "D1").ColumnWidth = 40;
            ws.get_Range("E1", "E1").ColumnWidth = 10;

            return ws;
        }
        private void BorderAround(Excel.Range range)
        {
            Excel.Borders borders = range.Borders;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Color = System.Drawing.Color.Black;
            borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                obj = null;
            }
            finally
            { GC.Collect(); }
        }
    }
}
