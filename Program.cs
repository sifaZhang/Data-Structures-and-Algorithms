// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;

Console.WriteLine("Please input the number:");
Console.WriteLine("01: Random Array");
string choice = Console.ReadLine();

switch (choice)
{
  case "01":
    practice01.Run();
    break;
  default:
    Console.WriteLine("Invalid input!");
    break;
}