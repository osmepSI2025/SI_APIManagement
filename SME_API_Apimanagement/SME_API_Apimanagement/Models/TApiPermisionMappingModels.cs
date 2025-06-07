using SME_API_Apimanagement.Entities;
using System.ComponentModel.DataAnnotations;

namespace SME_API_Apimanagement.Models
{
    public class TApiPermisionMappingModels
    {
        public int Id { get; set; }

        public string? OrganizationCode { get; set; }

        public string? SystemCode { get; set; }

        public string? ApiKey { get; set; }

        public string? ApiSystemCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? FlagActive { get; set; }

        public string? FlagDelete { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
        public string? SystemName { get; set; }

        public int? SystemApiMappingId { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsSelected { get; set; }
    }
    public partial class ApiPermisionApiRespone
    {
        public string? responseCode { get; set; }
        public string? responseMsg { get; set; }
        public List<TApiPermisionRespone> result { get; set; } = new List<TApiPermisionRespone>();
    }
    public class TApiPermisionRespone   
    {
     
        public string? BusinessId { get; set; }      

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? SystemName { get; set; }

        public string? ServiceName { get; set; }

        public bool? IsSelected { get; set; }
    }
}

