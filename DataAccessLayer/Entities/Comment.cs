using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int ReportId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Report Report { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
