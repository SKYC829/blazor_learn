using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorLearn_ExampleApi.Databases.Models;

public partial class SystemUser
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(12)]
    public string Name { get; set; } = null!;

    [InverseProperty("User")]
    public virtual SystemUserGoogleMap? SystemUserGoogleMaps { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<SystemUserRole> SystemUserRoles { get; set; } = new List<SystemUserRole>();

    [InverseProperty("User")]
    public virtual SystemUserSecret? SystemUserSecret { get; set; }
}
