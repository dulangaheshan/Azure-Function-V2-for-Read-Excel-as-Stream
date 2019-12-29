using Dapper;
using Newtonsoft.Json;
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



        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(connection_string);
            }
        }

        public dynamic AddRowsToDb(List<dynamic> rows)
        {

            try
            {
                int id = -1;

                foreach (var data in rows)
                {
                    id = id + 1;
                    string Track = data["Track"];
                    double RMS_Energy = data["RMS Energy"];
                    double Entropy = data["Entropy"];
                    double Spectral_RollOff = data["Entropy"];
                    double Tempo = data["Entropy"];
                    double Spectral_Centroid = data["Entropy"];
                    double Spectral_Flux = data["Entropy"];
                    double Z_Crossing_Rate = data["Entropy"];

                    using (SqlConnection dbConnection = Connection)
                    {
                        string sQuery = "INSERT INTO test(id,track,[rms Energy],entropy,[spectral RollOff],tempo,[spectral Centroid],[spectral Flux],[z_Crossing Rate])" +
                                        "VALUES(@id,@Track,@RMS_Energy,@Entropy,@Spectral_RollOff,@Tempo,@Spectral_Centroid,@Spectral_Flux,@Z_Crossing_Rate)";

                        dbConnection.Open();
                        dbConnection.Execute(sQuery, new { id=id, Track = Track,RMS_Energy=RMS_Energy,Entropy=Entropy,
                            Spectral_RollOff =Spectral_RollOff,Tempo=Tempo,Spectral_Centroid=Spectral_Centroid,Spectral_Flux=Spectral_Flux,Z_Crossing_Rate=Z_Crossing_Rate });
                        return true;




                    }







                    //return null;



                }

                return true;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
           
        }

    }

}
