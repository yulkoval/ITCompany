using Microsoft.EntityFrameworkCore;
using Services.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Database.Interfaces
{
    public interface IDataContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Developer> Developers { get; set; }
    }
}
