using Kolokwium_APBD_1.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_APBD_1.Services
{
    public interface IServicePrescriptionDataBase
    {
        public IActionResult getPrescriptionById(int id);
        public IActionResult enrollPrescription(EnrollPrescriptionRequest request);
    }
}