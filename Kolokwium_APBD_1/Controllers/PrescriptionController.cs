using Kolokwium_APBD_1.DTOs.Request;
using Kolokwium_APBD_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_APBD_1.Controllers
{
    [ApiController]
    [Route("api/prescription")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IServicePrescriptionDataBase _service;

        public PrescriptionController(IServicePrescriptionDataBase service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult getPrescriptionById(int id)
        {
            return _service.getPrescriptionById(id);
        }

        [HttpPost("enrollments")]
        public IActionResult enrollPrecription(EnrollPrescriptionRequest request)
        {
            return _service.enrollPrescription(request);
        }
    }
}