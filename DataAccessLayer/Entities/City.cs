using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;
    public ICollection<User>? Users { get; set; }
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();

}
