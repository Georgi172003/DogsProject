using DogApp.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogApp.Models;
using DogApp.Domain;
using DogApp.Services;
using Microsoft.AspNetCore.Http;
using DogApp.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace DogApp.Controllers
{
    [Authorize]
    public class DogsController : Controller
    {
        private readonly IDogService service;

        public DogsController(IDogService _service)
        {
            this.service = _service;
        }

        public IActionResult Create()
        {
             return this.View();      
        }

        [HttpPost]
        public IActionResult Create(DogCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {

                var created = service.Create(bindingModel.Name,bindingModel.Age, bindingModel.Breed,bindingModel.Picture);

                if (created)
                {
                    return this.RedirectToAction("Success");
                }             
            }      
            return this.View();
        }
        public IActionResult Edit(int id)
        {
            Dog item = service.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
            DogCreateViewModel dog = new DogCreateViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture=item.Picture
            };
            return View(dog);
        }

        [HttpPost]
        public IActionResult Edit(int id, DogCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {  
                var updated = service.Update(id, bindingModel.Name, bindingModel.Age, bindingModel.Breed, bindingModel.Picture);
                if (updated)
                {
                    return this.RedirectToAction("All");
                }

            }
            return View(bindingModel);
        }     

        public IActionResult Delete(int id)
        {
            Dog item = service.GetDogById(id);

            if (item == null)
            {
                return NotFound();
            }
            DogCreateViewModel dog = new DogCreateViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture
            };
            return View(dog);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = service.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("All", "Dogs");
            }
            return View();
            
        }
        public IActionResult Success()
        {
            return this.View();
        }
        [AllowAnonymous]
        public IActionResult All(string searchStringBreed, string searchStringName)
        {

            List<DogAllViewModel> dogs = service.GetDogs(searchStringBreed, searchStringName)
                .Select(dogFromDb=> new DogAllViewModel
                {
                    Id=dogFromDb.Id,
                    Name = dogFromDb.Name,
                    Age= dogFromDb.Age,
                    Breed=dogFromDb.Breed,
                    Picture=dogFromDb.Picture
                    
                }).ToList();
          
            return this.View(dogs);

        }

        public IActionResult Details(int id)
        {
            Dog item = service.GetDogById(id);

            if (item == null)
            {
                return NotFound();
            }
            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture
            };
            return View(dog);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
