using App.Core.Abstraction;
using App.Core.Entity;
using Microsoft.Xrm.Sdk;
using System;

namespace App.Core.Repository
{
    internal class DynamicsTimeEntryRepository : ITimeEntryRepository
    {
        private readonly IOrganizationService _organizationServiceProxy;

        public DynamicsTimeEntryRepository(IOrganizationService organizationServiceProxy)
        {
            _organizationServiceProxy = organizationServiceProxy;
        }
        public Guid Save(TimeEntry obj)
        {
            var timeEntry = new Microsoft.Xrm.Sdk.Entity("msdyn_timeentry");
            timeEntry["msdyn_start"] = obj.Start;
            timeEntry["msdyn_end"] = obj.End;
            timeEntry["msdyn_duration"] = obj.Duration;

            var id = _organizationServiceProxy.Create(timeEntry);
            return id;
        }
    }
}
