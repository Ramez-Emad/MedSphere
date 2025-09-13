﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Entities.Medicine;
public class MedicineImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = default!;
    public bool IsPrimary { get; set; }

    public int MedicineId { get; set; }
    public Medicine Medicine { get; set; } = default!;
}
