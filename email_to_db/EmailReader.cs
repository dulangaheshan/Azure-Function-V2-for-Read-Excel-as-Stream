﻿using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace email_to_db
{
    class EmailReader
    {
        public dynamic Read_email(dynamic file)
        {
            dynamic byte_obj = Convert_to_byte(file);

            List<string> rows = new List<string>();
            List<List<object>> columns = new List<List<object>>();
            System.Text.Encoding.RegisterProvider(provider: System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream(byte_obj))
            {

                //using (var reader = ExcelReaderFactory.CreateReader(stream))
                using (IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {

                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    return result;

                }
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
