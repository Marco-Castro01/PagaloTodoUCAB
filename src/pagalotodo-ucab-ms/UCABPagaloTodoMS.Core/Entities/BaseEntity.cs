using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Core.Entities;
[ExcludeFromCodeCoverage]
public class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public bool  deleted { get; set; }

    public string? UpdatedBy { get; set; }
}
