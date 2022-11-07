using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace TheGroupProject
{
    //Ansvarsområden:
    //Ingrid: DescriptiveStatistics, IfNull & OOP.
    //Rasmus: Minimum & Typvärde
    //Marcus: Maximum & Variationsbredd
    //Alexander: Medelvärde & Median
    //Mohammed: Standardavvikelse

    //Statisk klass som innehåller statiska metoder för att utföra efterfrågade beräkningar.
    public static class Statistics
    {
        // Ingrid

        //Konverterar JSON-fil till int[] vid namn source.
        public static int[] source = JsonConvert.DeserializeObject<int[]>(File.ReadAllText("data.json"));

        //Metod som kastar undantag om filen innehåller null eller saknar element.
        public static void IfNull()
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            if (source.Length == 0)
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }
        }
        public static dynamic DescriptiveStatistics()
        {
            // Ingrid 

            // Ordlista som innehåller efterfrågade beräkningar.
            Dictionary<string, dynamic> StatisticsList = new Dictionary<string, dynamic>()
            {
                { "Maximum", Maximum() },
                { "Minimum", Minimum() },
                { "Medelvärde", Mean() },
                { "Median", Median() },
                { "Typvärde", String.Join(", ", Mode()) }, // String.Join "joinar"/lägger ihop alla värden från array som metoden Mode() returnerar. 
                { "Variationsbredd", Range() },
                { "Standardavvikelse", StandardDeviation() }

            };

            //Utskriften sparas i variabel output, som hämtar information från Dictionary via "key"
            string output =
                $"Maximum: {StatisticsList["Maximum"]}\n" +
                $"Minimum: {StatisticsList["Minimum"]}\n" +
                $"Medelvärde: {StatisticsList["Medelvärde"]}\n" +
                $"Median: {StatisticsList["Median"]}\n" +
                $"Typvärde: {StatisticsList["Typvärde"]}\n" +
                $"Variationsbredd: {StatisticsList["Variationsbredd"]}\n" +
                $"Standardavvikelse: {StatisticsList["Standardavvikelse"]}";

            return output;
        }

        public static int Maximum()
        {
            //Marcus
            IfNull();

            //Sorterar listan från minst till högst
            Array.Sort(Statistics.source);

            //Vänder på listan så vi får det högsta värdet på plats "0" i arrayen
            Array.Reverse(source);

            //Ger result samma värde som source har på plats 0
            int result = source[0];

            //Returnerar result
            return result;
        }

        public static int Minimum()
        {
            // Rasmus
            IfNull();
            Array.Sort(Statistics.source);

            int result = source[0];

            return result;
        }

        public static double Mean()
        {
            // Alexander
            IfNull();
            Statistics.source = source;
            //Jämnar upp så det blir med en decimal
            double total = -88;

            for (int i = 0; i < source.LongLength; i++)
            {
                total += source[i];
            }
            return total / source.LongLength;
        }

        public static double Median()
        {
            // Alexander
            IfNull();
            //Sorterar arrayen
            Array.Sort(source);

            int size = source.Length;
            //Tar talet som är i mitten av filen
            int mid = size / 2;

            int dbl = source[mid];
            //Och returnerar det
            return dbl;
        }

        public static int[] Mode()
        {
            // Rasmus
            IfNull();
            // Vi upplevde beräkning av "typvärde" som väldigt svår. Så här hjälptes vi alla åt.

            int mode = source.GroupBy(n => n).
            OrderByDescending(g => g.Count()).
            Select(g => g.Key).FirstOrDefault();

            int mode2 = source.GroupBy(n => n).
            OrderByDescending(g => g.Count()).
            Select(g => g.Key).ElementAt(1);

            int mode3 = source.GroupBy(n => n).
            OrderByDescending(g => g.Count()).
            Select(g => g.Key).ElementAt(2);

            int[] trimodal = { mode, mode2, mode3 };

            return trimodal;

        }

        public static int Range()
        {
            // Marcus 
            IfNull();
            //Sorterar listan minst till högst
            Array.Sort(Statistics.source);

            //Sätter ut värden på variablerna "min" och "max" så att det får värdet på plats "0" i listan
            int min = source[0];
            int max = source[0];

            //Hämtar värdet som är längat bak i listan som vi sedan tilldelar till "max" så vi kan göra vår uträkning 
            for (int i = 0; i < source.Length; i++)
                if (source[i] > max)
                    max = source[i];

            //Beräknar variationsbredden
            int range = max - min;

            //returnerar "range" så vi kan skriva ut den
            return range;
        }

        public static double StandardDeviation()
        {
            // Mohammed 
            IfNull();
            double average = source.Average();
            double sumOfSquaresOfDifferences = source.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / source.Length);

            double round = Math.Round(sd, 1);


            return round;
        }

    }
}