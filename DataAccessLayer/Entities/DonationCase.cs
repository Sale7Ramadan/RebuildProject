using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class DonationCase
{
    public int DonationCaseId { get; set; }

    public int ReportId { get; set; }

    public decimal GoalAmount { get; set; }

    public decimal? CollectedAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? OpenedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual Report Report { get; set; } = null!;
}
