using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsApiBackend.Models;
using PetsApiBackend.Models.DTO;
using PetsApiBackend.Models.Repository;

namespace PetsApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        private readonly IPetRepository _petRepository;

        // INICIALIZAMOS EL CONSTRUCTOR CON EL CONTEXTO DE LA BASE DE DATOS
        public PetsController(AplicationDbContext context, IMapper mapper, IPetRepository petRepository)
        {
            _context = context;
            _mapper = mapper; // RECORDAR QUE TODO EL PROSESO DE LOS DTO ES OPCIONAL
            _petRepository = petRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listPets = await _context.Pets.ToListAsync();
                //var listPets = await _petRepository.GetListPets(); //--> uso con patron repositorio

                var listPetsDTO = _mapper.Map<IEnumerable<PetDTO>>(listPets);
                return Ok(listPetsDTO);
                //return Ok(listPets);
            }
            catch (Exception error) 
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var pet = await _context.Pets.FindAsync(id);
                //var pet = await _petRepository.GetPetDetail(id); //--> uso con patron repositorio
                if (pet == null)
                {
                    return NotFound();
                }

                var petDTO = _mapper.Map<PetDTO>(pet);
                return Ok(petDTO);
                //return Ok(pet);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pet = await _context.Pets.FindAsync(id);
                //var pet = await _petRepository.GetPetDetail(id); //--> uso con patron repositorio
                if (pet== null)
                {
                    return NotFound();
                }
                //await _petRepository.DeletePet(pet); //--> uso con patron repositorio
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PetDTO petDTO) //Post(Pet pet)
        {
            try
            {
                var pet_Dto = _mapper.Map<Pet>(petDTO);
                pet_Dto.createDate = DateTime.Now;
                //var newPet =_petRepository.AddPet(pet_Dto); //--> uso con patron repositorio
                _context.Pets.Add(pet_Dto);
                await _context.SaveChangesAsync();
                var pet_Item_Dto = _mapper.Map<PetDTO>(pet_Dto);
                return Ok(pet_Item_Dto);

                //pet.createDate = DateTime.Now;
                //_context.Pets.Add(pet);;
                //await _context.SaveChangesAsync();
                //return Ok(pet);

                // //return CreatedAtAction("Get", new { id = pet.Id}, pet);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PetDTO petDTO) // Put(int id, Pet pet)
        {
            try
            {
                var pet = _mapper.Map<Pet>(petDTO); // OPCIONAL para el uso con mapeo DTO

                if(id != pet.Id)
                {
                    return NotFound();
                }
                var petChanges = await _context.Pets.FindAsync(id);
                if (petChanges == null)
                {
                    return NotFound();
                }
                //petChanges.Id = id;
                petChanges.Name = pet.Name;
                petChanges.Age = pet.Age;
                petChanges.Race = pet.Race; 
                petChanges.Weight = pet.Weight;
                petChanges.Description = pet.Description;
                petChanges.Color = pet.Color;
                //petChanges.createDate = pet.createDate;
                //_context.Update(petChanges);
                await _context.SaveChangesAsync();
                return Ok(pet);

            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
