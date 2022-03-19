using App.Core.DTO;

namespace App.Core.Abstraction
{
    public interface ITimeEntryService
    {
        TimeEntryResponseDTO Save(TimeEntryDTO timeEntryDTO);
    }
}
