namespace PetsApiBackend.Models.Repository
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetListPets();

        Task<Pet> GetPetDetail(int id);

        Task DeletePet(Pet pet);

        Task<Pet> AddPet(Pet pet);

        Task<Pet> UpdatePet(int id, Pet pet);
    }
}
