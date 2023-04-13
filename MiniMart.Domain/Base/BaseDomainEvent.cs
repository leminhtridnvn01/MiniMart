﻿using MediatR;

namespace MiniMart.Domain.Base
{
    public abstract class BaseDomainEvent : INotification
    {
        public BaseDomainEvent()
        {
            EventId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public virtual Guid EventId { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
