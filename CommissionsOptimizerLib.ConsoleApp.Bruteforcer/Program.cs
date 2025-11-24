using CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Services;
using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Models;
using CommissionsOptimizerLib.Data.GoogleSheets.Services;
using System.Diagnostics;

string? testCommissionsDataUrl;
string? testTrekkersDataUrl;

do
{
    Console.WriteLine("Input test commissions endpoint, leave empty for default");
    testCommissionsDataUrl = Console.ReadLine();
} while (testCommissionsDataUrl == null);

if (string.IsNullOrEmpty(testCommissionsDataUrl))
{
    testCommissionsDataUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQRBUi0tmdK0PmkVXIwcNowWxcz4oTijMc12YF5mdbeb9Sw2Hi8jfJURb2Pe9wjAvamK2I6eLS50yMb/pub?gid=1732131747&single=true&output=csv";
}

Console.WriteLine();

do
{
    Console.WriteLine("Input test trekkers endpoint, leave empty for default");
    testTrekkersDataUrl = Console.ReadLine();
} while (testTrekkersDataUrl == null);

if (string.IsNullOrEmpty(testTrekkersDataUrl))
{
    testTrekkersDataUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQRBUi0tmdK0PmkVXIwcNowWxcz4oTijMc12YF5mdbeb9Sw2Hi8jfJURb2Pe9wjAvamK2I6eLS50yMb/pub?gid=1400848852&single=true&output=csv";
}

Console.WriteLine();

GoogleSheetsDataProvider dataProvider;

try
{
    Console.WriteLine("Loading data...");
    dataProvider = await GoogleSheetsDataProvider.CreateAsync(testCommissionsDataUrl, testTrekkersDataUrl, displayCsvToConsole: true);
}
catch (Exception e)
{
    Console.WriteLine("Error loading google sheets data provider:");
    Console.WriteLine(e.Message);
    return;
}

// intercept input after awaiting data loading
while (Console.KeyAvailable)
{
    Console.ReadKey(intercept: true); // intercept=true avoids echoing the key
}

Console.WriteLine();
Console.WriteLine("Trekkers found:");

foreach (var trekker in dataProvider.GetTrekkersData())
{
    Console.WriteLine(trekker.Name);
}

Console.WriteLine();

// select up to 4 materials then the brute forcer should run

Console.WriteLine("[1] Trekker Mat - Grotesque");
Console.WriteLine("[2] Trekker Mat - Duloos");
Console.WriteLine("[3] Trekker Mat - Lampflower");
Console.WriteLine("[4] Disc Mat - Grotesque");
Console.WriteLine("[5] Disc Mat - Duloos");
Console.WriteLine("[6] Disc Mat - Lampflower");
//Console.WriteLine("[4] Skill Mat - Rhythm");
//Console.WriteLine("[5] Skill Mat - Shooter");
//Console.WriteLine("[6] Skill Mat - Kung Fu");

var materialsSelect = new List<RewardType>();

