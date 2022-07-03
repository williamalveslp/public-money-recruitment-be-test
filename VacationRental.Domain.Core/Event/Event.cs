﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Domain.Core.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
