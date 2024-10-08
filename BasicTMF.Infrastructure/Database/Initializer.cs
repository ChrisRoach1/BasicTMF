using DbUp;

namespace BasicTMF.Infrastructure.Database
{
    public class Initializer
    {
        public static void InitilizeDatabase(string connectionString)
        {
            EnsureDatabase.For.MySqlDatabase(connectionString);


            var upgrader = DeployChanges.To.MySqlDatabase(connectionString)
                .WithScriptsAndCodeEmbeddedInAssembly(typeof(Initializer).Assembly).WithTransaction().LogToConsole().Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception("DB Migration failed");
            }
        }

    }
}
