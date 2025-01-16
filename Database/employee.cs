using System;
using System.Collections.Generic;

namespace InternBackendC_.Database;

public partial class employee
{
    public string employee_id { get; set; }

    public string firstname { get; set; }

    public string lastname { get; set; }

    public string email { get; set; }

    public string date_of_birth { get; set; }

    public bool is_enable { get; set; }

    public string team_id { get; set; }

    public string position_id { get; set; }

    public virtual ICollection<phone> phones { get; set; } = new List<phone>();

    public virtual position position { get; set; }

    public virtual team team { get; set; }
}
