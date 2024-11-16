using System;
using System.Collections.Generic;

namespace pharmacy.models;

public partial class Supplier
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<DrugsOnSupHand> DrugsOnSupHands { get; set; } = new List<DrugsOnSupHand>();

    public virtual ICollection<Pharmacy> Pharmacies { get; set; } = new List<Pharmacy>();
}
