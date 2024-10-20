using EventRegistrationSystem.Models;
using EventRegistrationSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace EventRegistrationSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EventRepository eventRepository;
        private readonly UserRepository userRepository;
        private readonly FormFieldRepository formFieldRepository;
        private readonly ResponseRepository responseRepository ;

        public HomeController(FormFieldRepository formFieldRepository,ILogger<HomeController> logger,EventRepository eventRepository, UserRepository userRepository,ResponseRepository responseRepository)
        {
            _logger = logger;
            this.eventRepository = eventRepository;
            this.userRepository = userRepository;
            this.formFieldRepository = formFieldRepository;
            this.responseRepository = responseRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Event> events = eventRepository.GetOngoingEvents();
            return View(events);
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Event eventd)
        {
            eventd.OrganizerUserID = userRepository.GetUserByUsername(User.Identity.Name).Id;
            DateTime today = DateTime.Now;
            if(eventd.DeadLine < today)
            {
                ViewBag.msg = "Please enter future date.";
                ViewBag.msgType = "danger";
                return View();
            }
            
            var newevent= eventRepository.Add(eventd);
            return RedirectToAction("Details", new { id = newevent.Id }); 
        }
        public IActionResult Edit(int id)
        {

            Event eventdata = eventRepository.GetEvent(id);
            return View(eventdata);
        }
        [HttpPost]
        public IActionResult Edit(Event eventd)
        {
            DateTime today = DateTime.Now;
            if (eventd.DeadLine < today)
            {
                ViewBag.msg = "Please enter future date.";
                ViewBag.msgType = "danger";
                return View();
            }
            var newevent = eventRepository.Update(eventd);
            return RedirectToAction("Details", new { id = newevent.Id });
        }
        public IActionResult UserEvents()
        {
            IEnumerable<Event> events = eventRepository.GetEventsByOrgnizer(userRepository.GetUserByUsername(User.Identity.Name).Id);
            return View("/Views/Home/Index.cshtml",events);
        }
        public IActionResult CompletedEvents()
        {
            IEnumerable<Event> events = eventRepository.CompletedEvents();
            return View("/Views/Home/Index.cshtml",events);
        }

        public IActionResult Details(int id)
        {
            
            Event eventdata = eventRepository.GetEvent(id);
            DateTime today = DateTime.Now;
            
            eventdata.Organizer = userRepository.GetUser(eventdata.OrganizerUserID);
            eventdata.FormFields = formFieldRepository.GetFormFieldsByEventId(id);
            var user = userRepository.GetUserByUsername(User.Identity.Name);
            List<Response> responses = responseRepository.GetResponseByEvent(id) ?? new List<Response>();
            List<User> users = new List<User>();
            foreach (var item in responses)
            {
                User user1 = userRepository.GetUser(item.UserID);
                if (users.Contains(user1) == false)
                {
                    users.Add(user1);
                }
            }
            ViewData["participants"]= users.Count;
            ViewData["responses"]= responseRepository.GetResponseByUserAndEvent(user.Id, id).IsNullOrEmpty();
            ViewData["submit"]= true;
            ViewData["user"]= user.Id;

            if (eventdata.DeadLine < today)
            {
                ViewData["submit"] = false;
            }

            return View(eventdata);
        }

        

        [HttpPost]
        public IActionResult RegisterEvent([FromForm] Dictionary<string,string> response)
        {
            var eventId = response["eventId"];
            var formFields = formFieldRepository.GetFormFieldsByEventId(int.Parse(eventId));
            var user = userRepository.GetUserByUsername(User.Identity.Name);
            foreach (var item in formFields)
            {
                string ans = response[item.Id.ToString()];
                if (ans != null)
                {
                    Response res = new Response();
                    res.FieldID = item.Id;
                    res.UserID = user.Id;
                    res.ResponseValue = ans;
                    res.EventId = int.Parse(eventId);
                    responseRepository.Add(res);
                }
            }
            return Redirect($"/Event/ResponseDetails/{eventId}?userId={user.Id}");
        }



        [HttpPost]
        public IActionResult AddFormField(int eventId, string label, string fieldtype, string isRequired)
        {
            FormField formField = new FormField();
            formField.FieldLabel = label;
            formField.FieldType = fieldtype;
            formField.EventID = eventId;
            formField.IsRequired = isRequired == "required";
            formFieldRepository.Add(formField);

            return RedirectToAction("details", "home", new { id = eventId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}