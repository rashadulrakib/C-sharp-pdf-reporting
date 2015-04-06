//**********************

//*********************

function getConnectionString(connstring)
{
	var connKeys = connstring.split(";");
		
	connstring = "";
	
	for(i=0; i<connKeys.length; i++ )
	{
		var keyvalue = connKeys[i].split("=");
				
		if(keyvalue[0]=="User ID")
		{
			connstring=connstring+connKeys[i]+";";
		}
		else if(keyvalue[0]=="Data Source")
		{
			connstring=connstring+connKeys[i]+";";
		}
		else if(keyvalue[0]=="Password")
		{
			connstring=connstring+connKeys[i]+";";
		}
	}
	
	return connstring;
}