using DogApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogApp.Abstractions
{
    public interface IDogService
    {
        bool Create(string name, int age, string breed, string picture);
        bool Update(int id, string name, int age, string breed, string picture);
        List<Dog> GetDogs();
        Dog GetDogById(int id);
        bool RemoveById(int id);
        List<Dog> GetDogs(string searchStringBreed, string searchStringName);
    }
}
