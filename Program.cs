// See https://aka.ms/new-console-template for more information
using HelloWorld;

Console.WriteLine("Please input the number:");
Console.WriteLine("01: Random Array");
Console.WriteLine("02: Peak in an Array");
Console.WriteLine("03: Binary Search");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
string choice = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

switch (choice)
{
    case "01":
        practice01.Run();
        break;
    case "02":
        practice02_FindPeakInArray.Run();
        break;
    case "03":
        BinarySearch.Run();
        break;
    default:
        Console.WriteLine("Invalid input!");
        break;
}