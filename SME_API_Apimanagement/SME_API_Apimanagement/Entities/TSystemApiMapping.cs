﻿using System;
using System.Collections.Generic;

namespace SME_API_Apimanagement.Entities;

public partial class TSystemApiMapping
{
    public int Id { get; set; }

    public string? OwnerSystemCode { get; set; }

    public string? ApiServiceName { get; set; }

    public string? ApiMethod { get; set; }

    public string? ApiUrlProdInbound { get; set; }

    public string? ApiUrlUatInbound { get; set; }

    public string? ApiUser { get; set; }

    public string? ApiPassword { get; set; }

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

    public string? ApiServiceType { get; set; }

    public string? ApiUrlProdOutbound { get; set; }

    public string? ApiUrlUatOutbound { get; set; }
}
