using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace DotNetCore.Data.Test
{
    public class PlaceRepositoryTests : IDisposable
    {
        private readonly string connectionString;
        private SqliteDbFactory dbFactory;

        public PlaceRepositoryTests()
        {
            connectionString = "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "DotNetCore.Data", "DotNetCore.sqlite");
            dbFactory = new SqliteDbFactory(connectionString);
        }

        public void Dispose()
        {
            dbFactory.Dispose();
        }

        [Fact]
        public void PlaceRepository_Get_ReturnsPlace()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                var place = unitOfWork.Places.Get(0);

                Assert.NotNull(unitOfWork);
                Assert.NotNull(place);
                Assert.Equal(0, place.Id);
            }
        }

        [Fact]
        public void PlaceRepository_Add_IsAdded()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                var place = new Place
                {
                    Name = "Test Place",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };
                var placeId = unitOfWork.Places.Add(place);
                unitOfWork.Save();

                var place2 = unitOfWork.Places.Get(placeId);

                Assert.Equal(placeId, place2.Id);
                Assert.Equal("Test Place", place2.Name);

                unitOfWork.Places.Remove(placeId);
                unitOfWork.Save();
            }
        }

        [Fact]
        public void PlaceRepository_Update_IsChanged()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                var place = unitOfWork.Places.Get(0);
                var oldValue = place.Name;

                place.Name = "Testing";
                unitOfWork.Save();

                var place2 = unitOfWork.Places.Get(0);
                var newValue = place.Name;

                place.Name = oldValue;
                unitOfWork.Save();

                Assert.Equal("Testing", newValue);
            }
        }

        [Fact]
        public void PlaceRepository_AddRange_IsAdded()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                List<Place> places = GetPlaceList();
                var placesAdded = unitOfWork.Places.AddRange(places);
                unitOfWork.Save();

                var places2 = unitOfWork.Places.GetList("Name = @Name", new { Name = "Test Place" });
                var places2Count = places2.Count();

                unitOfWork.Places.RemoveRange(places2);
                unitOfWork.Save();

                Assert.Equal(2, placesAdded);
                Assert.Equal(2, places2Count);
            }
        }

        [Fact]
        public void PlaceRepository_UpdateRange_IsUpdated()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                List<Place> places = GetPlaceList();
                var placesAdded = unitOfWork.Places.AddRange(places);
                unitOfWork.Save();

                var places2 = unitOfWork.Places.GetList("Name = @Name", new { Name = "Test Place" });
                var createdDate = new DateTime(2000, 1, 1);

                foreach (var p in places2)
                {
                    p.CreatedDate = createdDate;
                }

                var updated = unitOfWork.Places.UpdateRange(places2);
                unitOfWork.Save();

                var places3 = unitOfWork.Places.GetList("Name = @Name", new { Name = "Test Place" });
                var placesWithCreatedDate = places3.Where(m => m.CreatedDate == createdDate);

                unitOfWork.Places.RemoveRange(places3);
                unitOfWork.Save();

                Assert.Equal(2, placesAdded);
                Assert.True(updated);
                Assert.Equal(2, placesWithCreatedDate.Count());
            }
        }

        [Fact]
        public void PlaceRepository_RemoveRange_IsRemoved()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                List<Place> places = GetPlaceList();
                var placesAdded = unitOfWork.Places.AddRange(places);
                unitOfWork.Save();

                var places2 = unitOfWork.Places.GetList("Name = @Name", new { Name = "Test Place" });

                var removed = unitOfWork.Places.RemoveRange(places2);
                unitOfWork.Save();

                var places3 = unitOfWork.Places.GetList("Name = @Name", new { Name = "Test Place" });

                Assert.Equal(2, placesAdded);
                Assert.True(removed);
                Assert.Empty(places3);
            }
        }

        [Fact]
        public void PlaceRepository_Remove_IsRemoved()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                var place = new Place
                {
                    Name = "Test Place",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };
                var placeId = unitOfWork.Places.Add(place);
                unitOfWork.Save();

                var place2 = unitOfWork.Places.Get(placeId);

                unitOfWork.Places.Remove(placeId);
                unitOfWork.Save();

                var place3 = unitOfWork.Places.Get(placeId);

                Assert.Equal(placeId, place2.Id);
                Assert.Equal("Test Place", place2.Name);
                Assert.Null(place3);
            }
        }

        [Fact]
        public void PlaceRepository_GetAll_ReturnsList()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                var placeList = unitOfWork.Places.GetAll();

                Assert.NotEmpty(placeList);
            }
        }

        [Fact]
        public void PlaceRepository_GetList_ReturnsList()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                var placeList = unitOfWork.Places.GetList("Id >= 170");

                Assert.NotEmpty(placeList);
            }
        }

        [Fact]
        public void PlaceRepository_GetList_WithParam_ReturnsList()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                var placeList = unitOfWork.Places.GetList("Name like @Name", new { Name = "%norway" });

                Assert.NotEmpty(placeList);
            }
        }

        [Fact]
        public void PlaceRepository_GetListPaged_Returns5Places()
        {
            using (var unitOfWork = new UnitOfWork(dbFactory))
            {
                var placeList = unitOfWork.Places.GetListPaged("Name like @Name", new { Name = "%norway" }, string.Empty, 1, 5);

                Assert.Equal(5, placeList.Count());
            }
        }

        private static List<Place> GetPlaceList()
        {
            return new List<Place>()
                {
                    new Place
                    {
                        Name = "Test Place",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                    },
                    new Place
                    {
                        Name = "Test Place",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                    },

                };
        }
    }
}
