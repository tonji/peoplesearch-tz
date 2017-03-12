using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using People.Model.Db;
using People.Model.Service;
using PeopleSearch.Web.Models;

namespace PeopleSearch.Web.Controllers
{
    public class PeopleSearchController : Controller
    {
        private readonly IPersonService personService;
        private readonly IInterestService interestService;
        private static string PHOTO_PATH = "../Photos//";

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
            //convert to list of view person objects
            var peopleList = people.Select(p => new PersonView()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                PhotoUrl = p.PhotoUrl,
                Interests = p.Interests.Select(i => i.Description).ToList()
            });
            return new JsonResult { Data = peopleList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetAllInterests()
        {
            var interests = interestService.GetAll();
            //convert to list of view interest objects
            var interestList = interests.Select(i => new InterestView() { Id = i.Id, Description = i.Description });
            return new JsonResult { Data = interestList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public StatusCodeResult AddPerson(Person person, string interests, HttpPostedFileBase file)
        {

            //attempt to save the file
            try
            {
                if (null != file)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string path = Server.MapPath(PHOTO_PATH);
                    file.SaveAs(path + fileName);
                    person.PhotoUrl = fileName;
                }
            }
            catch (IOException exc)
            {
                Console.WriteLine(exc.StackTrace);
            }

            List<Interest> interestList = new JavaScriptSerializer().Deserialize<List<Interest>>(interests);

            foreach (var interest in interestList)
            {
                Interest personInterest = interestService.Get(interest.Id);
                if (null != personInterest)
                {
                    person.Interests.Add(personInterest);
                }
            }

            personService.Add(person);
            return new StatusCodeResult(HttpStatusCode.Created, new HttpRequestMessage());
        }
    }
}