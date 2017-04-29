namespace GuitarAPI.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuitarAPI.DAL.ApplicationDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuitarAPI.DAL.ApplicationDBContext context)
        {
            context.Guitars.AddOrUpdate(
            new Guitar { ID = 1, Name = "Strat", Make = "Fender", IsNew = true, Stock = 3 },
            new Guitar { ID = 2, Name = "Tele", Make = "Fender", IsNew = true, Stock = 2 },
            new Guitar { ID = 3, Name = "SG", Make = "Gibson", IsNew = false, Stock = 1 },
            new Guitar { ID = 4, Name = "Mustang", Make = "Fender", IsNew = true, Stock = 5 },
            new Guitar { ID = 5, Name = "HummingBird", Make = "Gibson", IsNew = false, Stock = 3 }

                );
        }
    }
}
