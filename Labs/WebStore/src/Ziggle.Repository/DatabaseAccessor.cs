using Ziggle.ProductDatabase;

namespace Ziggle.Repository
{
    public class DatabaseAccessor
    {
        static DatabaseAccessor()
        {
            Instance = new ProductDbContext();
        }

        public static ProductDbContext Instance { get; private set; }
    }
}