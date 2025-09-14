﻿namespace MedSphere.DAL.Entities;
public class AuditableEntity : BaseEntity
{
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? UpdatedById { get; set; }
    public DateTime? UpdatedOn { get; set; }

    //public ApplicationUser CreatedBy { get; set; } = default!;
    //public ApplicationUser? UpdatedBy { get; set; }
}
