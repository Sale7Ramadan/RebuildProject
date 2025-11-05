using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class Donation
{
    public int DonationsId { get; set; }

    public int DonationCaseId { get; set; }

    public int? UserId { get; set; }

    public string? DonorName { get; set; }

    public decimal Amount { get; set; }

    public DateTime? DonatedAt { get; set; }

    public virtual DonationCase DonationCase { get; set; } = null!;

    public virtual User? User { get; set; }
}
