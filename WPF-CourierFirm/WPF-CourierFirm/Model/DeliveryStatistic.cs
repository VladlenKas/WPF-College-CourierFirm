﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CourierFirm.Model
{
    public class DeliveryStatistic
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthYear { get; set; }
        public double CompletedDeliveries { get; set; }
    }
}
