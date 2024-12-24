namespace ParallelFileReading;

public static class ParallelFileReader
{
    public static async Task<int> CountSpacesInFileAsync(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath);
        return content.Count(c => c == ' ');
    }

    public static async Task<int> CountSpacesInDirectoryAsync(string folderPath)
    {
        var files = Directory.GetFiles(folderPath);

        var tasks = files.Select(file =>
            CountSpacesInFileAsync(file)
        );

        int[] results = await Task.WhenAll(tasks);
        return results.Sum();
    }
}
