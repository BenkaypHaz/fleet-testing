using Library.Infraestructure.Persistence.DTOs.Auth.Users.Read;
using Library.Infraestructure.Persistence.DTOs.General.City.Read;

namespace Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Read
{
    public class SettingDispatchBranchReadFirstDto
    {
        public long Id { get; set; }
        public long GeneralCityId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public bool IsActive { get; set; }
        public CityReadFirstDto? GeneralCity { get; set; }
        public AssignedUserReadFirstDto CreatedByNavigation { get; set; } = null!;
        public AssignedUserReadFirstDto? ModifiedByNavigation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
