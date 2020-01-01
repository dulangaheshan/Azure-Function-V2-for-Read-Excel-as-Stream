using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace email_to_db
{
    class EmailReader
    {
        public dynamic Read_email(dynamic file)
        {
            dynamic byte_obj = Convert_to_byte(file);
            return byte_array_to_datatable(byte_obj);
            
        }

        public dynamic byte_array_to_datatable(dynamic byte_obj)
        {

            dynamic result = null;
            List<string> row_list = new List<string>();
            List<List<object>> columns = new List<List<object>>();
            System.Text.Encoding.RegisterProvider(provider: System.Text.CodePagesEncodingProvider.Instance);
            try
            {
                using (var stream = new MemoryStream(byte_obj))
                {

                    //using (var reader = ExcelReaderFactory.CreateReader(stream))
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {

                        DataSet rows = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        AddToDatabase addToDatabase = new AddToDatabase();
                        foreach (DataTable table in rows.Tables)
                        {



                            List<dynamic> test = new List<dynamic>();
                            var text = JsonConvert.SerializeObject(table);
                            dynamic values = JsonConvert.DeserializeAnonymousType<dynamic>(text, null);

                            foreach (var val in values)
                            {
                                test.Add(val);
                            }

                            result = addToDatabase.AddRowsToDb(test);
                        }

                        return result;

                    }
                }
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            

        }


        dynamic Convert_to_byte(dynamic file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return fileBytes;

            }
        }



    }
}
