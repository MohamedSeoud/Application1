using Application1.DTO_s;

namespace Application1.Data
{
    public class DataStore
    {
        public static List<VillaDTO> villaDTOs = new List<VillaDTO>
        {
           new VillaDTO{Id = 1, Name ="Mohamed"},
           new VillaDTO{Id = 2, Name ="Ahmed"}
        };
    }
}
