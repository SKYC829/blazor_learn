using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace BlazorLearn_ExampleApi.Databases.Models;

[Table("SystemUser_Roles")]
public partial class SystemUserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    [Key]
    public int Id { get; set; }

    [JsonIgnore]
    [ForeignKey("RoleId")]
    [InverseProperty("SystemUserRoles")]
    public virtual SystemRole Role { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("UserId")]
    [InverseProperty("SystemUserRoles")]
    public virtual SystemUser User { get; set; } = null!;
}
