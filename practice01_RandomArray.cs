
public class RandomArray
{
  public static void Run()
  {
    Console.WriteLine("----------------01 start----------------");

    int iLen = 5;
    var rand = new Random();
    int[] scores = { rand.Next(100), rand.Next(100), rand.Next(100), rand.Next(100), rand.Next(100) };
    Console.Write("Generate random array:\n");
    for (int i = 0; i < iLen; i++)
    {
      Console.WriteLine($"{i}:{scores[i]}");
    }


    int iTemp = 0;
    for (int i = iLen - 1; i > 0; i--)
    {
      for (int j = 0; j < i; j++)
      {
        if (scores[j] > scores[j + 1])
        {
          iTemp = scores[j + 1];
          scores[j + 1] = scores[j];
          scores[j] = iTemp;
        }
      }
    }

    Console.Write("After sorted:\n");
    for (int i = 0; i < iLen; i++)
    {
      Console.WriteLine($"{i}:{scores[i]}");
    }

     Console.WriteLine("----------------01 end----------------");
  }
}