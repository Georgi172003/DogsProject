using DogApp.Abstractions;
using DogApp.Data;
using DogApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogApp.Services
{
    public class DogService :IDogService
    {
        private readonly ApplicationDbContext context;

        public DogService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public bool Create(string name, int age, string breed, string picture)
        {
            Dog dogFromDb = new Dog
            {
                Name = name,
                Age = age,
                Breed = breed,
                Picture = picture,
            };

            context.Dogs.Add(dogFromDb);
            return context.SaveChanges() != 0;
        }

        public Dog GetDogById(int id)
        {
            return context.Dogs.Find(id);
        }

        public List<Dog> GetDogs()
        {
            return context.Dogs.ToList();
        }

        public bool RemoveById(int id)
        {
            var dog = GetDogById(id);
            if (dog == null)
            {
                return false;
            }
            context.Remove(dog);
            return context.SaveChanges() != 0;
        }

        public bool Update(int id, string name, int age, string breed, string picture)
        {
            var dog = GetDogById(id);
            if (dog == null)
            {
                return false;
            }
            dog.Name = name;
            dog.Age = age;
            dog.Breed = breed;
            dog.Picture = picture;
            context.Update(dog);
            return context.SaveChanges() != 0;
        }
        public List<Dog> GetDogs(string searchStringBreed, string searchStringName)
        {
            var dogs = context.Dogs.ToList();

            if (!String.IsNullOrEmpty(searchStringBreed) && !String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(d => d.Breed.Contains(searchStringBreed) && d.Name.Contains(searchStringName)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBreed))
            {
                dogs = dogs.Where(d => d.Breed.Contains(searchStringBreed)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(d => d.Name.Contains(searchStringName)).ToList();
            }
            return dogs;
        }
    }
}
