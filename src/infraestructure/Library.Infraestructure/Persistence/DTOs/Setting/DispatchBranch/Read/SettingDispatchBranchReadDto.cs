
namespace Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Read
{
    public class SettingDispatchBranchReadDto
    {
        public long Id { get; set; }
        public long GeneralCityId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public bool IsActive { get; set; }
        public string? CityName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
