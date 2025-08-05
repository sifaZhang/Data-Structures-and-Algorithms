﻿// See https://aka.ms/new-console-template for more information
using HelloWorld;

Console.WriteLine("Please input the number:");
Console.WriteLine("01: Random Array");
Console.WriteLine("02: Peak in an Array");
Console.WriteLine("03: Liner Search");
Console.WriteLine("04: Binary Search");
Console.WriteLine("05: FlexiArray");
Console.WriteLine("06: RecursiveLinerSearch");
Console.WriteLine("07: RecursiveBinarySearch");

string choice = Console.ReadLine() ?? string.Empty;

switch (choice)
{
    case "01":
        RandomArray.Run();
        break;
    case "02":
        FindPeakInArray.Run();
        break;
    case "03":
        LinerSearch.Run();
        break;
    case "04":
        BinarySearch.Run();
        break;
    case "05":
        FlexiArray.Run();
        break;
    case "06":
        RecursiveLinerSearch.Run();
        break;
    case "07":
        RecursiveBinarySearch.Run();
        break;
    default:
        Console.WriteLine("Invalid input!");
        break;
}