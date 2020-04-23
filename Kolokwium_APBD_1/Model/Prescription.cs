using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Kolokwium_APBD_1.DAL;

namespace Kolokwium_APBD_1.Properties.Model
{
    public class Prescription
    {
        [Required]
        public int IdPrescription { get; set; }
        [Required]
        [JsonConverter(typeof(CustomDateTimeConverter.CustonDateTimeConverter))]
        public DateTime Date { get; set; }
        [Required]
        [JsonConverter(typeof(CustomDateTimeConverter.CustonDateTimeConverter))]
        public DateTime DueDate { get; set; }
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public int IdDoctor { get; set; }
    }
}