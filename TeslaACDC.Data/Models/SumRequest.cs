using System;

namespace TeslaACDC.Data.Models;

public class SumRequest
{
    public float Value2 { get; set; }
    public float Value1 { get; set; }

    public float Sum()
    {
        return Value1 + Value2;
    }

}
