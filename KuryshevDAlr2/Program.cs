using KuryshevDAlr2.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

var result = new Dictionary<string, Dictionary<string, Dictionary<int, Dictionary<int, long>>>>();


DisplaySortingTimeStatistic();
DisplaySortingTimeStatisticInSmallArray();

Console.WriteLine();
Console.WriteLine("Результаты:");

foreach (var sort in result)
{
    Console.WriteLine($"Сортировка: {sort.Key}");

    foreach (var arrayName in sort.Value)
    {
        Console.WriteLine($"\tМассив: {arrayName.Key}");

        foreach (var size in arrayName.Value)
        {
            Console.WriteLine($"\t\tРазмер: {size.Key}");

            var average = ((float)size.Value.Values.Sum()) / (float)size.Value.Values.Count;
            Console.WriteLine($"\t\tСреднее время выполнения: {average}");
            Console.WriteLine($"\t\tПопытка: {string.Join(", ", size.Value.Values)}");
        }
    }
}

Console.ReadLine();

void DisplaySortingTimeStatistic()
{
    Console.WriteLine("Сбор статистики, время работы алгоритмов сортировок при различных размерах массива");

    DisplaySortingTimeStatisticInRandomLengthArray(5, GetSortedArray, "Отсортированный массив");
    DisplaySortingTimeStatisticInRandomLengthArray(5, GetAlmostSortedArray, "Почти отсортированный массив");
    DisplaySortingTimeStatisticInRandomLengthArray(5, GetReverseSortedArray, "Отсортированный в обратном массив");
    DisplaySortingTimeStatisticInRandomLengthArray(5, GetRandomArray, "Рандомный массив");

    Console.WriteLine();
}

void DisplaySortingTimeStatisticInRandomLengthArray(int attept, Func<int, int[]> arrayGenerator, string arrayName)
{
    var random = new Random();
    var arrayLength = 0;

    for (int i = 0; i < attept; i++)
    {
        arrayLength += random.Next(1000, 5000);
        var array = arrayGenerator(arrayLength);

        DisplaySortingStatistic(array, arrayName + $" с длиной: {arrayLength}");
    }
}

void DisplaySortingTimeStatisticInSmallArray()
{
    Console.WriteLine("Сбор статистики, времени работы алгоритмов сортировки при малом размере массива");

    var sortedArray = GetSortedArray(100);
    var almostSortedArray = GetAlmostSortedArray(100);
    var reverseSortedArray = GetReverseSortedArray(100);
    var randomArray = GetRandomArray(100);

    DisplaySortingStatistic(sortedArray, "Отсортированный массив");
    DisplaySortingStatistic(almostSortedArray, "Почти отсортированный массив");
    DisplaySortingStatistic(reverseSortedArray, "Отсортированный в обратном массив");
    DisplaySortingStatistic(randomArray, "Рандомный массив");

    Console.WriteLine();
}

int[] GetSortedArray(int maxSize)
{
    List<int> list = new List<int>();

    for(int i=0; i< maxSize; i++)
        list.Add(i);

    return list.ToArray();
}

int[] GetAlmostSortedArray(int maxSize)
{
    List<int> list = new List<int>();
    var random = new Random();
    int maxSortedIndex = random.Next(maxSize-10, maxSize);

    for (int i = 0; i < maxSortedIndex; i++)
        list.Add(i);

    while (list.Count < maxSize)
        list.Add(random.Next());

    return list.ToArray();
}

int[] GetReverseSortedArray(int maxSize)
{
    var sortedArray = GetSortedArray(maxSize);
    return sortedArray
        .Reverse()
        .ToArray();
}

int[] GetRandomArray(int maxSize)
{
    List<int> list = new List<int>();
    var random = new Random();

    for (int i = 0; i < maxSize; i++)
        list.Add(random.Next());

    return list.ToArray();
}

void DisplaySortingStatistic(int[] array, string arrayName)
{
    Console.WriteLine($"\tСортировка массива: {arrayName}");

    for (int i=0; i<5; i++)
    {
        Console.WriteLine($"\t\tПопытка номер: {i + 1}");

        var newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.SelectionSort, "Сортировка выбором", arrayName, i + 1);

        newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.SwapSort, "Сортировка вставками", arrayName, i + 1);

        newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.BubbleSort, "Сортировка пузырьком", arrayName, i + 1);

        newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.MergeSort, "Сортировка слиянием", arrayName, i + 1);

        newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.QuickSort, "Быстрая сортировка", arrayName, i + 1);

        newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.ShellSort, "Сортировка Шелла", arrayName, i + 1);

        newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.HeapSort, "Пирамидальная сортировка", arrayName, i + 1);

        newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.TimSort, "TimSort", arrayName, i + 1);

        newArray = (int[])array.Clone();
        DisplaySortExecutionTime(newArray, NumSortingExtensions.IntroSort, "IntroSort", arrayName, i + 1);
    }
}

void DisplaySortExecutionTime(int[] array, Action<int[]> sort, string sortingName, string arrayName, int attept)
{
    var arrName = arrayName.Split(" с длиной: ").First();

    result.TryAdd(sortingName, new Dictionary<string, Dictionary<int, Dictionary<int, long>>>());
    result[sortingName].TryAdd(arrName, new Dictionary<int, Dictionary<int, long>>());
    result[sortingName][arrName].TryAdd(array.Length, new Dictionary<int, long>());

    var stopWatch = new Stopwatch();
    stopWatch.Start();
    sort(array);
    stopWatch.Stop();
    Console.WriteLine($"\t\t\tВремя выполнения сортировки {sortingName}: {stopWatch.ElapsedMilliseconds} мс");

    result[sortingName][arrName][array.Length].TryAdd(attept, stopWatch.ElapsedMilliseconds);
}