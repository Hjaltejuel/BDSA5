# Assignment #5: Week 39

## Software Engineering

### Exercise 1

## C&#35;

Fork this repository and implement the code required for the assignments below.

### Slot Car Tournament part deux

![](images/c2960.jpg "Scalextric C2960 Aston Martin DBR9 Gulf")

Implement and test the `IRaceRepository` interface.

```csharp
public interface IRaceRepository : IDisposable
{
    int Create(RaceCreateDTO race);
    IEnumerable<RaceListDTO> Read();
    RaceCreateDTO Read(int raceId);
    (bool ok, string error) Update(RaceCreateDTO race);
    (bool ok, string error) AddCarToRace(int carId, int raceId, int? startPosition = null);
    (bool ok, string error) UpdateCarInRace(RaceCarDTO car);
    (bool ok, string error) RemoveCarFromRace(int carId, int raceId);
    (bool ok, string error) Delete(int raceId);
}
```

with the following rules:

- Once a race is started (actual start time != `null`) it cannot be deleted.
- Cars can only be added or removed from a race before start.
- You cannot add more cars to a race than the track supports.

Your code should not throw exceptions. Instead, if for instance someone is trying to add a car which does exist to a race which does not exist:

```csharp
return (false, "race not found");
```

Your code must use an in-memory database and/or mocks for testing.

## Submitting the assignment

To submit the assignment you need to create a .pdf document using LaTeX containing the answers to the questions and a link to a public repository containing your fork of the completed code.
Upload the document to Peergrade.
