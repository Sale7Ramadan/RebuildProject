using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
