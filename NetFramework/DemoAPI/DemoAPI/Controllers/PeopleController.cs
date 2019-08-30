using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoAPI.Controllers
{
    
    /// <summary>
    /// This is where I give you all the information about my peeps.
    /// </summary>
    public class PeopleController : ApiController
    {
        List<Person> people = new List<Person>();

            public PeopleController()
        {
            people.Add(new Person { FirstName = "Tim", LastName = "Kowalski", ID = 1 });
            people.Add(new Person { FirstName = "Ola", LastName = "Kowalski", ID = 2 });
            people.Add(new Person { FirstName = "Szymon", LastName = "Kowalski", ID = 3 });

        }
        // GET: api/People
        public List<Person> Get()
        {
            return people;
        }

        // GET: api/People/5
        public Person Get(int id)
        {
            return people.Where(x => x.ID ==id).FirstOrDefault();
        }
        /// <summary>
        /// Gets a list of the forst names of users.
        /// </summary>
        /// <param name="userID">The unique indentifier</param>
        /// <param name="age">We want know how old they are</param>
        /// <returns>A list of firs names</returns>
        [Route("api/People/GetFirstName/{userID:int}/{age:int}")]
        [HttpGet]
        public List<string> GetFirstName(int userID, int age)
        {
            var outpot = new List<string>();

            foreach (var p in people)
            {
                outpot.Add(p.FirstName);
            }
            return outpot;
        }

        // POST: api/People
        public void Post(Person val)
        {
            people.Add(val);
        }

        // PUT: api/People/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {
        }
    }
}
