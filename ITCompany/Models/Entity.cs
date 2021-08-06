using ITCompany.Services.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Models
{
    public abstract class Entity : IEntity
    {
        public ObjectId Id { get; set; }

      //  public DateTime CreatedAt => Id.CreationTime;
    }
}
