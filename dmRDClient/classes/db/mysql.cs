using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace dm
{


    public static class mysqldb
    {

        public static MySqlConnection  conn;
        public static string connString = "";


        public static string columnEnclosingLeft="`";
        public static string columnEnclosingRight="`";
        
        public static bool nonQuery(string query){
            try{
                if (conn == null || conn.State == System.Data.ConnectionState.Closed) mysqldb.connect();
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
            }catch (Exception e){
              //  log.error("db.query", e.Message);
            }
            return false;
        }

        public static void disposeReader(MySqlDataReader reader){
            if(null!=reader) reader.Dispose();
        }

        public static void closeReader(MySqlDataReader reader){
            if(null!=reader) reader.Close();
        }


        public static void connect(){
            try{
                conn = new MySqlConnection(connString);
                conn.Open();
            }catch (Exception e){
             //   log.error("DB::connect",e.ToString());
            }

        }

        
        public static void connect(string ip,string user,string password){
            connString="Server="+ip+";User ID="+user+";Password="+password;
            connect();
        }

        public static void close(){
            if(conn!=null) {
                conn.Close();
            }
        }

        public static bool isConnected(){
            if(conn==null) return false;
            
            switch(conn.State){
                case System.Data.ConnectionState.Broken: return false;
                case System.Data.ConnectionState.Closed: return false;
                case System.Data.ConnectionState.Connecting: return true;
                case System.Data.ConnectionState.Executing: return true;
                case System.Data.ConnectionState.Open: return true;
                case System.Data.ConnectionState.Fetching: return true;
            }
            return false;
        }

        public static string escapeString(string data){
            return MySql.Data.MySqlClient.MySqlHelper.EscapeString(data);//.Replace("_", "\\_")
            /*return data;
            StringBuilder s2=new StringBuilder();
            foreach(char c in data){
                    switch (c){
                        case '\\': s2.Append(@"\\"); break;
                        case '\'': s2.Append(@"\'"); break;
                        case '%' : s2.Append(@"\%"); break;
                        default  : s2.Append(c); break;
                    }
            }
            return s2.ToString();
             */
            //return data.Replace(@"\", @"\\").Replace("'", @"\'").Replace("%", "\\%");
            
        }

        public static void selectDB(string db){
            if(isConnected())
            conn.ChangeDatabase(db);
        }
          
        public static MySqlDataReader query(string query) {
            MySqlCommand command;
            MySqlDataReader reader;    
            try{
                if (conn == null || conn.State == System.Data.ConnectionState.Closed) mysqldb.connect();
                
                command = new MySqlCommand(query,conn);
                reader = command.ExecuteReader();
                reader.Close();
                
            }catch (Exception e){
             //   log.error("DB::query",e.ToString());
            }
            return null;
        }

        public static Hashtable fetchSingle(MySqlDataReader reader){
            if(null==reader || reader.IsClosed) return null;
            Hashtable results = new Hashtable();
                if(!reader.Read()) {
                    reader.Close();
                    return null;
                }
                if (reader.HasRows){
                for (int i = 0; i < reader.FieldCount; i++){
                    string data="";
                    if(null!=reader[i]) data=reader[i].ToString().Trim();
                    string column=reader.GetName(i);
                    results[column] =data;
                }
            } else {
                reader.Close();
                return null;
            }
            return results;
        }


        public static Hashtable fetch(string query){
            Hashtable results = new Hashtable();
            MySqlDataReader reader = null;
            MySqlCommand command = null;

            try{
                query = query.Replace("[", "`");
                query = query.Replace("]", "`");
                if (conn == null || conn.State == System.Data.ConnectionState.Closed) mysqldb.connect();
                command = new MySqlCommand(query, conn);
                reader = command.ExecuteReader();
                if(!reader.Read()) {
                    reader.Close();
                    return null;
                }
                if (reader.HasRows){
                    for (int i = 0; i < reader.FieldCount; i++){
                        results[reader.GetName(i)] = reader[i].ToString().Trim();
                    }
                }
            }
            catch (Exception e){
              //  log.error("DB::query", e.ToString());
            }
            try{
                reader.Close();
                command.Dispose();
            }catch (Exception e){
                MessageBox.Show(query + " " + e.Message);
            }
            return results;
        }

        public static List<Hashtable> fetchAll(string query){
            List<Hashtable> results = new List<Hashtable>();

            MySqlDataReader reader = null;
            MySqlCommand command = new MySqlCommand(query, conn);

            try{
                query=query.Replace("[", "`");
                query = query.Replace("]", "`");
                if (conn == null || conn.State == System.Data.ConnectionState.Closed) mysqldb.connect();
                command = new MySqlCommand(query, conn);
                command.CommandTimeout = 75;
                reader = command.ExecuteReader();
               if (reader.HasRows) {
                    while (reader.Read()) {
                        Hashtable result = new Hashtable();
                        for (int i = 0; i < reader.FieldCount; i++) {
                            string data="";
                            if(null!=reader[i]) data=reader[i].ToString().Trim();
                            string column=reader.GetName(i);
                            result[column] =data;
                            
                        }
                        results.Add(result);
                    }
                }
            }catch (Exception e){
                
                //log.error("DB::query", e.ToString());
            }
            try{
                reader.Close();
                command.Dispose();
            }catch (Exception e){
                MessageBox.Show(query+" "+e.Message);
            }
            return results;
        }//end function
        //
        public static string columnConvert(string type,bool provideDefault){
            switch (type.ToLower()){
                case "undefined"          : break;          //Undefined
                case "tinyint"            : return type; 	//1-byte signed integer
                case "tinyunsigned" 	  : return type;    //1-byte unsigned integer
                case "smallint" 	      : return type;    //2-byte signed integer
                case "smallunsigned" 	  : return type;    //2-byte unsigned integer
                case "mediumint" 	      : return type;    //3-byte signed integer
                case "mediumunsigned" 	  : return type;    //3-byte unsigned integer
                case "int" 	              : return type;    //4-byte signed integer
                case "unsigned" 	      : return type;    //4-byte unsigned integer
                case "bigint" 	          : return type;    //8-byte signed integer
                case "bigunsigned" 	      : return type;    //8-byte signed integer
                case "float" 	          : return type;    //4-byte float
                case "double" 	          : return type;    //8-byte float
                case "olddecimal" 	      : return type;    //Signed decimal as used prior to MySQL 5.0
                case "olddecimalunsigned" : return type; 	//Unsigned decimal as used prior to MySQL 5.0
                case "decimal" 	          : return type;    //Signed decimal as used by MySQL 5.0 and later
                case "decimalunsigned" 	  : return type;    //Unsigned decimal as used by MySQL 5.0 and later
                case "char" 	          : return type;    //A fixed-length array of 1-byte characters; maximum length is 255 characters
                case "varchar" 	          : return type;    //A variable-length array of 1-byte characters; maximum length is 255 characters
                case "binary" 	          : return type;    //A fixed-length array of 1-byte binary characters; maximum length is 255 characters
                case "varbinary" 	      : return type;    //A variable-length array of 1-byte binary characters; maximum length is 255 characters
                case "datetime" 	      : return type;    //A 8-byte date and time value, with a precision of 1 second
                case "date" 	          : return type;    //A 4-byte date value, with a precision of 1 day
                case "blob" 	          : return type;    //A binary large object; see Section 2.3.16, “The NdbBlob Class”
                case "text" 	          : return type;    //A text blob
                case "bit" 	              : return type;    //A bit value; the length specifies the number of bits
                case "longvarchar" 	      : return type;    //A 2-byte Varchar
                case "longvarbinary"      : return type; 	//A 2-byte Varbinary
                case "time"               : return type; 	//Time without date
                case "year"               : return type; 	//1-byte year value in the range 1901-2155 (same as MySQL)
                case "timestamp"          : return type;    //Unix time
                //default : return ""; 
            }
            if(provideDefault) return "text";
            return "";
       }

       public static List<Hashtable> getTables(string database){
            List<Hashtable> results=new List<Hashtable>();
            List<Hashtable> tableData=mysqldb.fetchAll("SELECT `TABLES`.TABLE_NAME as name,`TABLES`.TABLE_ROWS as rows, `TABLES`.`ENGINE` as engine FROM `TABLES` where `TABLES`.TABLE_SCHEMA='"+database+"' order by TABLE_NAME ASC");
            
            foreach(Hashtable tableRow in tableData){
                Hashtable table=new Hashtable();
                table["name"]=tableRow["name"].ToString();
                results.Add(table);
            }
            return results;
        }


        public static List<Hashtable> getColumns(string tableName,string database){
            string query=   "SELECT column_name as `column`,data_type as `type`, "+
                            "CASE is_nullable WHEN 'YES' THEN 'null' ELSE 'not null' END AS `nullable`, "+
                            "CASE !isnull(character_maximum_length) WHEN '' then '' ELSE CAST(character_maximum_length as CHAR) END AS `length` "+
                            "FROM information_schema.columns "+
                            "WHERE TABLE_NAME='"+tableName+"' AND TABLE_SCHEMA='"+database+"' ORDER BY ordinal_position";
            mysqldb.nonQuery("USE "+database);
            List<Hashtable> tables=mysqldb.fetchAll(query);
            return tables;
        }

        public static string createInsert(string database,string table,Hashtable data){
            StringBuilder values =new StringBuilder();
            StringBuilder columns=new StringBuilder();
            
            bool first=true;
            foreach (String key in data.Keys) {
                switch(first) {
                    case true:  columns .Append(columnEnclosingLeft + mysqldb.escapeString(key) + columnEnclosingRight);
                                values.Append("\""+escapeString(data[key].ToString())+"\"");
                                first = false;
                                break;
                    
                    case false: columns .Append(","+columnEnclosingLeft + mysqldb.escapeString(key) + columnEnclosingRight);
                                values.Append(",\""+escapeString(data[key].ToString())+"\"");
                                break;
                    }
            }

            return "INSERT INTO "+columnEnclosingLeft+table+columnEnclosingRight+"("+columns+") VALUES ("+values+");"; //+columnEnclosingLeft+database+columnEnclosingRight+"."+
        }//end function

    }//end mysqldb class
}//end dm namespace
