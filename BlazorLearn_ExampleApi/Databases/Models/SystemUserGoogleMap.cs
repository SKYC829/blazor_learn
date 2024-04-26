using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace BlazorLearn_ExampleApi.Databases.Models;

[Table("SystemUser_Google_Map")]
public partial class SystemUserGoogleMap
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string GoogleOpenId { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("UserId")]
    [InverseProperty("SystemUserGoogleMaps")]
    public virtual SystemUser? User { get; set; }
}
