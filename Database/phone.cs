using System;
using System.Collections.Generic;

namespace InternBackendC_.Database;

public partial class phone
{
    public string phone_id { get; set; }

    public string employee_id { get; set; }

    public string phone_number { get; set; }

    public virtual employee employee { get; set; }
}
