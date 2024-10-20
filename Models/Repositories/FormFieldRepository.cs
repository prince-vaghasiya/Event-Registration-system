


namespace EventRegistrationSystem.Models.Repositories
{
    public class FormFieldRepository
    {
        private readonly AppDbContext context;

        public FormFieldRepository(AppDbContext context)
        {
            this.context = context;
        }

        public FormField Add(FormField formField1)
        {
            context.FormFields.Add(formField1);
            context.SaveChanges();
            return formField1;
        }
        public FormField Delete(int id)
        {
            FormField formField1 = context.FormFields.Find(id)!;
            if (formField1 != null)
            {
                context.FormFields.Remove(formField1);
                context.SaveChanges();
            }
            return formField1!;
        }

        public IEnumerable<FormField> GetAllFormFields()
        {
            return context.FormFields;
        }
        public List<FormField> GetFormFieldsByEventId(int id)
        {
            return context.FormFields.Where(t=> t.EventID ==id).ToList<FormField>();
        }

        public FormField GetFormField(int id)
        {
            return context.FormFields.FirstOrDefault(x => x.Id == id);
        }

         public FormField Update(FormField formField1)
        {
            var UUser = context.FormFields.Attach(formField1);
            UUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return formField1;
        }
    }
}
