using App.Core.DTO;
using System.Collections.Generic;

namespace App.Core.Abstraction
{
    public interface IRequestValidator
    {
        IList<string> Validate(TimeEntryDTO timeEntryDTO);
    }
}
