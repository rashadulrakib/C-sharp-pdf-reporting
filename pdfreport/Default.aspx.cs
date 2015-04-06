using System;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.OracleClient;
using System.Collections.Generic;
using PdfSharp.Drawing;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
            PrintReport();
    }

    /// <summary>
    /// 
    /// </summary>
    private void PrintReport()
    {
        //if space is required, then it can be passed by - char 
        //the - will remove by space

        string username = "null";
        string titel = "null";
        string showfilter = "null";
        string curdate = "null";
        string paperformat = string.Empty;
        string papersize = string.Empty;
        string table = string.Empty;
        string fields = string.Empty;
        string whereclause = string.Empty;
        string groupby = "NONE";
        string orderby = string.Empty;
        string reporttype = string.Empty;
        string reportid = string.Empty;

        string selectSql = string.Empty;
        string countSql = string.Empty;

        EPageSize EpageSize;
        EPaperFormat EpaperFormat;

        try
        {
            string QueryStringUrl = Server.UrlDecode(Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1));
            string[] KeyValues = QueryStringUrl.Split('&');

            for (int i = 0; i < KeyValues.Length; i++)
            {
                string[] KeyValuePair = KeyValues[i].Split('=');
                string key = KeyValuePair.Length > 0 ? KeyValuePair[0].Trim() : string.Empty;
                string value = KeyValues[i].Substring(KeyValues[i].IndexOf('=') + 1).Trim();

                switch (key)
                {
                    case "username":
                        username = value;
                        break;
                    case "titel":
                        titel = value;
                        break;
                    case "showfilter":
                        showfilter = value;
                        break;
                    case "curdate":
                        curdate = value;
                        break;
                    case "paperformat":
                        paperformat = value;
                        break;
                    case "papersize":
                        papersize = value;
                        break;
                    case "reporttype":
                        reporttype = value;
                        break;
                    case "reportid":
                        reportid = value;
                        break;
                    case "table":
                        table = value;
                        break;
                    case "fields":
                        fields = value;
                        break;
                    case "whereclause":
                        whereclause = value;
                        break;
                    case "groupby":
                        groupby = value;
                        break;
                    case "orderby":
                        orderby = value;
                        break;
		            case "connstring":
                        DBUtil.CONN_STRING = value;
                        break;
                }
            }

            //temporary asign user name;
            username = "test user name";
            titel = "test title";
            paperformat = "portrait";
            papersize = "a4";
            reporttype = "Regular";
            reportid = "Markeringen";
            //


            if (!string.IsNullOrEmpty(fields) && !string.IsNullOrEmpty(table))
            {
                selectSql = " select " + fields + " from " + table;

                if (!string.IsNullOrEmpty(whereclause))
                {
                    selectSql += " where " + whereclause + " ";
                }

                if (!groupby.Equals("NONE") && !string.IsNullOrEmpty(groupby))
                {
                    countSql = " select count(*)," + groupby + " from " + table;

                    if (!string.IsNullOrEmpty(whereclause))
                    {
                        countSql += " where " + whereclause + " ";
                    }

                    countSql += " group by " + groupby;

                    selectSql += " order by " + groupby;
                }

                if (!string.IsNullOrEmpty(orderby))
                {
                    selectSql += selectSql.Contains("order by") == true ? " , " + orderby : " order by " + orderby;
                }
            }

            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Add("username", username);
            dicParams.Add("titel", titel);

            showfilter = showfilter != "null" ? whereclause == string.Empty ? "De data wordt niet gefiltered"
                : whereclause
                : showfilter;
            dicParams.Add("showfilter", showfilter);
            dicParams.Add("curdate", curdate.Equals("null") == true ? "null" : DateTime.Now.ToString("dd/MM/yyyy"));

            switch (papersize)
            {
                case "a3":
                    EpageSize = EPageSize.A3;
                    break;
                case "a4":
                    EpageSize = EPageSize.A4;
                    break;
                default:
                    EpageSize = EPageSize.A4;
                    break;
            }

            switch (paperformat)
            {
                case "landscape":
                    EpaperFormat = EPaperFormat.LandScape;
                    break;
                case "portrait":
                    EpaperFormat = EPaperFormat.Portrait;
                    break;
                default:
                    EpaperFormat = EPaperFormat.Portrait;
                    break;
            }

            ReportUtil oReportUtil = new ReportUtil();

            ReportProperties data = oReportUtil.GetReportProperties(selectSql, dicParams, EpageSize, EpaperFormat, Server.MapPath(FileNameManager.PdfFileName));

            //Tempory populate paramers in the report 

           
            
            //End popualate parameters

            //DataTable recordCountTableForEachGroup = oReportUtil.GetRecordCountForEachGroup(countSql);t
            DataTable recordCountTableForEachGroup = new DataTable();




            switch (reporttype)
            {
                case "Regular":
                    oReportUtil.GenerateRegularReport(data, recordCountTableForEachGroup, Response);
                    break;
                case "Piechart":
                case "Histogram":
                    Dictionary<string, string> groupColors =
                        new ColorUtil().GetGroupColors(recordCountTableForEachGroup,table, groupby, reportid);
                    List<Group> oListGroup = new List<Group>();
                    for (int i = 0; i < recordCountTableForEachGroup.Rows.Count; i++)
                    {
                        Group oGroup = new Group();
                        oGroup.GroupValue = double.Parse(recordCountTableForEachGroup.Rows[i][0].ToString());
                        oGroup.GroupName = recordCountTableForEachGroup.Rows[i][groupby].ToString();
                        oGroup.GroupColour = XColor.FromArgb(System.Drawing.ColorTranslator.FromHtml("#" + groupColors[oGroup.GroupName]));
                        oListGroup.Add(oGroup);
                    }
                    
                    if (reporttype == "Piechart")
                    {
                        oReportUtil.GeneratePieChart(data, Response, oListGroup);
                    }
                    else if (reporttype == "Histogram")
                    {
                        oReportUtil.GenerateColumnChart(data, Response, oListGroup, groupby);
                    }
                    break;
                default:
                    oReportUtil.GenerateRegularReport(data, recordCountTableForEachGroup, Response);
                    break;
            }
        }
        catch (Exception oEx)
        {
            Response.Write("Following server error occurs...<br/>");
            Response.Write("Select sql:" + selectSql + "<br/>");
            Response.Write("Count sql:" + countSql + "<br/>");
            Response.Write(oEx.Message + "<br/>");
            Response.Write(oEx.StackTrace);
        }
    }
}
