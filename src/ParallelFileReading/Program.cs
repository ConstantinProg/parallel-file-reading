using ParallelFileReading;
using System.Diagnostics;

const string TestFilesDirectoryName = "TestFiles";
string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), TestFilesDirectoryName);
var fileCollection = Directory.GetFiles(directoryPath, "*.txt", SearchOption.AllDirectories);

Stopwatch stopwatch = new Stopwatch();

// Task 1: Read 3 files in parallel and calculate the number of spaces in them
const int TOTAL_FILES_TO_READ = 3;
stopwatch.Start();

var tasks = new List<Task<int>>(TOTAL_FILES_TO_READ);
foreach (var filePath  in fileCollection.Take(TOTAL_FILES_TO_READ))
    tasks.Add(ParallelFileReader.CountSpacesInFileAsync(filePath));

int[] results = await Task.WhenAll(tasks);
int totalSpaces = results.Sum();
stopwatch.Stop();

Console.WriteLine($"Total spaces in {TOTAL_FILES_TO_READ} files: {totalSpaces}");
Console.WriteLine($"Time taken for Task 1: {stopwatch.ElapsedMilliseconds} ms");

stopwatch.Restart();

// Task 2: Read all files in the directory and count spaces 
int totalDirectorySpaces = await ParallelFileReader.CountSpacesInDirectoryAsync(directoryPath);
stopwatch.Stop();
Console.WriteLine($"Total spaces in files from folder: {totalDirectorySpaces}");
Console.WriteLine($"Time taken for Task 2: {stopwatch.ElapsedMilliseconds} ms");