do
{
    Console.WriteLine("Select 4 materials (i.e. \"1 2 3 4\") (type \"exit\" to exit)");

    materialsSelect.Clear();
    var materialsOption = Console.ReadLine();

    if (string.IsNullOrEmpty(materialsOption))
    {
        continue; // Restart
    }

    if (materialsOption.Equals("exit"))
    {
        return; // Prematurely exit program
    }

    try
    {
        var optionSplit = materialsOption.Split(" ");

        if (optionSplit.Length != 4)
        {
            Console.WriteLine("Must choose 4 options!");
            continue; // Do agane
        }

        if (optionSplit.Length != optionSplit.Distinct().Count())
        {
            Console.WriteLine($"Cannot choose duplicate reward types!");
            continue; // Do agane
        }

        for (int i = 0; i < 4; i++)
        {
            if (!int.TryParse(optionSplit[i], out var option))
            {
                Console.WriteLine($"Error parsing option {i+1}");
                break; // Do agane
            }

            var chosenRewardType = option switch
            {
                1 => RewardType.TrekkerMats_Grotesque,
                2 => RewardType.TrekkerMats_Duloos,
                3 => RewardType.TrekkerMats_Lampflower,
                4 => RewardType.DiscMats_Grotesque,
                5 => RewardType.DiscMats_Duloos,
                6 => RewardType.DiscMats_Lampflower,
                _ => throw new NotImplementedException($"Not implemented for this option: {option}"),
            };

            materialsSelect.Add(chosenRewardType);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
} while (materialsSelect.Count != 4);

var materialsPriority = new List<int>();

do
{
    Console.WriteLine($"Assign a priority value (1-6, larger = higher) for each item:\n{string.Join(" | ", materialsSelect)}");

    materialsPriority.Clear();
    var priorityOption = Console.ReadLine();

    if (string.IsNullOrEmpty(priorityOption))
    {
        continue; // Restart
    }

    if (priorityOption.Equals("exit"))
    {
        return; // Prematurely exit program
    }

    try
    {
        var prioritySplit = priorityOption.Split(" ");

        if (prioritySplit.Length != materialsSelect.Count)
        {
            Console.WriteLine($"Must choose {materialsSelect.Count} options!");
            continue; // Do agane
        }

        for (int i = 0; i < materialsSelect.Count; i++)
        {
            if (!int.TryParse(prioritySplit[i], out var priority))
            {
                Console.WriteLine($"Error parsing option {i + 1}");
                break; // Do agane
            }

            if (priority < 1 || priority > 6)
            {
                Console.WriteLine($"Option {i + 1} cannot have priority {priority}!");
                break; // Do agane
            }

            materialsPriority.Add(priority);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
} while (materialsPriority.Count != materialsSelect.Count);

Console.WriteLine("Calculating best commissions for these items:");
Console.WriteLine(string.Join(" | ", materialsSelect));

// Assume I own all trekkers and they are max level
var allTrekkers = dataProvider.GetTrekkersData();
List<PlayerTrekkerData> playerTrekkerData =
[
    .. allTrekkers.Select(x => new PlayerTrekkerData()
    {
        Exists = true,
        Level = 90,
        Trekker = x
    })
];

OptimizationResults results;
var optionsBuilder = new OptimizerOptions.Builder()
    .SetTyrantLevel(40)
    .SetHyperFocus(false);

for (int i = 0; i < materialsSelect.Count; i++)
{
    optionsBuilder.AddOrSetMaterialPriority(materialsSelect[i], materialsPriority[i]);
}

var options = optionsBuilder.Build();
var calculator = new BruteForceCalculator(dataProvider);

Stopwatch sw = Stopwatch.StartNew();

try
{
    results = calculator.CalculateCommissions(playerTrekkerData, options);
}
catch (Exception e)
{
    sw.Stop();
    Console.WriteLine(e.Message);
    Console.WriteLine($"Time elapsed until error discovered: {sw.Elapsed}");
    return;
}

sw.Stop();

Console.WriteLine("======= RESULTS =======");

// prepare a text file named the datetime, then fill it with these info:

string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
string logFile = $"log_{timestamp}.txt";

try
{
    using var writer = new StreamWriter(logFile, append: true);

    Log($"The calculator has done {calculator.ChecksDone} checks.", writer);
    Log($"Duration: {sw.Elapsed}", writer);
    Log("To get the most efficient output of these materials:", writer);
    Log($"{string.Join(" | ", options.MaterialsSelect.Select(x => x.Material))}", writer);
    Log("Here are the top 3 commissions setups you need to run.", writer);

    int suggestionCount = 1;
    foreach (var suggestion in results.Top3Results)
    {
        Log($"""
        [Suggestion {suggestionCount}]
        {suggestion.Groups[0].Commission.Name}
        {string.Join(" | ", suggestion.Groups[0].TrekkersToSend.Select(x => x.Name))}
        Vigor Efficiency: {suggestion.Groups[0].AverageRewards}
        ------
        {suggestion.Groups[1].Commission.Name}
        {string.Join(" | ", suggestion.Groups[1].TrekkersToSend.Select(x => x.Name))}
        Vigor Efficiency: {suggestion.Groups[1].AverageRewards}
        ------
        {suggestion.Groups[2].Commission.Name}
        {string.Join(" | ", suggestion.Groups[2].TrekkersToSend.Select(x => x.Name))}
        Vigor Efficiency: {suggestion.Groups[2].AverageRewards}
        ------
        {suggestion.Groups[3].Commission.Name}
        {string.Join(" | ", suggestion.Groups[3].TrekkersToSend.Select(x => x.Name))}
        Vigor Efficiency: {suggestion.Groups[3].AverageRewards}
        ------
        """, writer);
        suggestionCount++;
    }

    writer.Flush();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

static void Log(string message, StreamWriter writer)
{
    Console.WriteLine(message);
    writer.WriteLine(message);
}
