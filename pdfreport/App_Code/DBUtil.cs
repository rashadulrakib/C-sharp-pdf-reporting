using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OracleClient;


/// <summary>
/// This class handle the database connection & 
/// returns datatable for sql
/// </summary>
public class DBUtil
{
    #region Member variables

    private OracleConnection _Connection = null;

    #endregion
	
    #region Public Properties

    public static string CONN_STRING = string.Empty;

    #endregion

    #region Constructor

    /// <summary>
    /// Constructs DBUtuil object
    /// </summary>
    public DBUtil()
   {
		//
		// TODO: Add constructor logic here
		//
    }

    #endregion

    #region private methods

    /// <summary>
    /// creates an orcale connection & open the connection & returns it.
    /// </summary>
    /// <returns>OracleConnection</returns>
    private OracleConnection GetDBConnection()
    {
        try
        {
            if (_Connection == null)
            {
                _Connection = new OracleConnection(DBUtil.CONN_STRING);
                _Connection.Open();
            }
        }
        catch (OracleException oraExe)
        {
            //_Connection.Open();
            throw oraExe;
        }
        catch (Exception exp) 
        {
            throw exp;
        }
        
        return _Connection;
    }

    #endregion

    #region Public methods

    /// <summary>
    /// executes a sql & retuns datatable
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>DataTable</returns>
    public DataTable GetDataTable(string sql)
    {
        DataTable oDataTable = new DataTable();

        try
        {
            if (!string.IsNullOrEmpty(sql))
            {
                using (OracleCommand command = new OracleCommand(sql, this.GetDBConnection())) 
                {
                    OracleDataReader reader = command.ExecuteReader();
                    oDataTable.Load(reader);
                }               
            }
        }
        catch (Exception oEx)
        {
            throw oEx;
        }

        return oDataTable;
    }

    #endregion
}
