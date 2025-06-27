

namespace Library.Infraestructure.Persistence.DTOs.General.Country.Update
{
    public class CountryUpdateDto
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string ISO_Code { get; set; }
        public bool IsActive { get; set; }
    }
}
