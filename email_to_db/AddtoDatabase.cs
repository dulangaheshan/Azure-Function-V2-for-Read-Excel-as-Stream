using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace email_to_db
{
    class AddToDatabase
    {
        private readonly String connection_string;
        public AddToDatabase()
            {
                connection_string = "server =.; database = dxLabs_test; Integrated Security = true; ";
            }



        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connection_string);
            }
        }

        public dynamic AddRowsToDb(DataSet rows)
        {
            List<dynamic> test = new List<dynamic>();
            Dictionary<dynamic, List<dynamic>> dict = new Dictionary<dynamic, List<dynamic>>();
            try
            {



                foreach (DataTable table in rows.Tables)
                {


                    
                    //int rowcount = table.Rows.Count;
                    //int columnCount = table.Columns.Count;

                    //for (int c = 0; c <= columnCount; c++)
                    //{
                    //    string columnName = table.Columns[c].ColumnName;
                    //    List<dynamic> tempList = new List<dynamic>();

                    //    //for (int r = 0; r <= rowcount; r++)
                    //    //{
                    //    //    var row = table.Rows[r];
                    //    //    if (row[c] != DBNull.Value)
                    //    //        tempList.Add((dynamic)row[c]);
                    //    //    else
                    //    //        tempList.Add(null);
                    //    //}
                    //    test.Add(columnName);
                    //}

                    ////    test.Add(table);
                    //// foreach (DataRow row in table.Rows)
                    ////{


                    ////        //test.Add(row);
                    ////        //foreach (object item in row.ItemArray)
                    ////        //{
                    ////        //    // read item
                    ////        //    test.Add(item);
                    ////        //}
                    ////        //foreach (KeyValuePair<dynamic, dynamic> kvp in row.ItemArray)
                    ////        //{
                    ////        //    test.Add(kvp.Key);
                    ////        //    test.Add(kvp.Value);
                    ////        //}
                    ////        //test.Add(row);

                    ////}

                   
                }
                return test;



            }
            catch(Exception e){
                return e.ToString();
            }
           
        }

    }

}
