using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace BlazorLearn_ExampleApi.Databases.Models;

[Table("SystemUser_Secrets")]
public partial class SystemUserSecret
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    [Key]
    public int Id { get; set; }

    [JsonIgnore]
    [ForeignKey("UserId")]
    [InverseProperty("SystemUserSecret")]
    public virtual SystemUser User { get; set; } = null!;
}
