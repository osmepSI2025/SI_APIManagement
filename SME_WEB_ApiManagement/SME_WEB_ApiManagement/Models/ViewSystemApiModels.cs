namespace SME_WEB_ApiManagement.Models
{
    public class ViewSystemApiModels
    {
        public vDropdownDTO vDdlSystem { get; set; }
        public vDropdownDTO vDdlMethodApi { get; set; }
        public vDropdownDTO vDdlStatus { get; set; }
        public vDropdownDTO vDdlOrg { get; set; }
        public PagingModel PageModel { get; set; }
        public TSystemApiMappingModels TSystemAPI { get; set; }
        public List<TSystemApiMappingModels> LSysApi { get; set; }
        public List<MSystemModels> LSystem { get; set; }
        public MSystemModels MSystem { get; set; }
        public MSystemModels InsMSystem { get; set; }
    }
    public class UpSerTSystemApiMappingModels
    {

        public TSystemApiMappingModels TSystemAPI { get; set; }
        public MSystemModels InsMSystem { get; set; }
        //   public List<MSystemModels> LSystem { get; set; }

    }
}
