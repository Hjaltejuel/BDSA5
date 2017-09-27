using BDSA2017.Assignment05.Entities;
using BDSA2017.Assignment05;
using Xunit;
using System.Linq;
using System;
using BDSA2017.Assignment05.Repositories;
using BDSA2017.Assignment05.DTOs;

namespace BDSA2017.Assignment05.Tests
{
    public class RaceRepositoryTests
    {
        DesignTimeDbContextFactory contextBuilder = new DesignTimeDbContextFactory();
        RaceRepository raceRepository = new RaceRepository();





        [Fact]
        public void TestRemoveCarFromRace()
        {
            using (var context = contextBuilder.CreateDbContext(null))
            {
                Car car = new Car() { Driver = "Mads", Name = "Suzuki" };
                var track = new Track()
                {
                    BestTime = 121213123,
                    LengthInMeters = 123214,
                    MaxCars = 50,
                    Name = "RaceTrack"
                };
                var race = new Race()
                {

                    NumberOfLaps = 5,
                    PlannedEnd = new DateTime(1920, 11, 11),
                    PlannedStart = new DateTime(1920, 11, 11),
                    Track = track
                };
                var carInRace = new CarInRace() { Car = car, Race = race };
                context.Add(carInRace);
                context.SaveChanges();
                raceRepository.RemoveCarFromRace(car.Id, race.Id);
                
                var test = context.CarsInRace.Find(carInRace.CarId, carInRace.RaceId);

                Assert.Null(context.CarsInRace.Find(carInRace.CarId,carInRace.RaceId));

            }
        }
        [Fact]
        public void TestAddCarToRace()
        {
            using (var context = contextBuilder.CreateDbContext(null))
            {
                Car car = new Car() { Driver = "Mads", Name = "Suzuki" };
                var track = new Track()
                {
                    BestTime = 121213123,
                    LengthInMeters = 123214,
                    MaxCars = 50,
                    Name = "RaceTrack"
                };
                var race = new Race()
                {
                    
                    NumberOfLaps = 5,
                    PlannedEnd = new DateTime(1920, 11, 11),
                    PlannedStart = new DateTime(1920, 11, 11),
                    Track = track
                };
               
                context.Cars.Add(car);
                context.Races.Add(race);
                context.SaveChanges();
                raceRepository.AddCarToRace(car.Id, race.Id, 5);
                var carInRace = (from carInRaces in context.CarsInRace
                                where carInRaces.CarId == car.Id && carInRaces.RaceId == race.Id
                                select carInRaces).Count();
                Assert.True(carInRace>0);

            }
        }
        [Fact]
        public void TestAddCarToRaceFalseRaceHasStarted()
        {
            using (var context = contextBuilder.CreateDbContext(null))
            {
                Car car = new Car() { Driver = "Mads", Name = "Suzuki" };
                var track = new Track()
                {
                    BestTime = 121213123,
                    LengthInMeters = 123214,
                    MaxCars = 50,
                    Name = "RaceTrack"
                };
                var race = new Race()
                {
                   
                    NumberOfLaps = 5,
                    ActualStart = new DateTime(1231,04,11),
                    PlannedEnd = new DateTime(1920, 11, 11),
                    PlannedStart = new DateTime(1920, 11, 11),
                    Track = track
                };
                context.Cars.Add(car);
                context.Races.Add(race);
                context.SaveChanges();
                
              
                Assert.Equal((false, "Race or car not excisting or has started"), raceRepository.AddCarToRace(car.Id, race.Id, 5));

            }
        }
        [Fact]
        public void TestCreateRace()
        {
              using (var context = contextBuilder.CreateDbContext(null))
            {
                var track = new Track()
                {
                    BestTime = 121213123,
                    LengthInMeters = 123214,
                    MaxCars = 50,
                    Name = "RaceTrack"
                };
                context.Add(track);
                context.SaveChanges();
                var raceDTO = new RaceCreateDTO()
                {
                    ActualEnd = new DateTime(1920, 11, 11),
                    ActualStart = new DateTime(1920, 11, 11),
                    NumberOfLaps = 5,
                    PlannedEnd = new DateTime(1920, 11, 11),
                    PlannedStart = new DateTime(1920, 11, 11),
                    TrackId = track.Id
                };
                
                  Assert.NotNull(context.Races.Find(raceRepository.Create(raceDTO)));
               

            }
             
        }
        [Fact]
        public void TestDeleteRace()
        {
            
            using (var context = contextBuilder.CreateDbContext(null))
            {
                
                var track = new Track()
                {
                    BestTime = 121213123,
                    LengthInMeters = 123214,
                    MaxCars = 50,
                    Name = "RaceTrack"
                };
                var race = new Race()
                {
                    ActualEnd = new DateTime(1920, 11, 11),
                    ActualStart = new DateTime(1920, 11, 11),
                    NumberOfLaps = 5,
                    PlannedEnd = new DateTime(1920, 11, 11),
                    PlannedStart = new DateTime(1920, 11, 11),
                    Track = track
                };
                context.Tracks.Add(track);
                context.Races.Add(race);
                context.SaveChanges();
                Assert.False(raceRepository.Delete(race.Id).ok);

            }
           

        }
        [Fact]
        public void TestDeleteRace2()
        {

            RaceRepository raceRepository = new RaceRepository();
            using (var context = contextBuilder.CreateDbContext(null))
            {

                var track = new Track()
                {
                    BestTime = 121213123,
                    LengthInMeters = 123214,
                    MaxCars = 50,
                    Name = "RaceTrack"
                };
                var race = new Race()
                {
                    NumberOfLaps = 5,
                    PlannedEnd = new DateTime(1920, 11, 11),
                    PlannedStart = new DateTime(1920, 11, 11),
                    Track = track,
                };
                context.Tracks.Add(track);
                context.Races.Add(race);
                context.SaveChanges();
                Assert.True(raceRepository.Delete(race.Id).ok);

            }


        }
    }
}
