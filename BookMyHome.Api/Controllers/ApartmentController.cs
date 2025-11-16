using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using BookMyHome.Application.Interfaces;
using BookMyHome.Application.Dtos;
using BookMyHome.Domain;
using Microsoft.Extensions.Logging;

namespace BookMyHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly ILogger<ApartmentController> _logger;

        public ApartmentController(IApartmentRepository apartmentRepository, ILogger<ApartmentController> logger)
        {
            _apartmentRepository = apartmentRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApartments()
        {
            try
            {
                var apartments = await _apartmentRepository.GetAllAsync();
                return Ok(apartments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all apartments.");
                return StatusCode(500, new { message = "An error occurred while fetching apartments.", error = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartmentById(Guid id)
        {
            try
            {
                var apartment = await _apartmentRepository.GetByIdAsync(id);
                if (apartment == null) return NotFound();
                return Ok(apartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching apartment by id {ApartmentId}.", id);
                return StatusCode(500, new { message = "An error occurred while fetching the apartment.", error = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApartmentDto>> CreateApartment([FromBody] ApartmentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var apartment = new Apartment
            {
                ApartmentID = Guid.NewGuid(),
                UserID = dto.UserID,
                Address = dto.Address,
                Description = dto.Description,
                Price = dto.Price,
                Image = dto.Image,
                AvailabiltyStatus = dto.AvailabiltyStatus
            };

            try
            {
                await _apartmentRepository.CreateAsync(apartment);
                return CreatedAtAction(nameof(GetApartmentById), new { id = apartment.ApartmentID }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating apartment for UserID {UserId}.", dto.UserID);
                // Return useful error message for debugging (include inner exception message when available)
                return BadRequest(new { message = "Failed to create apartment.", error = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartment(Guid id, [FromBody] ApartmentDto updatedApartment)
        {
            var apartment = new Apartment
            {
                ApartmentID = id,
                UserID = updatedApartment.UserID,
                Address = updatedApartment.Address,
                Description = updatedApartment.Description,
                Price = updatedApartment.Price,
                AvailabiltyStatus = updatedApartment.AvailabiltyStatus
            };

            try
            {
                var result = await _apartmentRepository.UpdateAsync(id, apartment);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating apartment {ApartmentId}.", id);
                return StatusCode(500, new { message = "Failed to update apartment.", error = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(Guid id)
        {
            try
            {
                var result = await _apartmentRepository.DeleteAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting apartment {ApartmentId}.", id);
                return StatusCode(500, new { message = "Failed to delete apartment.", error = ex.Message, detail = ex.InnerException?.Message });
            }
        }
    }
}
