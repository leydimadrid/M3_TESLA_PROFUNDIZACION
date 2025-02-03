using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeslaACDC.Data.Models;

public class BaseEntity<TId>
where TId : struct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TId Id { get; set; }
}
