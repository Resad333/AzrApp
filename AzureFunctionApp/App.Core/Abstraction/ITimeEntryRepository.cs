using App.Core.Entity;
using System;

namespace App.Core.Abstraction
{
    public interface ITimeEntryRepository
    {
        Guid Save(TimeEntry obj);
    }
}
