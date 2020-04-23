using System;
using System.Collections.Generic;
using Kolokwium_APBD_1.Properties.Model;

namespace Kolokwium_APBD_1.DTOs.Response
{
    public class PrescriptionResponse
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public List<Medicament> Medicaments { get; set; } 
    }
}