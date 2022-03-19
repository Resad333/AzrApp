using App.Core.Abstraction;
using App.Core.DTO;
using App.Core.Entity;
using App.Core.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace AppTest.Service
{
    public class DynamicsTimeEntryServiceTests
    {
        [Fact]
        public void Should_Save_All_Days_Between_2_Dates()
        {
            var timeEntryDTO = new TimeEntryDTO()
            {
                Schema = "http://json-schema.org/draft-04/schema#",
                Type = "object",
                Required = new List<string> { "StartOn", "EndOn" },
                Properties = new Properties
                {
                    StartOn = new Property { Type = "15-03-2022", Format = "dd-MM-yyyy" },
                    EndOn = new Property { Type = "17-03-2022", Format = "dd-MM-yyyy" }
                }
            };

            var ids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            var service = new DynamicsTimeEntryService(createMockRepository(ids));
            var result = service.Save(timeEntryDTO);
            var savedIdList = result.SavedDays.Select(x => x.Id);

            Assert.True(result.FailedDays.Count() == 0);
            Assert.True(savedIdList.Count() == 3);
            Assert.Contains(ids[0], savedIdList);
            Assert.Contains(ids[1], savedIdList);
            Assert.Contains(ids[2], savedIdList);

            ITimeEntryRepository createMockRepository(List<Guid> idList)
            {
                var day = DateTime.ParseExact("15-03-2022", "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                var repositoryMock = new Mock<ITimeEntryRepository>();

                var day1 = day;
                var timeEntry1 = new TimeEntry { Start = day1, End = day1, Duration = 1 };
                var id1 = idList[0];
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry1.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry1.End, a.End) == 0))
                                     ).Returns(id1);

                var day2 = day.AddDays(1).Date;
                var timeEntry2 = new TimeEntry { Start = day2, End = day2, Duration = 1 };
                var id2 = idList[1];
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry2.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry2.End, a.End) == 0))
                                     ).Returns(id2);

                var day3 = day.AddDays(2).Date;
                var timeEntry3 = new TimeEntry { Start = day3, End = day3, Duration = 1 };
                var id3 = idList[2];
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry3.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry3.End, a.End) == 0))
                                     ).Returns(id3);

                return repositoryMock.Object;
            }

        }

        [Fact]
        public void Should_Not_Save_Some_Days_Between_2_Dates()
        {
            var timeEntryDTO = new TimeEntryDTO()
            {
                Schema = "http://json-schema.org/draft-04/schema#",
                Type = "object",
                Required = new List<string> { "StartOn", "EndOn" },
                Properties = new Properties
                {
                    StartOn = new Property { Type = "15-03-2022", Format = "dd-MM-yyyy" },
                    EndOn = new Property { Type = "17-03-2022", Format = "dd-MM-yyyy" }
                }
            };

            var ids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            var service = new DynamicsTimeEntryService(createMockRepository(ids));
            var result = service.Save(timeEntryDTO);
            var savedIdList = result.SavedDays.Select(x => x.Id);

            Assert.True(result.FailedDays.Count() == 1);
            Assert.True(savedIdList.Count() == 2);
            Assert.Contains(ids[0], savedIdList);
            Assert.Contains(ids[2], savedIdList);

            ITimeEntryRepository createMockRepository(List<Guid> idList)
            {
                var day = DateTime.ParseExact("15-03-2022", "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                var repositoryMock = new Mock<ITimeEntryRepository>();

                var day1 = day;
                var timeEntry1 = new TimeEntry { Start = day1, End = day1, Duration = 1 };
                var id1 = idList[0];
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry1.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry1.End, a.End) == 0))
                                     ).Returns(id1);

                var day2 = day.AddDays(1).Date;
                var timeEntry2 = new TimeEntry { Start = day2, End = day2, Duration = 1 };
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry2.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry2.End, a.End) == 0))
                                     ).Throws(new Exception($"{day2} failed while saving"));

                var day3 = day.AddDays(2).Date;
                var timeEntry3 = new TimeEntry { Start = day3, End = day3, Duration = 1 };
                var id3 = idList[2];
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry3.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry3.End, a.End) == 0))
                                     ).Returns(id3);

                return repositoryMock.Object;
            }

        }

        [Fact]
        public void Should_Not_Save_All_Days_Between_2_Dates()
        {
            var timeEntryDTO = new TimeEntryDTO()
            {
                Schema = "http://json-schema.org/draft-04/schema#",
                Type = "object",
                Required = new List<string> { "StartOn", "EndOn" },
                Properties = new Properties
                {
                    StartOn = new Property { Type = "15-03-2022", Format = "dd-MM-yyyy" },
                    EndOn = new Property { Type = "17-03-2022", Format = "dd-MM-yyyy" }
                }
            };

            var ids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            var service = new DynamicsTimeEntryService(createMockRepository(ids));
            var result = service.Save(timeEntryDTO);
            var savedIdList = result.SavedDays.Select(x => x.Id);

            Assert.True(result.FailedDays.Count() == 3);
            Assert.True(savedIdList.Count() == 0);

            ITimeEntryRepository createMockRepository(List<Guid> idList)
            {
                var day = DateTime.ParseExact("15-03-2022", "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                var repositoryMock = new Mock<ITimeEntryRepository>();

                var day1 = day;
                var timeEntry1 = new TimeEntry { Start = day1, End = day1, Duration = 1 };
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry1.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry1.End, a.End) == 0))
                                     ).Throws(new Exception($"{day1} failed while saving"));

                var day2 = day.AddDays(1).Date;
                var timeEntry2 = new TimeEntry { Start = day2, End = day2, Duration = 1 };
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry2.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry2.End, a.End) == 0))
                                     ).Throws(new Exception($"{day2} failed while saving"));

                var day3 = day.AddDays(2).Date;
                var timeEntry3 = new TimeEntry { Start = day3, End = day3, Duration = 1 };
                repositoryMock.Setup(x => x.Save(It.Is<TimeEntry>(a => DateTime.Compare(timeEntry3.Start, a.Start) == 0 &&
                                                                       DateTime.Compare(timeEntry3.End, a.End) == 0))
                                     ).Throws(new Exception($"{day3} failed while saving"));

                return repositoryMock.Object;
            }

        }
    }
}
