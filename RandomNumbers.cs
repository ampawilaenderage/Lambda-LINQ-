using System.Linq;

namespace Lambda_LINQ
{
    internal class RandomNumbers
    {
        int count = 40;
        Random random = new Random();
        List<int> numbers = new List<int>();
        public List<int> RandomList()
        {
            for (int i = 0; i < count; i++)
            {
                numbers.Add(random.Next(1, 100));               
            }
            return numbers;          
        }

        internal void SortingNumbers(List<int> randomNumbers)
        {
            var sortedNumbers = randomNumbers.OrderBy(n => n).Distinct().ToList();
            var maxNumber = randomNumbers.OrderBy(n => n).Max();
            var minNumber = randomNumbers.OrderBy(n => n).Min();
            var sum = randomNumbers.OrderBy(n => n).Sum();

            foreach (var num in sortedNumbers)
            {
                Console.Write(num +" ");
            }
           
            Console.WriteLine();
            Console.WriteLine("Maximum Number is "+maxNumber);
            Console.WriteLine("Minimum Number is " + minNumber);
            Console.WriteLine("Sum is " + sum);
        }
        internal void SortingDescNumbers(List<int> randomNumbers)
        {
            var sortedNumbers = randomNumbers.OrderByDescending(n => n).ToList();

            foreach (var num in sortedNumbers)
            {
                Console.Write(num + " "); 
            }
            Console.WriteLine();
        }

        internal void NumbersLessthan(List<int> randomNumbers)
        {
            var sortedNumbers = randomNumbers.Where(n => n < 20).OrderByDescending(n => n).ToList();

            foreach (var num in sortedNumbers)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
    }
}