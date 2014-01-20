using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SonnetTESTLib
{
    //File Name: CsvReader.cs
    ///Purpose  : To read test data
    ///Author   : Tejaswini K, Testing Competency
    ///---------------------------------------------------------------------------------------------------------
    public class CsvReader
    {
        /// <summary>
        /// Class to store one CSV row
        /// </summary>
        public class CsvRow : List<string>
        {
            public string LineText { get; set; }
        }

        /// <summary>
        /// Class to read data from a CSV file
        /// </summary>
        public class CsvFileReader : StreamReader
        {
            private DataTable _data, _tempDataTable = new DataTable();
            private int _rowNumber;

            public CsvFileReader(Stream stream)
                : base(stream)
            {

            }

            public CsvFileReader(string filename)
                : base(filename)
            {

            }

            /// <summary>
            /// Reads a row of data from a CSV file
            /// </summary>
            /// <param name="row"></param>
            /// <returns></returns>
            private bool ReadRow(CsvRow row)
            {
                row.LineText = ReadLine();
                if (String.IsNullOrEmpty(row.LineText))
                    return false;

                int pos = 0;
                int rows = 0;

                while (pos < row.LineText.Length)
                {
                    string value;

                    // Special handling for quoted field
                    if (row.LineText[pos] == '"')
                    {
                        // Skip initial quote
                        pos++;

                        // Parse quoted value
                        int start = pos;
                        while (pos < row.LineText.Length)
                        {
                            // Test for quote character
                            if (row.LineText[pos] == '"')
                            {
                                // Found one
                                pos++;

                                // If two quotes together, keep one
                                // Otherwise, indicates end of value
                                if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                                {
                                    pos--;
                                    break;
                                }
                            }
                            pos++;
                        }
                        value = row.LineText.Substring(start, pos - start);
                        value = value.Replace("\"\"", "\"");
                    }
                    else
                    {
                        // Parse unquoted value
                        int start = pos;
                        while (pos < row.LineText.Length && row.LineText[pos] != ',')
                            pos++;
                        value = row.LineText.Substring(start, pos - start);
                    }

                    // Add field to list
                    if (rows < row.Count)
                        row[rows] = value;
                    else
                        row.Add(value);
                    rows++;

                    // Eat up to and including next comma
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    if (pos < row.LineText.Length)
                        pos++;
                }

                // Delete any unused items
                while (row.Count > rows)
                    row.RemoveAt(rows);

                // Return true if any columns read
                return (row.Count > 0);
            }

            /// <summary>
            /// Reads data of csv into a DataTable
            /// </summary>
            /// <returns>Datatable with csv data</returns>
            public DataTable ReadCsvAsDataTable()
            {
                DataTable datatable = new DataTable();
                int count = 0;
                CsvRow row = new CsvRow();

                //while there are rows in csv
                while (this.ReadRow(row))
                {
                    //the first row is added as columns
                    if (count == 0)
                    {
                        foreach (string s in row)
                        {
                            datatable.Columns.Add(s);
                            count++;
                        }
                    }

                    //second row onwards are read as rows
                    else
                    {
                        String[] rowValues = null;
                        rowValues = row.ToArray();
                        datatable.Rows.Add(rowValues);
                    }
                }

                _tempDataTable = _data = datatable;
                
                //return the datarow
                return datatable;
            }

            /// <summary>
            /// set specific row in Datatable
            /// </summary>
            /// <param name="rowNumber"></param>
            public void SetDataRow(int rowNumber)
            {
                _rowNumber = rowNumber;
            }

            public string GetValue(string keyName)
            {
                string value = null;

                try
                {
                    value = _data.Rows[_rowNumber][keyName].ToString();
                }
                catch (Exception exception)
                {                   
               
                    throw new Exception(exception.Message);
                }
                return value;
            }

            /// <summary>
            /// filter the Datatable content by filterCriteria
            /// </summary>
            /// <param name="filterCriteria"></param>
            /// <returns></returns>
            public Boolean SetFilterTo(String filterCriteria)
            {
                _data = _tempDataTable;
                _rowNumber = 0;
                Boolean retValue = false;

                try
                {
                    string filtercondition = string.Format("Criteria='" + filterCriteria + "'");
                    DataRow[] row = _data.Select(filtercondition);
                    _data = row.CopyToDataTable();

                    if (_data.Rows.Count > 0)
                        retValue = true;
                }
                catch (Exception exception)
                {                   
                    throw new Exception(exception.Message);
                }

                return retValue;
            }

            public DataTable GetData()
            {
                    return _data;
            }


            public int Count()
            {
                return _data.Rows.Count;
            }

            /// <summary>
            /// get an integer value from the Datarow
            /// </summary>
            /// <param name="keyName"></param>
            /// <returns></returns>
            public double GetIntValue(string keyName)
            {
                return Convert.ToDouble(_data.Rows[_rowNumber][keyName]);
            }

            /// <summary>
            /// Get boolean value from the datarow
            /// </summary>
            /// <param name="keyName"></param>
            /// <returns></returns>
            public bool GetBooleanValue(string keyName)
            {
                return Convert.ToBoolean(_data.Rows[_rowNumber][keyName]);
            }
        }
    }
}
