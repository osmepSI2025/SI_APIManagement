using System;
using System.Collections.Generic;

namespace SME_API_Apimanagement.Entities;

public partial class TSystemApi
{
    public int Id { get; set; }

    public string? OwnerSystemCode { get; set; }

    public string? ApiServiceName { get; set; }

    public string? ApiMethod { get; set; }

    public string? ApiUrlProd { get; set; }

    public string? ApiUrlUat { get; set; }

    public string? ApiKey { get; set; }

    public string? ApiRequestExample { get; set; }

    public string? ApiResponseExample { get; set; }

    public string? ApiNote { get; set; }

    public bool? FlagActive { get; set; }

    public string? FlagDelete { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
