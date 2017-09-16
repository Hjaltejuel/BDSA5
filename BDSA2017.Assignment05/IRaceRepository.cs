using BDSA2017.Assignment05.DTOs;
using System;
using System.Collections.Generic;

namespace BDSA2017.Assignment05.Repositories
{
    public interface IRaceRepository : IDisposable
    {
        int Create(RaceDTO race);
        IEnumerable<RaceListDTO> Read();
        RaceDTO Read(int raceId);
        (bool ok, string error) Update(RaceDTO race);
        (bool ok, string error) AddCarToRace(int carId, int raceId);
        (bool ok, string error) RemoveCarFromRace(int carId, int raceId);
        (bool ok, string error) Delete(int raceId);
    }
}
