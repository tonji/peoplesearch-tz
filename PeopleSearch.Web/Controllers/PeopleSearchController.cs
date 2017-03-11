using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using People.Model.Config;
using People.Model.Db;
using People.Model.Service;
using PeopleSearch.Web.Models;

namespace PeopleSearch.Web.Controllers
{
    public class PeopleSearchController : Controller
    {
        private readonly IPersonService personService;
        private readonly IInterestService interestService;

        public PeopleSearchController(IPersonService personService, IInterestService interestService)
        {
            this.personService = personService;
            this.interestService = interestService;

        }
        // GET: PeopleSearch
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPersonList()
        {
            var people = personService.GetAll();
            var peopleList = people.Select(p => new PersonView() { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName,
                Age = p.Age, PhotoUrl = p.PhotoUrl, Interests = p.Interests.Select(i => i.Description).ToList()});
            return new JsonResult { Data = peopleList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetAllInterests()
        {
            var interests = interestService.GetAll();
            var interestList = interests.Select(i => new InterestView() {Id = i.Id, Description = i.Description});
            return new JsonResult {Data = interestList, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        [HttpPost]
        public StatusCodeResult AddPerson(Person person)
        {
            List<Interest> personInterests = new List<Interest>();
            foreach (var interest in person.Interests)
            {
                Interest personInterest = interestService.Get(interest.Id);
                if (null != personInterest)
                {
                    personInterests.Add(personInterest);
                }
            }
            person.Interests = personInterests;
            personService.Add(person);
            return new StatusCodeResult(HttpStatusCode.Created, new HttpRequestMessage());
        }
    }
}