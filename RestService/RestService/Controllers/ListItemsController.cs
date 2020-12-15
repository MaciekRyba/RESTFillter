using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestService.Models;

namespace RestService.Controllers
{
    public class ListItemsController : ApiController
    {
        private static List<Persons> lstPersons { get; set; } = new List<Persons>() { 
            new Persons(){id = 1,name = "Maciej", surname = "Rybarczuk",year=1990, city = "Siemiatycze", pet = new Pets{name = "Rocky", animal = "Pies", year = 2015 }, car = new Cars{ theCarBrand = "Audi", year = 2000, color = "Czerwony", mileage = 10000} },
            new Persons(){id = 2,name = "Ala", surname = "Kot",city="Bialystok",year=1997, pet= new Pets{name = "Alfa", animal = "Kot", year = 2018 }, car = new Cars{ theCarBrand = "BMW", year = 2010, color = "Czarny", mileage = 10000}},
            new Persons(){id = 3,name = "Ala", surname = "Nowak",city="Krakow",year=1997, pet= new Pets{name = "Max", animal = "Papuga", year = 2015 }, car = new Cars{ theCarBrand = "Fiat", year = 1998, color = "Czerwony", mileage = 10000}}
        };
        // GET api/<controller>
        public IEnumerable<Persons> Get()
        {
            return lstPersons;
        }

        public HttpResponseMessage Get(int id)
        {
            var item = lstPersons.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("api/Persons/{name?}/{city?}/{year?}/{contains?}/{lettersize?}")]
        public HttpResponseMessage GetByName(string name = null, string city = null, int ? year = null, bool contains = false, bool lettersize  = false)
        {
            var item = lstPersons.AsEnumerable();

            if(year!=null)
                item= item.Where(x => x.year == year);

            if (!string.IsNullOrEmpty(name))
            {
                if(contains)
                     item = item.Where(x => (lettersize ? x.name.ToLower().Contains(name.ToLower()) : x.name.Contains(name)));
                else
                    item = item.Where(x => (lettersize ? x.name.ToLower() == name.ToLower() : x.name == name));
            }

            if (!string.IsNullOrEmpty(city))
            {
                if (contains)
                    item = item.Where(x => (lettersize ? x.city.ToLower().Contains(city.ToLower()) : x.city.Contains(city)));
                else
                    item = item.Where(x => (lettersize ? x.city.ToLower() == city.ToLower() : x.city == city));
            }

            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Persons model)
        {
            if (string.IsNullOrEmpty(model?.name) || string.IsNullOrEmpty(model?.surname))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var maxId = 0;

            if (lstPersons.Count > 0)
            {
                maxId = lstPersons.Max(x => x.id);
            }
            model.id = maxId + 1;
            lstPersons.Add(model);
            return Request.CreateResponse(HttpStatusCode.Created, model);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody] Persons model)
        {
            if (string.IsNullOrEmpty(model?.name) || string.IsNullOrEmpty(model?.surname))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var item = lstPersons.FirstOrDefault(x => x.id == id);

            if (item != null)
            {
                // Update *all* of the item's properties
                item.name = model.name;
                item.surname = model.surname;
                item.city = model.city;
                item.year = model.year;

                item.pet.animal = model.pet.animal;
                item.pet.name = model.pet.name;
                item.pet.year = model.pet.year;

                item.car.theCarBrand = model.car.theCarBrand;
                item.car.color = model.car.color;
                item.car.year = model.car.year;
                item.car.mileage = model.car.mileage;

                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            var item = lstPersons.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                lstPersons.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}