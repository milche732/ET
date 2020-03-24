using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Domain.Domains.Events
{
    public class DomainEvent : INotification
    {
        /// <summary>
        /// Use auto Id for idempotency
        /// </summary>
        public string Id => Guid.NewGuid().ToString();
        public DateTime DateCreated { get; private set; } = DateTime.Now;
    }
}
