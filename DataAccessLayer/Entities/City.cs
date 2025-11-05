using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
