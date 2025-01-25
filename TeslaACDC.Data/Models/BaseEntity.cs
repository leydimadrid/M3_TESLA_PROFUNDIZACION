using System;

namespace TeslaACDC.Data.Models;

public class BaseEntity<TId>
where TId : struct
{
    public TId Id { get; set; }
}
