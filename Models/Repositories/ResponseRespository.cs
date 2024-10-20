


namespace EventRegistrationSystem.Models.Repositories
{
    public class ResponseRepository
    {
        private readonly AppDbContext context;

        public ResponseRepository(AppDbContext context)
        {
            this.context = context;
        }

         public Response Add(Response response1)
        {
            context.Responses.Add(response1);
            context.SaveChanges();
            return response1;
        }
        public Response Delete(int id)
        {
            Response response1 = context.Responses.Find(id)!;
            if (response1 != null)
            {
                context.Responses.Remove(response1);
                context.SaveChanges();
            }
            return response1!;
        }
        public void deleteByEvent(int id) {
            var responses = context.Responses.Where(x => x.EventId == id);
            foreach (var item in responses)
            {
                Delete(item.Id);
            }
        }

        public IEnumerable<Response> GetAllResponses()
        {
            return context.Responses;
        }

        public Response GetResponse(int id)
        {
            return context.Responses.FirstOrDefault(x => x.Id == id);
        }
        public List<Response> GetResponseByUserAndEvent(int userId,int eventId)
        {
            return context.Responses.Where(x => x.UserID == userId && x.EventId == eventId).ToList<Response>();
        }
        public List<Response> GetResponseByEvent( int eventId)
        {
            return context.Responses.Where(x => x.EventId == eventId).ToList<Response>();
        }
        public List<Response> GetResponseByUser( int id)
        {
            return context.Responses.Where(x => x.UserID == id).ToList<Response>();
        }
        public Response Update(Response response1)
        {
            var UUser = context.Responses.Attach(response1);
            UUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return response1;
        }
    }
}
