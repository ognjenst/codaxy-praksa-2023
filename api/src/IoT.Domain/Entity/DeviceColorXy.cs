﻿using System.ComponentModel.DataAnnotations;

namespace IoT.Domain.Entity;

public class DeviceColorXy
{
    [Range(0, 1)]
    public decimal X { get; set; }

    [Range(0, 1)]
    public decimal Y { get; set; }
}
