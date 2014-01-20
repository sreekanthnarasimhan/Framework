using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace SonnetTESTLib
{
    /// <summary>
    /// Read data maintained in XML
    /// </summary>
    public class readXML
    {
        private XmlDocument xmlData = new XmlDocument();
        private String elementXPath;
        private XmlNodeList nodeList;
        private XmlNode xmlNode;

        /// <summary>
        /// Open a xml file to extract data
        /// </summary>
        /// <param name="fileName"></param>
        public void OpenXMLFile(String fileName)
        {
            string xmlFile = "..\\..\\..\\CodedUI\\" + fileName;
            this.xmlData.Load(@xmlFile);
        }

        /// <summary>
        /// Find an element in XML using xpath
        /// </summary>
        /// <param name="stateXPath">specify XPath to seach an element</param>
        /// <returns></returns>
        /// <remarks>Identify an element(s) matching XPath</remarks>
        public readXML FindElement(String stateXPath)
        {
            if (this.xmlData == null)
                throw new Exception("ResourceXML - FindElement(): XML data is NOT retrieved...");

            this.nodeList = this.xmlData.SelectNodes(stateXPath);
            this.elementXPath = stateXPath;

            if (nodeList.Count == 0)
                throw new Exception("ResourceXML - FindElement():  No data found for the given XPath...");

            this.xmlNode = nodeList.Item(0);

            return this;
        }

        /// <summary>
        /// Select an element within the collection 
        /// </summary>
        /// <param name="elementIndex">Index / Position of an element within the collection</param>
        /// <remarks>In case <c>FindElement()</c> method returns mutliple elements as collection, it needs to be filtered using index</remarks>
        public readXML FilterByIndex(int elementIndex)
        {
            if (this.nodeList.Count > 0)
                this.xmlNode = this.nodeList.Item(elementIndex);

            return this;
        }

        /// <summary>
        /// Select an element within the collection 
        /// </summary>
        /// <param name="attName">Attribute name</param>
        /// <param name="attValue">Attribute value used to search</param>
        /// <remarks>In case <c>FindElement()</c> method returns mutliple elements as collection, it needs to be filtered using index</remarks>
        public readXML FilterByAttribute(String attName, String attValue)
        {
            if (this.xmlData == null)
                throw new Exception("ResourceXML - FilterByAttribute(): XML data is NOT retrieved...");

            this.nodeList = this.xmlData.SelectNodes(this.elementXPath + "[@" + attName + "='" + attValue + "']");
            if (nodeList.Count == 0)
                throw new Exception("ResourceXML - FilterByAttribute():  No data found for the given attribute name and value...");

            // [@name='usp_GET_HOME_PAGE_DATA']"
            this.xmlNode = nodeList.Item(0);

            return this;
        }

        /// <summary>
        /// Get a value in the selected element
        /// </summary>
        /// <returns>String - Node Value</returns>
        public String GetValue()
        {
            String retValue = "";

            if (this.xmlNode != null)
                retValue = this.xmlNode.FirstChild.Value.Trim();

            return retValue;
        }

        /// <summary>
        /// Get a value in the selected element
        /// </summary>
        /// <returns>Number - Node Value</returns>
        public int GetNumericValue()
        {
            int retValue = 0;

            if (this.xmlNode != null)
                retValue = Convert.ToInt16(this.xmlNode.FirstChild.Value);

            return retValue;
        }

        /// <summary>
        /// Get a value in the selected element
        /// </summary>
        /// <returns>Boolean - Node Value</returns>
        public Boolean GetBooleanValue()
        {
            Boolean retValue = false;

            if (this.xmlNode != null)
                retValue = Convert.ToBoolean(this.xmlNode.FirstChild.Value);

            return retValue;
        }
    }

    /// <summary>
    /// class to write .xml output file
    /// </summary>
    public class WriteXml
    {
        /// <summary>
        /// xml writer object
        /// </summary>
        private XmlTextWriter writer;
        /// <summary>
        /// string for storing values
        /// </summary>
        private String url = "", browser = "", buildnumber = "", executedAt = "";
        /// <summary>
        /// scenario and testcase names to read from excel file
        /// </summary>
        private Hashtable testCase = new Hashtable();
        /// <summary>
        /// counters for total pass and fail
        /// </summary>
        private int totalPass = 0, totalFail = 0, total = 0;
        /// <summary>
        /// to read the node number
        /// </summary>
        private String node_num = "";

        /// <summary>
        /// Reads the file name for xml file.
        /// </summary>
        /// <param name="filename">filename of output</param>
        public void createXML(String filename)
        {
            writer = new XmlTextWriter(filename, null);
            writer.WriteStartDocument();
            writer.WriteStartElement("testresult");
            writer.WriteStartElement("detail");
        }

        /// <summary>
        /// sets the build number
        /// </summary>
        /// <param name="build">build number</param>
        public void SetBuildNumber(String build)
        {
            this.buildnumber = build;
        }

        /// <summary>
        /// Adds the test information
        /// </summary>
        /// <param name="url">url info</param>
        /// <param name="browser">browser info</param>
        public void AddHeader(String url, String browser)
        {
            if (browser.Equals("ie"))
                this.browser = "Internet Explorer";
            else if (browser.Equals("ff"))
                this.browser = "Firefox";
            else if (browser.Equals("chrome"))
                this.browser = "Google Chrome";

            this.url = url;
            DateTime dateValue = new DateTime(2010, 1, 18);
            this.executedAt = dateValue.ToString();
        }

        /// <summary>
        /// sets the hashtable with testcase, scenario from excel file
        /// </summary>
        /// <param name="table">hash table with info from excel file</param>
        public void SetTestCase(Hashtable table)
        {
            this.testCase = table;
        }

        /// <summary>
        /// appends the teststep to the report file
        /// </summary>
        /// <param name="description">decription of test</param>
        /// <param name="errorFileName">error filename</param>
        /// <param name="comment">comment for the teststep</param>
        /// <param name="execution_status">true if pass, otherwise false</param>
        public void append(String description, String errorFileName,
            String comment, Boolean execution_status)
        {
            this.writer.WriteStartElement("teststep");
            this.writer.WriteAttributeString("value", "1");

            if (this.testCase.ContainsKey("sid"))
            {
                this.writer.WriteElementString("sid", (string)this.testCase["sid"]);
            }

            if (this.testCase.ContainsKey("sname"))
            {
                this.writer.WriteElementString("sname", (string)this.testCase["sname"]);
            }

            if (this.testCase.ContainsKey("tid"))
            {
                this.writer.WriteElementString("tid", (string)this.testCase["tid"]);
            }

            if (this.testCase.ContainsKey("tname"))
            {
                this.writer.WriteElementString("tname", (string)this.testCase["tname"]);
            }

            this.writer.WriteElementString("description", description);

            this.writer.WriteElementString("error_filename", errorFileName);


            this.total++;
            String exe_status = "Fail";
            if (execution_status == true)
            {
                this.totalPass++;
                exe_status = "Pass";
            }
            else
                this.totalFail++;

            this.writer.WriteElementString("status", exe_status);


            comment = this.node_num + comment;
            this.writer.WriteElementString("comment", comment);
            this.writer.WriteEndElement();
        }

        /// <summary>
        /// sets the build number
        /// </summary>
        /// <param name="node">build number</param>
        public void setNodeNumber(String node)
        {
            this.node_num = "Node number=" + node + " ";
        }

        /// <summary>
        /// generates the xml file... must be called
        /// </summary>
        public void updateSummary()
        {
            this.writer.WriteEndElement();
            this.writer.WriteStartElement("summary");
            this.writer.WriteElementString("url", this.url);
            this.writer.WriteElementString("browser", this.browser);
            //this.writer.WriteElementString("executed on", this.executedAt);
            this.writer.WriteElementString("pass", this.totalPass.ToString());
            this.writer.WriteElementString("fail", this.totalFail.ToString());
            this.writer.WriteElementString("total", this.total.ToString());
            this.writer.WriteElementString("build", this.buildnumber);
            this.writer.WriteEndElement();
            this.writer.WriteEndElement();
            this.writer.WriteEndDocument();
            this.writer.Close();
        }
    }
}
