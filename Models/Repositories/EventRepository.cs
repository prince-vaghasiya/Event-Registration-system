

namespace EventRegistrationSystem.Models.Repositories
{
    public class EventRepository
    {
        private readonly AppDbContext context;

        public EventRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Event Add(Event event1) 
        {
            context.Events.Add(event1);
            context.SaveChanges();
            return event1;
        }
        public Event Delete(int id)
        {
            Event event1 = context.Events.Find(id)!;
            if (event1 != null)
            {
                context.Events.Remove(event1);
                context.SaveChanges();
            }
            return event1!;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return context.Events;
        }
        public IEnumerable<Event> GetOngoingEvents()
        {
            return context.Events.Where(x=> x.DeadLine >= DateTime.Now);
        }
        public IEnumerable<Event> CompletedEvents()
        {
            return context.Events.Where(x=> x.DeadLine < DateTime.Now);
        }
        public IEnumerable<Event> GetEventsByOrgnizer(int id)
        {
            return context.Events.Where(e => e.OrganizerUserID == id);
        }

        public Event GetEvent(int id)
        {
            return context.Events.FirstOrDefault(x => x.Id == id);
        }

        public Event Update(Event event1)
        {
            var UUser = context.Events.Attach(event1);
            UUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return event1;
        }
    }
}
