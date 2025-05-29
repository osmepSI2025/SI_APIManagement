namespace SME_WEB_ApiManagement.Models
{
    public class ViewRegisterApiModels
    {
        public vDropdownDTO vDdlSystem { get; set; }
        public vDropdownDTO vDdlOrg { get; set; }
        public vDropdownDTO vDdlStatus { get; set; }
        public PagingModel PageModel { get; set; }
        public MRegisterModels MRegister { get; set; }
        public List<TApiPermisionMappingModels> LApi { get; set; }
        public List<MSystemModels> LSystem { get; set; }
        public List<MRegisterModels> LRegis { get; set; }
        public MOrganizationModels MOrg { get; set; }
        public List<MOrganizationModels> LOrg { get; set; }
        public MOrganizationModels InsMOrg { get; set; }
    }
    public class UpSertRegisterApiModels
    {
     
        public MRegisterModels MRegister { get; set; }
       
        public List<MSystemModels> LSystem { get; set; }

    }
}
