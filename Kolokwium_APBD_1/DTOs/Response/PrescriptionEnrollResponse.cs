using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Kolokwium_APBD_1.DAL;

namespace Kolokwium_APBD_1.DTOs.Response
{
    public class PrescriptionEnrollResponse
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
    }
}