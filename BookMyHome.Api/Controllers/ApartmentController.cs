using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using BookMyHome.Application.Interfaces;
using BookMyHome.Application.Dtos;
using BookMyHome.Domain;

namespace BookMyHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentController(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApartments()
        {
            var apartments = await _apartmentRepository.GetAllAsync();
            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartmentById(Guid id)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id);
            if (apartment == null) return NotFound();
            return Ok(apartment);
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
                AvailabiltyStatus = dto.AvailabiltyStatus
            };
            await _apartmentRepository.CreateAsync(apartment);
            return CreatedAtAction(nameof(GetApartmentById), new { id = apartment.ApartmentID }, dto);
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
            var result = await _apartmentRepository.UpdateAsync(id, apartment);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(Guid id)
        {
            var result = await _apartmentRepository.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
