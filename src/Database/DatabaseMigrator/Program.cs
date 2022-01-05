using DbUp;
using DbUp.Helpers;
using System;
using System.IO;

namespace DatabaseMigrator
{
  class Program
  {
    private const string PreSubdir = "PreScripts";
    private const string MigrationsSubdir = "Migrations";
    private const string SeedsSubdir = "Seeds";

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

      var preUpgrader = DeployChanges
          .To
          .SqlDatabase(connectionString)
          .WithScriptsFromFileSystem($"{scripsPath}/{PreSubdir}")
          .JournalTo(new NullJournal())
          .Build();

      var preResult = preUpgrader.PerformUpgrade();

      if (!preResult.Successful)
      {
        Console.WriteLine("Pre scripts unsuccessful");
        return;
      }

      var upgrader = DeployChanges
        .To
        .SqlDatabase(connectionString)
        .WithScriptsFromFileSystem($"{scripsPath}/{MigrationsSubdir}")
        .JournalToSqlTable("app", "MigrationsJournal")
        .Build();

      var result = upgrader.PerformUpgrade();

      if (!result.Successful)
      {
        Console.WriteLine("Migration did not succeed");
        return;
      }

      Console.WriteLine("Migration succeded");

      var seeder = DeployChanges
        .To
        .SqlDatabase(connectionString)
        .WithScriptsFromFileSystem($"{scripsPath}/{SeedsSubdir}")
        .JournalTo(new NullJournal())
        .Build();

      var seedResult = seeder.PerformUpgrade();

      if (!seedResult.Successful)
      {
        Console.WriteLine("Seeding did not succeed");
        return;
      }

      Console.WriteLine("Seeding succeded");
    }
  }
}
