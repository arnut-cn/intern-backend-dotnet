using System;
using System.Collections.Generic;

namespace InternBackendC_.Database;

public partial class position
{
    public string position_id { get; set; }

    public string name { get; set; }

    public string? description { get; set; }

    public bool is_enable { get; set; }

    public virtual ICollection<employee> employees { get; set; } = new List<employee>();
}
