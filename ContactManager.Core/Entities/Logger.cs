using ContactManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContactManager.Core.Entities
{
    public class LogInfo : BaseEntity, IAggregateRoot
    {
        public string? Message { get; set; }
        public string? InnerMessage { get; set; }
        public string? Source { get; set; }
        public string? StackTrace { get; set; }
        public string? ExtraData { get; set; }
    }
}
