using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using STGenetics.Models;

namespace STGenetics.Controllers
{
    [Authorize] 
    public class AnimalsController : ApiController
    {
        private STGeneticsEntities db = new STGeneticsEntities();

        // GET: api/Animals
        public List<RespAnimal> GetAnimal()
        {
            List<Animal> listAnimal= db.Animal.ToList();
            List<RespAnimal> listRespAnimal = new List<RespAnimal>();
            foreach (Animal animal in listAnimal) {
                RespAnimal resp = new RespAnimal();
                resp.AnimalId = animal.AnimalId;
                resp.Breed = animal.Breed.Name;
                resp.Sex = animal.Sex.Name;
                resp.Name = animal.Name;
                resp.Status = animal.Status.Name;
                resp.Birthdate = animal.Birthdate;
                resp.Price = animal.Price;
                listRespAnimal.Add(resp);
            }

            return listRespAnimal;
        }        

        // GET: api/Animals/5
        [ResponseType(typeof(RespAnimal))]
        public async Task<IHttpActionResult> GetAnimal(int id)
        {
            Animal animal = await db.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            RespAnimal resp = new RespAnimal();
            resp.AnimalId = animal.AnimalId;
            resp.Breed = animal.Breed.Name;
            resp.Sex = animal.Sex.Name;
            resp.Name = animal.Name;
            resp.Status = animal.Status.Name;
            resp.Birthdate = animal.Birthdate;
            resp.Price = animal.Price;               

            return Ok(resp);
        }

        // PUT: api/Animals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnimal(int id, ReqAnimalToUpdate reqAnimalToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reqAnimalToUpdate.AnimalId)
            {
                return BadRequest();
            }

            Animal animal = db.Animal.Find(reqAnimalToUpdate.AnimalId);            
            animal.Name = reqAnimalToUpdate.Name;
            animal.BreedId = reqAnimalToUpdate.BreedId;
            animal.StatusId = reqAnimalToUpdate.StatusId;
            animal.SexId = reqAnimalToUpdate.SexId;
            animal.Birthdate = reqAnimalToUpdate.Birthdate;
            animal.Price = reqAnimalToUpdate.Price;
            //db.Entry(animal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Animals
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> PostAnimal(ReqAnimal reqAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Animal animal = new Animal();
            animal.Name = reqAnimal.Name;
            animal.BreedId = reqAnimal.BreedId;
            animal.StatusId = reqAnimal.StatusId;
            animal.SexId = reqAnimal.SexId;
            animal.Birthdate = reqAnimal.Birthdate;
            animal.Price = reqAnimal.Price;

            db.Animal.Add(animal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = animal.AnimalId }, animal);
        }

        // DELETE: api/Animals/5
        [ResponseType(typeof(ReqAnimalToDelete))]
        public async Task<IHttpActionResult> DeleteAnimal(ReqAnimalToDelete reqAnimalToDelete)
        {
            Animal animal = await db.Animal.FindAsync(reqAnimalToDelete.AnimalId);
            if (animal == null)
            {
                return NotFound();
            }

            try
            {
                db.Animal.Remove(animal);
                await db.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                return Ok(false);
            }
        }



        // POST: api/Animals
        [ResponseType(typeof(ReqAnimalFilter))]
        public List<RespAnimal> PostFilter(ReqAnimalFilter reqAnimalfilter, bool filter)
        {

            //List<Animal> listAnimal = db.Animal.ToList();

            List<Animal> listAnimal = new List<Animal>();

            if (reqAnimalfilter.AnimalId > 0)
            {
                listAnimal = db.Animal.Where(x => x.AnimalId == reqAnimalfilter.AnimalId).OrderByDescending(x => x.Name).ToList();
            }
            else if (reqAnimalfilter.Name != string.Empty)
            {
                listAnimal = db.Animal.Where(x => x.Name.Contains(reqAnimalfilter.Name) ).OrderByDescending(x => x.Name).ToList();
            }
            else if (reqAnimalfilter.SexId > 0)
            {
                listAnimal = db.Animal.Where(x => x.SexId == reqAnimalfilter.SexId).OrderByDescending(x => x.Name).ToList();
            }
            else if (reqAnimalfilter.StatusId > 0)
            {
                listAnimal = db.Animal.Where(x => x.StatusId == reqAnimalfilter.StatusId).OrderByDescending(x => x.Name).ToList();
            }
            else
            {
                listAnimal = db.Animal.Where(x => x.AnimalId == reqAnimalfilter.AnimalId).OrderByDescending(x => x.Name).ToList();
            }

            List<RespAnimal> listRespAnimal = new List<RespAnimal>();
            foreach (Animal animal in listAnimal)
            {
                RespAnimal resp = new RespAnimal();
                resp.AnimalId = animal.AnimalId;
                resp.Breed = animal.Breed.Name;
                resp.Sex = animal.Sex.Name;
                resp.Name = animal.Name;
                resp.Status = animal.Status.Name;
                resp.Birthdate = animal.Birthdate;
                resp.Price = animal.Price;
                listRespAnimal.Add(resp);
            }

            return listRespAnimal;
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimalExists(int id)
        {
            return db.Animal.Count(e => e.AnimalId == id) > 0;
        }
    }
}