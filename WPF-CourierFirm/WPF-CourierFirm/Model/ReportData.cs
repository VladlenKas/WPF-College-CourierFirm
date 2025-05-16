using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CourierFrim.Model
{
    public class ReportData
    {
        public string NumberDelivery { get; set; } = null!;

        public string FullnameCourier { get; set; } = null!;

        public string FullnameClient { get; set; } = null!;

        public string OrganisationName { get; set; } = null!;

        public string RateName { get; set; } = null!;

        public string ContentTypeName { get; set; } = null!;

        public string PaymentMethod { get; set; } = null!;
    }
}
