using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorLearn_ExampleApi.Databases.Models;

public partial class SystemRole
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string Name { get; set; } = null!;

    [StringLength(30)]
    public string Code { get; set; } = null!;

    [StringLength(50)]
    public string? Comment { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<SystemUserRole> SystemUserRoles { get; set; } = new List<SystemUserRole>();
}
