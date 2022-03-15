using System;
using System.ComponentModel.DataAnnotations;
using TrackId.Data.Interfaces;

namespace TrackId.Data.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
