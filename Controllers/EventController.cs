using EventRegistrationSystem.Models;
using EventRegistrationSystem.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationSystem.Controllers
{
    public class EventController : Controller
    {
        // GET: EventController
        private readonly EventRepository eventRepository;
        private readonly UserRepository userRepository;
        private readonly FormFieldRepository formFieldRepository;
        private readonly ResponseRepository responseRepository;
        public EventController(FormFieldRepository formFieldRepository, EventRepository eventRepository, UserRepository userRepository, ResponseRepository responseRepository)
        {
            this.eventRepository = eventRepository;
            this.userRepository = userRepository;
            this.formFieldRepository = formFieldRepository;
            this.responseRepository = responseRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult delete(int id)
        {
            eventRepository.Delete(id);
            responseRepository.deleteByEvent(id);
            return Redirect("/");
        }
        // GET: EventController/Details/5
        public ActionResult Responses(int id)
        {
            List<Response> responses = responseRepository.GetResponseByEvent(id) ?? new List<Response>();
            //Dictionary<User, List<Response>> dic = new Dictionary<User, List<Response>>();
            List<User> users = new List<User>();
            foreach (var item in responses)
            {
                User user = userRepository.GetUser(item.UserID);
                if(users.Contains(user) == false)
                {
                    users.Add(user);
                }
            }
            //ViewBag.Users = users;
            //ViewBag.eventId = id;
            ViewData["users"] = users;
            ViewData["eventId"] = id;
            return View();
        }
        
        [HttpGet]
        public IActionResult ResponseDetails(int id,int userId)
        {
            var responses = responseRepository.GetResponseByUserAndEvent(userId, id);
            foreach (var response in responses)
            {
                var form = formFieldRepository.GetFormField(response.FieldID);
                response.Field = form;
            }
            //ViewBag.responses = responses;
            ViewData["responses"] = responses;
            return View();
        }
    }
}
