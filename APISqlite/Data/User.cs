using System;
using System.Collections.Generic;

namespace APISqlite.Data;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public int? Age { get; set; }
}
