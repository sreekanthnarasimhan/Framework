using System;
using System.Data;

namespace SonnetTESTLib
{
    public class DataSource
    {
        private DataTable _testDataSource, _testData;
        private int _rowNumber = 0;
        private bool _enableTestData = false;

        public void CSVFile(string fileName)
        {
            CsvReader.CsvFileReader reader = new CsvReader.CsvFileReader(fileName);
            _testData = _testDataSource = reader.ReadCsvAsDataTable();
            EnableTestData();
        }

        public void EnableTestData()
        {
            _enableTestData = true;
        }

        public void DisableTestData()
        {
            _enableTestData = false;
        }

        public bool isTestDataEnabled
        {
            get
            {
                return _enableTestData;
            }
        }

        public void ExcelFile(string fileName)
        {

        }

        public bool SetFilterTo(string filterCriteria)
        {
            _testData = _testDataSource;
            _rowNumber = 0;
            Boolean retValue = false;

            try
            {
                string filtercondition = string.Format("Criteria='" + filterCriteria + "'");
                DataRow[] row = _testData.Select(filtercondition);
                _testData = row.CopyToDataTable();

                if (_testData.Rows.Count > 0)
                    retValue = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return retValue;
        }

        public void SetDataRow(int rowNumber)
        {
            _rowNumber = rowNumber;
        }

        public string GetValue(string keyName)
        {
            string reqValue = null;

            try
            {
                reqValue = _testData.Rows[_rowNumber][keyName].ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return reqValue;
        }

        public double GetIntValue(string keyName)
        {
            return Convert.ToDouble(_testData.Rows[_rowNumber][keyName]);
        }

        public bool GetBooleanValue(string keyName)
        {
            return Convert.ToBoolean(_testData.Rows[_rowNumber][keyName]);
        }
    }
}
