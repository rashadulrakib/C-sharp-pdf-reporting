using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using PdfSharp.Drawing;


/// <summary>
/// Summary description for ColorUtil
/// </summary>
public class ColorUtil
{
    string[] colors =
        new string[] { 
            "E00001",
            "C0C0C0",
            "808080",
            "000000",
            "FF0000",
            "FFFF00",
            "00FF00",
            "00FFFF",
            "0000FF",
            "FF00FF",
            "800000",
            "808000",
            "008000",
            "008080",
            "000080",
            "800080", //15
            "500000",
            "700000",
            "900000",
            "C00000",
            "E00000",
            "FF0000",
            "FF2020",
            "FF4040",
            "FF5050",
            "FF6060",
            "FF8080",
            "FF9090",
            "FFA0A0",
            "FFB0B0",
            "FFD0D0",
 "501400",
"701C00",
 "902400",
 "A02800",
"C03000",
"E03800",
 "FF4000",
 "FF5820",
"FF7040",
 "FF7C50",
"FF9470",
 "FFA080",
 "FFB8A0",
"FFC4B0",
 "FFDCD0",
 "502800",
 "603000",
 "804000",
"A05000",
"B05800",
 "D06800",
 "E07000",
 "FF8000",
"FF9830",
 "FFA850",
 "FFB060",
 "FFC080",
 "FFD0A0",
"FFD8B0",
"FFE8D0",
 "503C00",
 "604800",
 "806000",
"906C00",
 "A07800",
"C09000",
"D09C00",
 "F0B400",
"FFC000",
 "FFD040",
 "FFD860",
 "FFDC70",
 "FFE490",
 "FFECB0",
 "FFF4D0",
 "505000",
"606000",
"707000",
 "909000",
 "A0A000",
"B0B000",
 "C0C000",
"F4F400",
"F0F000",
 "FFFF00",
 "FFFF40",
"FFFF70",
"FFFF90",
"FFFFB0",
 "FFFFD0",
 "305000",
 "3A6000",
 "4D8000",
 "569000",
"60A000",
 "73C000",
 "7DD000",
 "90F000",
"9AFF00",
"B3FF40",
 "C0FF60",
"CDFF80",
"D3FF90",
 "E0FFB0",
 "EDFFD0",
 "005000",
 "006000",
"008000",
"00A000",
 "00B000",
 "00D000",
 "00E000",
"00FF00",
 "50FF50",
 "60FF60",
 "70FF70",
"90FF90",
"A0FFA0",
 "B0FFB0",
 "D0FFD0",
 "005028",
"006030",
 "008040",
"00A050",
"00B058",
 "00D068",
 "00F470",
 "00FF80",
 "50FFA8",
 "60FFB0",
 "70FFB8",
 "90FFC8",
 "A0FFD0",
 "B0FFD8",
"D0FFE8",
 "005050",
"006060",
"008080",
"009090",
"00A0A0",
 "00C0C0",
 "00D0D0",
 "00F0F0",
"00FFFF",
"50FFFF",
"70FFFF",
"80FFFF",
"A0FFFF",
 "B0FFFF",
 "D0FFFF",
"003550",
"004B70",
 "006090",
"006BA0",
"0080C0",
 "0095E0",
 "00ABFF",
"40C0FF",
 "50C5FF",
"60CBFF",
"80D5FF",
 "90DBFF",
"A0E0FF",
 "B0E5FF",
"D0F0FF",
 "001B50",
 "002570",
 "001C90",
 "0040C0",
 "004BE0",
 "0055FF",
 "3075FF",
 "4080FF",
 "508BFF",
 "70A0FF",
 "80ABFF",
 "90B5FF",
 "A0C0FF",
 "C0D5FF",
"D0E0FF",
 "000050",
 "000080",
 "0000A0",
 "0000D0",
 "0000FF",
 "2020FF",
 "1C1CFF",
 "5050FF",
 "6060FF",
"7070FF",
"8080FF",
"9090FF",
 "A0A0FF",
"C0C0FF",
 "D0D0FF",
 "280050",
 "380070",
 "480090",
 "6000C0",
 "7000E0",
 "8000FF",
 "9020FF",
 "A040FF",
 "A850FF",
 "B060FF",
 "C080FF",
"C890FF",
"D0A0FF",
 "D8B0FF",
 "E8D0FF",
"500050",
"700070",
 "900090",
 "A000A0",
"C000C0",
 "E000E0",
 "FF00FF",
 "FF20FF",
 "FF40FF",
"FF50FF",
 "FF70FF",
 "FF80FF",
 "FFA0FF",
 "FFB0FF",
 "FFD0FF",
 "500028",
"700060",
"900048",
 "C00060",
 "E00070",
 "FF008A",
 "FF2090",
"FF40A0",
 "FF50A8",
"FF60B0",
 "FF80C0",
 "FF90C8",
 "FFA0D0",
"FFB0D8",
"FFD0E8",
"000000",
"101010",
"202020",
 "303030",
"404040",
 "505050",
"606060",
"707070",
"909090",
 "A0A0A0",
"B0B0B0",
 "C0C0C0",
 "D0D0D0",
 "E0E0E0",
 "F0F0F0"
        };
    
    
    public ColorUtil()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Dictionary<string, string> GetGroupColors(DataTable dtGroups, string table, string groupby, string reportid)
    {
        Dictionary<string, string> dicColors = new Dictionary<string, string>();

        try
        {
            //table parameter is not used
            //string selectGroupsByASC = "select count(*), " + groupby +
            //    " from " + table + " group by " + groupby + " order by " + groupby + " asc";

            //DataTable dtNewGroups = new DBUtil().GetDataTable(selectGroupsByASC); //it is not used

            int totalGroups = dtGroups.Rows.Count;

            int[] colorIndex = null;

            //if (totalGroups > 0 && totalGroups <= 24)
            //{
            //    colorIndex = new int[] { 17, 20, 27, 54, 49, 58, 86, 79, 96, 100, 139, 144, 
            //        153, 164, 200, 205, 215, 219, 224, 245, 249, 252, 255, 24 };
            //}
            //else if(totalGroups>24)
            {
                colorIndex = new int[colors.Length];

                for (int i = 0; i < colorIndex.Length; i++)
                {
                    colorIndex[i] = i;
                }
            }

            if (!string.IsNullOrEmpty(groupby) && !groupby.Equals("NONE"))
            {
                string select = "select COLORCODE,GROUPCODE from group_color where REPORTCODE='" + reportid + "' and "
                    + " GROUPBY='" + groupby + "'";

                for (int i = 0; i < totalGroups; i++)
                {
                    string groupName = dtGroups.Rows[i][groupby].ToString();

                    if (i == 0)
                    {
                        select += " AND GROUPCODE in ('" + groupName + "'";
                    }

                    if (i > 0)
                    {
                        select += ",'" + groupName + "'";
                    }

                    if (i == totalGroups - 1)
                    {
                        select += " )";
                    }
                }

                if (totalGroups > 0)
                {
                    DataTable dtSavedColor = new DBUtil().GetDataTable(select);

                    for (int i = 0; i < dtSavedColor.Rows.Count; i++)
                    {
                        if (!dicColors.ContainsKey(dtSavedColor.Rows[i][1].ToString()))
                        {
                            dicColors.Add(dtSavedColor.Rows[i][1].ToString(), dtSavedColor.Rows[i][0].ToString());
                        }
                    }

                    for (int i = 0; i < totalGroups; i++)
                    {
                        if (!dicColors.ContainsKey(dtGroups.Rows[i][groupby].ToString()))
                        {
                            if (i < colorIndex.Length && colorIndex[i] < colors.Length)
                            {
                                dicColors.Add(dtGroups.Rows[i][groupby].ToString(),
                                    colors[colorIndex[i]]);
                            }
                            else
                            {
                                dicColors.Add(dtGroups.Rows[i][groupby].ToString(),
                                    "FFFFFF");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception oEx)
        {
            throw oEx;
        }

        return dicColors;
    }
}
