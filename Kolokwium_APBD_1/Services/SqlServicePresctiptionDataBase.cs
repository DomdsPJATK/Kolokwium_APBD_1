using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Kolokwium_APBD_1.DTOs.Request;
using Kolokwium_APBD_1.Properties.Model;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_APBD_1.Services
{
    public class SqlServicePresctiptionDataBase : IServicePrescriptionDataBase
    {
        
        private readonly string databaseURL = "Data Source=db-mssql;Initial Catalog=s19036;Integrated Security=True";
        
        public IActionResult getPrescriptionById(int id)
        {
            using (var client = new SqlConnection(databaseURL))
            using (var com = new SqlCommand())
            {
                
                List<Medicament> medicaments = new List<Medicament>();
                com.Connection = client;
                client.Open();
                com.Transaction = client.BeginTransaction();
                SqlDataReader db;
                
                try
                {
                    com.CommandText = "SELECT * FROM Prescription WHERE IdPrescription = @idPres";
                    com.Parameters.AddWithValue("idPres", id);
                    db = com.ExecuteReader();
                    if(!db.Read()) return new NotFoundResult();

                    Prescription prescription = new Prescription()
                    {
                        IdPrescription = id,
                        Date = DateTime.Parse(db["Date"].ToString()),
                        DueDate = DateTime.Parse(db["DueDate"].ToString()),
                        IdPatient = Convert.ToInt32(db["IdPatient"].ToString()),
                        IdDoctor = Convert.ToInt32(db["IdDoctor"].ToString())
                    };
                    
                    db.Close();

                    com.CommandText = "SELECT * FROM Prescription_Medicament WHERE IdPrescription = @id";
                    com.Parameters.AddWithValue("id", id);
                    db = com.ExecuteReader();
                    
                    List<int> Ids = new List<int>();
                    while (db.Read())
                    {
                        Ids.Add(Convert.ToInt32(db["IdMedicament"].ToString()));
                    }
                    
                    db.Close();

                    foreach (var idMed in Ids)
                    {
                        com.CommandText = $"Select * FROM Medicament WHERE IdMedicament = {idMed}";
                        db = com.ExecuteReader();
                        if (db.Read())
                        {
                            medicaments.Add(new Medicament()
                            {
                                IdMedicament = Convert.ToInt32(db["IdMedicament"].ToString()),
                                Name = db["Name"].ToString(),
                                Description = db["Description"].ToString(),
                                Type = db["Type"].ToString()
                            });  
                        }
                        db.Close();
                    }
                    
                    return new OkObjectResult(medicaments);
                }
                catch (SqlException e)
                {
                    com.Transaction.Rollback();
                    return new BadRequestResult();
                }
            }
        }

        public IActionResult enrollPrescription(EnrollPrescriptionRequest request)
        {
            using (var client = new SqlConnection(databaseURL))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                client.Open();
                com.Transaction = client.BeginTransaction();
                SqlDataReader db;
                
                try
                {
                    if(request.DueDate < request.Date) return new BadRequestResult();
                    
                    com.CommandText = "SELECT MAX(IdPrescription) FROM Prescription";
                    db = com.ExecuteReader();
                    int maxId = Convert.ToInt32(db[0].ToString()) + 1;
                    db.Close();

                    com.CommandText = "INSERT INTO Prescription values (@id, @date, @dueDate, @idPat, @idDoc)";
                    com.Parameters.AddWithValue("id", maxId);
                    com.Parameters.AddWithValue("date", request.Date);
                    com.Parameters.AddWithValue("dueDate", request.DueDate);
                    com.Parameters.AddWithValue("idPat", request.IdPatient);
                    com.Parameters.AddWithValue("idDoc", request.IdDoctor);
                    com.ExecuteNonQuery();

                    return new OkObjectResult(maxId);
                    
                }
                catch (SqlException e)
                {
                    com.Transaction.Rollback();
                    return new BadRequestResult();
                }
            }
        }
        
    }
}