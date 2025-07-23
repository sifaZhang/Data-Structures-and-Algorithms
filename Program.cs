// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;

Console.WriteLine("Please input the number:");
Console.WriteLine("01: Random Array");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
string choice = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

switch (choice)
{
  case "01":
    practice01.Run();
    break;
  default:
    Console.WriteLine("Invalid input!");
    break;
}