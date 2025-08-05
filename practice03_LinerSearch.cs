
    public class LinerSearch
{
        public static void Run()
        {
            Console.WriteLine("----------------03 start----------------");

            Console.WriteLine("Please input the size of the array:");
        string strSize = Console.ReadLine() ?? string.Empty;
            int size = int.Parse(strSize);
            Console.WriteLine("Please input the maximum value for the random numbers:");
            string strMaxValue = Console.ReadLine() ?? string.Empty;
            int maxValue = int.Parse(strMaxValue);

            int[] numbers = CreateRandomArray(size, maxValue);
            Console.WriteLine("Generated array:");
            for(int number = 0; number < numbers.Length; number++)
            {
                Console.WriteLine("array[{0}]={1}", number, numbers[number]);
            }

            Console.WriteLine("Which number do you want to find:");
            string strTarget = Console.ReadLine() ?? string.Empty;
        int target = int.Parse(strTarget);

            int index;
            if (BinarySearch(numbers, target, out index))
            {
                Console.WriteLine($"Number {target} found at index {index}.");
            }
            else
            {
                Console.WriteLine($"Number {target+1} not found in the array.");
            }

            Console.WriteLine("----------------03 end----------------");
        }


        public static int[] CreateRandomArray(int size, int maxValue)
        {
            Random rand = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(maxValue);
            }

            return array;
        }

        public static bool BinarySearch(int[] array, int target, out int index)
        {
            index = -1; // Default to -1 if not found
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }
    }
