using System;

namespace MongoDB_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var repo = new MongoDBRepository("mongodb://localhost:27017");
            repo.ListDBs();
            repo.CreateACollection("Equipment");
        }
    }
}
