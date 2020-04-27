using MyProject.DTOs;
using MyProject.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class SqlServerDbService : IDBService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18410;Integrated Security=True";

        //Powinniśmy najpiew sciagnac rekordy z bazy Prescription sciagnac wszystkich pacjentow, nastepnie usunac wszystkie rekordy z Prescription_Medicament
        //Nastepnie usunac rekordy z Prescription i a potem z Patient, wszystko w jednej transakcji
        //public bool DeletePatient(int id)
        //{
        //    using (var con = new SqlConnection(ConString))
        //    using (var com = new SqlCommand())
        //    {
        //        com.Connection = con;
        //        con.Open();
        //        var transaction = con.BeginTransaction();
        //        com.Transaction = transaction;
        //        SqlDataReader dr;

        //        try
        //        {
                    
        //        }
        //        catch (SqlException exc)
        //        {
        //            transaction.Rollback();
        //            throw new SqlServerException(exc.Message);
        //        }
        //    }
        //}

        public GetMedicationResponse GetMedicament(int id)
        {
            GetMedicationResponse result;
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                SqlDataReader dr;

                com.CommandText = "SELECT * FROM Medicament WHERE IdMedicament = @id";
                com.Parameters.AddWithValue("id", id);
                dr = com.ExecuteReader();
                if (!dr.HasRows)
                {
                    throw new MedicamentNotInDatabaseExecption("Lek o podanym ID nie istnieje w bazie danych!");
                }
                else
                {
                    dr.Read();
                    result = new GetMedicationResponse();
                    result.IdMedicament = Int32.Parse(dr["IdMedicament"].ToString());
                    result.Name = dr["Name"].ToString();
                    result.Description = dr["Description"].ToString();
                    result.Type = dr["Type"].ToString();
                    dr.Close();
                }

                List<int> prescriptionIndexes = new List<int>();
                com.CommandText = "SELECT IdPrescription FROM Prescription_Medicament WHERE IdMedicament = @id";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    prescriptionIndexes.Add(Int32.Parse(dr["IdPrescription"].ToString()));
                }
                dr.Close();

                if (prescriptionIndexes.Count > 0)
                {
                    // Nie wiem dlaczego się nie dodaje do listy to pewnie jakaś mała rzecz ale nie mogę jej znaleźć
                    result.prescriptions = new List<GetMedicationPrescription>();
                    foreach (int index in prescriptionIndexes)
                    {
                        com.CommandText = "SELECT IdPrescription, Date, DueDate FROM Prescription WHERE IdPrescription = @prescriptionId";
                        if (!com.Parameters.Contains("prescriptionId"))
                            com.Parameters.AddWithValue("prescriptionId", index);
                        else
                            com.Parameters["prescriptionId"].Value = index;
                        dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            var prescription = new GetMedicationPrescription();
                            prescription.IdPerscription = Int32.Parse(dr["IdPrescription"].ToString());
                            prescription.Date = DateTime.Parse(dr["Date"].ToString());
                            prescription.DueDate = DateTime.Parse(dr["DueDate"].ToString());
                            result.prescriptions.Add(prescription);
                        }
                        dr.Close();
                    }

                }
            }
            return result;
        }
    }
}
