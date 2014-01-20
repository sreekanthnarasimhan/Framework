using System;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;

namespace SonnetTESTLib
{
    #region OpenSource_API
    /// <summary>
    /// Extract data from excel into an object i.e. <c>DataTable</c>.  It uses Open Source API to read data in excel
    /// </summary>
    public class ExcelOS
    {
        #region Fields
        private ISheet excelSheet = null;
        private HSSFWorkbook excelFile = null;
        private System.Data.DataTable excelData = null;
        private int columnCount = 0;
        #endregion

        #region Methods
        /// <summary>
        /// Open an excel file to extract data (as test data or object repository)
        /// </summary>
        /// <param name="excelFileName">excel file name to be opened</param>
        /// <param name="sheetName">specify excel worksheet name to extract data</param>
        /// <returns>Data is extracted to <c>DataTable</c></returns>
        /// <remarks>Data is extracted based on the filter criteria.   Data is hold internally within the object instance.</remarks>
        public ExcelOS OpenExcelFile(String excelFileName, String sheetName)
        {
            this.excelFile = new HSSFWorkbook(new FileStream(excelFileName, FileMode.Open, FileAccess.Read));
            this.excelSheet = this.excelFile.GetSheet(sheetName);
            IRow headerRow = this.excelSheet.GetRow(0);
            this.columnCount = headerRow.LastCellNum;
            this.excelData = new System.Data.DataTable();
            AddHeader();
            return this;
        }

        /// <summary>
        /// Filter data based on filter criteria (string format) mapped to first column
        /// </summary>
        /// <param name="columnNumber">index of column number</param>
        /// <param name="filterCriteria">set filter criteria</param>
        /// <returns>Data is extracted to a <c>DataTable</c></returns>
        public System.Data.DataTable FilterBy(int columnNumber, Object filterCriteria)
        {
            int rowCount = this.excelSheet.LastRowNum;

            for (int iRow = (this.excelSheet.FirstRowNum); iRow <= this.excelSheet.LastRowNum; iRow++)
            {
                IRow rowData = this.excelSheet.GetRow(iRow);
                if (rowData.GetCell(1) == null)
                    break;
                else if (rowData.GetCell(1).CellType == CellType.BLANK)
                    break;
                if (rowData.GetCell(columnNumber).StringCellValue.Trim().Equals(filterCriteria.ToString()))
                    this.excelData.Rows.Add(AddRowData(rowData));
            }

            return this.excelData;
        }

        /// <summary>
        /// Extract entire data from specified worksheet
        /// </summary>
        /// <returns>Data is extracted to a <c>DataTable</c></returns>
        public System.Data.DataTable GetAllRows()
        {
            int rowCount = this.excelSheet.LastRowNum;

            for (int iRow = (this.excelSheet.FirstRowNum + 1); iRow <= this.excelSheet.LastRowNum; iRow++)
            {
                IRow rowData = this.excelSheet.GetRow(iRow);

                if (rowData == null)
                    break;

                if (rowData.GetCell(0) == null)
                    break;

                else if (rowData.GetCell(0).CellType == CellType.BLANK)
                    break;

                this.excelData.Rows.Add(AddRowData(rowData));
            }

            return this.excelData;
        }

        /// <summary>
        /// Retrieve a row data into DataRow object
        /// </summary>
        /// <param name="rowData">IRow Object</param>
        /// <returns>DataRow</returns>
        private DataRow AddRowData(IRow rowData)
        {
            DataRow dataRow = this.excelData.NewRow();

            for (int iCol = rowData.FirstCellNum; iCol < this.columnCount; iCol++)
            {
                try
                {
                    if (this.excelSheet.GetRow(0).GetCell(iCol) == null)
                        break;
                    else if (this.excelSheet.GetRow(0).GetCell(iCol).CellType == CellType.BLANK)
                        break;

                    if (rowData.GetCell(iCol) == null)
                        dataRow[iCol] = "";

                    else if (rowData.GetCell(iCol).CellType == CellType.NUMERIC)
                        dataRow[iCol] = rowData.GetCell(iCol).NumericCellValue;

                    else if (rowData.GetCell(iCol).CellType == CellType.BOOLEAN)
                        dataRow[iCol] = rowData.GetCell(iCol).BooleanCellValue;

                    else if (rowData.GetCell(iCol).CellType == CellType.BLANK)
                        dataRow[iCol] = "";

                    else
                        dataRow[iCol] = rowData.GetCell(iCol).StringCellValue;
                }
                catch (Exception)
                {
                    dataRow[iCol] = rowData.GetCell(iCol).StringCellValue;
                }
            }
            return dataRow;
        }

        /// <summary>
        /// Defines column Headers to the datatable
        /// </summary>
        private void AddHeader()
        {
            IRow headerRow = this.excelSheet.GetRow(0);
            for (int i = headerRow.FirstCellNum; i < this.columnCount; i++)
            {
                if (headerRow.GetCell(i) == null)
                    break;
                else if (headerRow.GetCell(i).CellType == CellType.BLANK)
                    break;

                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue.ToLower().Trim());
                this.excelData.Columns.Add(column);
            }

            this.columnCount = this.excelData.Columns.Count;
        }
        #endregion
    }
    #endregion


}
