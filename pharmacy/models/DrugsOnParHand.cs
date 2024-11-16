using System;
using System.Collections.Generic;

namespace pharmacy.models;

public partial class DrugsOnParHand
{
    public int DrugId { get; set; }

    public string DrugName { get; set; } = null!;

    public int? PharmacyId { get; set; }

    public decimal? Price { get; set; }

    public int? Filling { get; set; }

    public string? BestBeforeDate { get; set; }

    public int? Amount { get; set; }

    public virtual Pharmacy? Pharmacy { get; set; }
}
