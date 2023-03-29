using Microsoft.EntityFrameworkCore;

namespace PetsApiBackend.Models.Repository
{
    public class PetRepository:  IPetRepository
    {
        public readonly AplicationDbContext _context;

        public PetRepository(AplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Pet>> GetListPets()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet> GetPetDetail(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            return pet;
        }
        public async Task DeletePet(Pet pet)
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
        }

        public async Task<Pet> AddPet(Pet pet)
        {
            _context.Pets.Add(pet);;
            await _context.SaveChangesAsync();
            return pet;
        }

        public async Task<Pet> UpdatePet(int id, Pet pet)
        {
            var petChanges = await _context.Pets.FirstOrDefaultAsync(pet => pet.Id == id);

            petChanges.Name = pet.Name;
            petChanges.Age = pet.Age;
            petChanges.Race = pet.Race;
            petChanges.Weight = pet.Weight;
            petChanges.Description = pet.Description;
            petChanges.Color = pet.Color;

            await _context.SaveChangesAsync();

            return petChanges;
        }
    }
}
