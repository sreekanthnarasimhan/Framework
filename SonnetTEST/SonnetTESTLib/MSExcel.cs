using Microsoft.Office.Interop.Excel;
using System;
using System.Data;

namespace SonnetTESTLib
{
    #region Excel_API
    /// <summary>
    /// Used to read an excel file.  MS Office - Excel should have been installed locally to execute these methods
    /// </summary>
    public class MSExcel
    {
        #region Fields
        private System.Data.DataTable excelData = null;
        private Range usedRange = null;
        private Worksheet excelSheet = null;
        private int columnCount = 0;
        private Microsoft.Office.Interop.Excel.Application application = null;
        private Microsoft.Office.Interop.Excel.Workbook workBook = null;
        #endregion

        #region Methods
        /// <summary>
        /// Open a excel file and set focus to specified worksheet to extract data
        /// </summary>
        /// <param name="excelFileName">Full path and name of the excel file</param>
        /// <param name="sheetName">Worksheet Name</param>
        /// <returns>MSExcel</returns>
        public MSExcel OpenExcelFile(String excelFileName, String sheetName)
        {
            application = new Microsoft.Office.Interop.Excel.Application();
            workBook = application.Workbooks.Open(excelFileName);
            this.excelSheet = (Worksheet)workBook.Sheets[sheetName];
            this.usedRange = this.excelSheet.UsedRange;
            this.excelData = new System.Data.DataTable();
            AddHeader();
            return this;
        }

        /// <summary>
        /// Create columns in Datatable adding headers
        /// </summary>
        private void AddHeader()
        {
            Range headerRow = this.usedRange.Rows[1];
            for (int i = 1; i <= headerRow.Columns.Count; i++)
            {
                if (headerRow.Cells[i].Value == null)
                    break;

                DataColumn column = new DataColumn(headerRow.Cells[i].Value.ToString().ToLower().Trim());
                this.excelData.Columns.Add(column);
            }

            this.columnCount = this.excelData.Columns.Count;
        }

        /// <summary>
        /// To add row to the datatable
        /// </summary>
        /// <param name="rowData">Range of row</param>
        /// <returns>DataRow</returns>
        private System.Data.DataRow AddRowData(Range rowData)
        {
            DataRow dataRow = this.excelData.NewRow();
            Object cellVal = "";
            Range headerRow = this.usedRange.Rows[1];
            for (int iCol = 0; iCol < rowData.Columns.Count; iCol++)
            {
                try
                {
                    //Condition to terminate the loop based on first row)
                    if (headerRow.Cells[iCol + 1].Value == null)
                        break;
                    cellVal = rowData.Cells[iCol + 1].Value;

                    if (cellVal == null)
                        dataRow[iCol] = "";

                    else if (cellVal.GetType().Name.Equals("Integer"))
                        dataRow[iCol] = Convert.ToInt16(cellVal);

                    else if (cellVal.GetType().Name.Equals("Double"))
                        dataRow[iCol] = Convert.ToDouble(cellVal);

                    else if (cellVal.GetType().Name.Equals("Boolean"))
                        dataRow[iCol] = Convert.ToBoolean(cellVal);

                    else if (cellVal.GetType().Name.Equals("DateTime"))
                        dataRow[iCol] = Convert.ToDateTime(cellVal);

                    else
                        dataRow[iCol] = cellVal;
                }
                catch (Exception)
                {
                    dataRow[iCol] = "";
                }
            }
            return dataRow;
        }

        /// <summary>
        /// Filters data based on filter criteria (string format) mapped to a column
        /// </summary>
        /// <param name="columnNumber">index of column number starting with 1</param>
        /// <param name="filterCriteria">set filter criteria</param>
        /// <returns>Data is extracted to a <c>DataTable</c></returns>
        public System.Data.DataTable FilterBy(int columnNumber, Object filterCriteria)
        {
            foreach (Range dataRow in this.usedRange.Rows)
            {
                if (dataRow.Cells[1].Value == null)
                    break;

                Object cellVal = dataRow.Cells[columnNumber + 1].Value;

                if (cellVal.ToString().Trim().Equals(filterCriteria.ToString().Trim()))
                    this.excelData.Rows.Add(AddRowData(dataRow));
            }
            Close();
            return this.excelData;
        }

        /// <summary>
        /// Extract entire data from specified worksheet
        /// </summary>
        /// <returns>Data is extracted to a <c>DataTable</c></returns>
        public System.Data.DataTable GetAllRows()
        {
            foreach (Range dataRow in this.usedRange.Rows)
            {
                if (dataRow.Row == 1)
                    continue;
                if (dataRow.Cells[1].Value == null)
                    break;

                this.excelData.Rows.Add(AddRowData(dataRow));
            }

            Close();
            return this.excelData;
        }

        /// <summary>
        /// To release the com object.
        /// </summary>
        private void Close()
        {

            if (workBook != null)
                workBook.Close();

            if (application != null)
            {
                application.Workbooks.Close();
                application.Quit();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Release the objects
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(this.excelSheet);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(application);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(this.usedRange);
            this.usedRange = null;
            this.excelSheet = null;
            workBook = null;
            application = null;
        }
        #endregion
    }
    #endregion
}
