using System;
using System.Collections.Generic;

namespace WPF_CourierFirm.Model;

public partial class ContentType
{
    public int ContentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
}
