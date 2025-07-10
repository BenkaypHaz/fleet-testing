namespace Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Create
{
    public class SettingDispatchBranchCreateDto
    {
        public long GeneralCityId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
    }
}