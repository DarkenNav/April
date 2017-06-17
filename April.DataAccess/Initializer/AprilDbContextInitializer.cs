using April.DataAccess.Context;
using April.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace April.DataAccess.Initializer
{
    public class AprilDbContextInitializer
        : DropCreateDatabaseAlways<AprilDbContext>
       // : CreateDatabaseIfNotExists<AprilDbContext>
    {
        Random rand;
        AprilDbContext context;

        protected override void Seed(AprilDbContext context)
        {
            this.context = context;
            rand = new Random();

            AddUsers();
        }

        private void AddUsers()
        {
            context.User.AddRange(new List<User> {
                new User { Name = "Иванов Иван"},
                new User { Name = "Чугунков Петр"},
                new User { Name = "Бахмутов Альберт"},
                new User { Name = "Сидельников Василий"}
            });

            context.SaveChanges();
        }

    }
}
