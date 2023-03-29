using AutoMapper;
using PetsApiBackend.Models.DTO;

namespace PetsApiBackend.Models.Profiles
{
    public class PetProfile: Profile
    {
        public PetProfile() 
        {
            CreateMap<Pet, PetDTO>();
            CreateMap<PetDTO, Pet>();
        }
    }
}
