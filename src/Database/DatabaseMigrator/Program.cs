using DbUp;
using System;
using System.IO;

namespace DatabaseMigrator
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length != 2)
      {
        Console.WriteLine("Invalid arguments. Execution: dotnet DatabaseMigrator.dll [connectionString] [pathToScripts].");

        Console.WriteLine("Migration stopped");

        return;
      }

      var connectionString = args[0];

      var scripsPath = args[1];

      if (!Directory.Exists(scripsPath))
      {
        Console.WriteLine("Invalid path");
        return;
      }

      var upgrader = DeployChanges
        .To
        .SqlDatabase(connectionString)
        .WithScriptsFromFileSystem(scripsPath)
        .JournalToSqlTable("app", "MigrationsJournal")
        .Build();

      var result = upgrader.PerformUpgrade();

      if (!result.Successful)
      {
        Console.WriteLine("Migration did not succeed");
        return;
      }

      Console.WriteLine("Migration succeded");
    }
  }
}
