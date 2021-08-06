using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Services.Interfaces
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
    }
}
