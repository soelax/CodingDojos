using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvTabellieren
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string>
            {
                "Name;Strasse;Ort;Alter",
                "Peter Pan; Am Hang 5; 12345 Einsam; 42",
                "Maria Schmitz; Kölner Straße 45; 50123 Köln; 43",
                "Paul Meier; Münchener Weg 1; 87654 München; 65"
            };

            var result = tabellize(input);

            result.ToList().ForEach(s => Console.WriteLine(s));
        }

        private static IEnumerable<string> tabellize(IEnumerable<string> input)
        {
            if (input == null) return null;
            if (input.Count() == 0) return new List<string>();

            var headers = input.ElementAt(0).Split(';');
            var lengths = new int[headers.Length];
            for(int i = 0; i < headers.Length; i++)
            {
                lengths[i] = headers[i].Length;
            }

            for(int i = 1; i < input.Count(); i++)
            {
                var items = input.ElementAt(i).Split(';');
                for(int item = 0; item < items.Length; item++)
                {
                    if (items[item].Length > lengths[item]) lengths[item] = items[item].Length;
                }
            }

            var result = new List<string>();
            var charCount = lengths.Sum() + lengths.Length - 1;
            var nextResult = new string('-', charCount);
            result.Add(nextResult);

            nextResult = "";
            for (int i = 0; i < headers.Length - 1; i++) nextResult = nextResult + headers[i].PadRight(lengths[i], ' ') + "|";
            nextResult += headers.Last();
            result.Add(nextResult);

            for(int i = 1; i < input.Count(); i++)
            {
                var items = input.ElementAt(i).Split(';');
                nextResult = "";
                for (int item = 0; item < items.Length; item++)
                {
                    nextResult = nextResult + items[item].PadRight(lengths[item], ' ');
                    nextResult += "|";
                }
                nextResult = nextResult.Substring(0, nextResult.Length - 1);
                result.Add(nextResult);
            }

            return result;
        }
    }
}
