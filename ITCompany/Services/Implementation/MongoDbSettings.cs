using ITCompany.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Services.Implementation
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
    }
}
