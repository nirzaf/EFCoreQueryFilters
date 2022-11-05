using System.ComponentModel.DataAnnotations;

namespace EFCoreQueryFilters.Models;

public class BaseAuditableEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } 
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}