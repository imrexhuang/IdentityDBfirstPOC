using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace IdentityDBfirstPOC.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext() : base("name=IdentityDBEntities")
        {
        }
        public virtual DbSet<AccountBook> AccountBook { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<IdentityDBfirstPOC.Models.ViewModels.SpendingTrackerViewModel> SpendingTrackerViewModels { get; set; }


        // https://dotblogs.com.tw/wasichris/2015/01/24/148255
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage =
                          string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }   

    }
}