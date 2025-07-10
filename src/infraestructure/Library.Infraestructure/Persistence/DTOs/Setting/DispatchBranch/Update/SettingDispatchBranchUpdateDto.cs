namespace Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Update
{
    public class SettingDispatchBranchUpdateDto
    {
        public long GeneralCityId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public bool IsActive { get; set; }
    }
}