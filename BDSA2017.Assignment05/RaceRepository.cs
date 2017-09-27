using BDSA2017.Assignment05.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


using BDSA2017.Assignment05.DTOs;
using BDSA2017.Assignment05.Entities;
using Microsoft.EntityFrameworkCore;

namespace BDSA2017.Assignment05
{
    public class RaceRepository : IRaceRepository
    {
        DesignTimeDbContextFactory contextBuilder = new DesignTimeDbContextFactory();
        public (bool ok, string error) AddCarToRace(int carId, int raceId, int? startPosition = null)
        {
            using(var context = contextBuilder.CreateDbContext(null))
            {
                context.Tracks.Load();
                Race race = context.Races.Find(raceId);
                Car car = context.Cars.Find(carId);
                if(race!= null && car != null && race.ActualStart == null)
                {
                    var CarInRacesNumber = (from a in context.CarsInRace
                                           where a.Race == race
                                           select a).Count();
                   
                    if(CarInRacesNumber< race.Track.MaxCars)
                    {
                        context.CarsInRace.Add(new CarInRace() { Car = context.Cars.Find(carId), Race = context.Races.Find(raceId), StartPosition = startPosition });
                        context.SaveChanges();
                        return (true, "");
                    }
                    return (false, "Too many cars in race");

                }
                return (false, "Race or car not excisting or has started");
            }
        }

        public int Create(RaceCreateDTO race)
        {
            using(var context = contextBuilder.CreateDbContext(null))
            {
                Track track = context.Tracks.Find(race.TrackId);
                Race createdRace = new Race()
                {
                    ActualEnd = race.ActualEnd,
                    ActualStart = race.ActualStart,
                    NumberOfLaps = race.NumberOfLaps,
                    PlannedEnd = race.PlannedEnd,
                    PlannedStart = race.PlannedStart,
                    Track = track
                };
                context.Races.Add(createdRace);
                context.SaveChanges();
                return createdRace.Id;
            }
            
        }

        public (bool ok, string error) Delete(int raceId)
        {
            using (var context = contextBuilder.CreateDbContext(null))
            {
                Race race = context.Races.Find(raceId);
                if (race?.ActualStart == null)
                {
                    context.Remove(race);
                    context.SaveChanges();
                    return (true,""); ;
                   
                }
                return (false, "Race was not found or hasnt started yet");
                
            }
        }

        public void Dispose()
        {
           
        }

        public IEnumerable<RaceListDTO> Read()
        {
            throw new NotImplementedException();
        }

        public RaceCreateDTO Read(int raceId)
        {
            throw new NotImplementedException();
        }

        public (bool ok, string error) RemoveCarFromRace(int carId, int raceId)
        {
            using (var context = contextBuilder.CreateDbContext(null))
            {
                Race race = context.Races.Find(raceId);
                Car car = context.Cars.Find(carId);
                if (race != null && race.ActualStart == null && car!= null)
                {
                    CarInRace TestForCarInRace = (from carInRaces in context.CarsInRace
                                           where carInRaces.RaceId == raceId && carInRaces.CarId == carId
                                           select carInRaces).FirstOrDefault();
                    if (TestForCarInRace != null)
                    {
                        context.CarsInRace.Remove(TestForCarInRace);
                        context.SaveChanges();
                        return (true, "");
                    }
                    return (false, "The choosen car was not in the choosen race");

                }
                return (false, "Race not excisting or has started");
            }
        }

        public (bool ok, string error) Update(RaceCreateDTO race)
        {
            throw new NotImplementedException();
        }

        public (bool ok, string error) UpdateCarInRace(RaceCarDTO car)
        {
            throw new NotImplementedException();
        }
    }
}
