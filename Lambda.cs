
namespace Lambda_LINQ
{

    internal class Lambda
    {
        delegate void MitarbeiterSort(List<Mitarbeiter> mitarbeiterliste);
        delegate void NumberSort(List<int> randomNumbers);
        delegate double MatheHandler(double zahl);
        enum Abteilung { Vetrieb, Buchhaltung, Entwicklung }
        static void Main(string[] args)
        {
            double zahl = 4.84;

            // Lambda expression instead of anonymous method
            MatheHandler mathe = x => Math.Round(x);

            Console.WriteLine($"Lambda expression: {zahl} rounded = {mathe(zahl)}");

            // Using ExpressionBodies class
            ExpressionBodies eb = new ExpressionBodies(5);

            Console.WriteLine($"Quadrat: {eb.Quadrat}");

            eb.ZahlQuadrat = 4;
            Console.WriteLine($"ZahlQuadrat Getter returns: {eb.ZahlQuadrat}");

            Console.WriteLine($"Summe with 3: {eb.Summe(3)}");

            /********** LINQ *************/

            List<Mitarbeiter> mitarbeiterliste = new List<Mitarbeiter>();

            mitarbeiterliste.Add(new Mitarbeiter("Schmidt", "Hans", Abteilung.Buchhaltung, 2485, "12.04.1958"));
            mitarbeiterliste.Add(new Mitarbeiter("Schulz", "Gabi", Abteilung.Vetrieb, 2743, "15.08.1979"));
            mitarbeiterliste.Add(new Mitarbeiter("Ransauer", "Karl", Abteilung.Entwicklung, 4213, "28.08.1964"));
            mitarbeiterliste.Add(new Mitarbeiter("Müller", "Maria", Abteilung.Vetrieb, 2196, "09.01.1998"));
            mitarbeiterliste.Add(new Mitarbeiter("Schmidt", "Anna", Abteilung.Buchhaltung, 2876, "07.08.1974"));
            mitarbeiterliste.Add(new Mitarbeiter("Meier", "Franz", Abteilung.Vetrieb, 3255, "16.12.1958"));
            mitarbeiterliste.Add(new Mitarbeiter("Kiesling", "Susanne", Abteilung.Entwicklung, 3184, "20.11.2000"));

            LinqOperations linqQueries = new LinqOperations();
            MitarbeiterSort del = null;
            NumberSort delsort = null;
            // Assign methods from another class
            del += linqQueries.ListNames;
            del += linqQueries.GruppenAbteilung;
            del += linqQueries.SortingAbteilung;
            del += linqQueries.SortingGebursTag;
            del(mitarbeiterliste);

            //linqQueries.ListNames(mitarbeiterliste);
            //linqQueries.GruppenAbteilung(mitarbeiterliste);
            //linqQueries.SortingAbteilung(mitarbeiterliste);
            //linqQueries.SortingGebursTag(mitarbeiterliste);

            RandomNumbers numbers = new RandomNumbers();
            List<int> randomNumbers = numbers.RandomList();
            delsort += numbers.SortingNumbers;
            delsort += numbers.SortingDescNumbers;
            delsort += numbers.NumbersLessthan;
            delsort(randomNumbers);

        }

        class ExpressionBodies
        {
            int zahl;

            // expression-bodied read-only property
            public int Quadrat => zahl * zahl;

            // expression-bodied accessors
            public int ZahlQuadrat
            {
                get => zahl;
                set => zahl = value * value;
            }

            // expression-bodied constructor
            public ExpressionBodies(int zahl) => this.zahl = zahl;

            // expression-bodied method
            public double Summe(int a) => zahl + a;
        }

        class Mitarbeiter
        {
            public string Name { get; private set; }
            public string Vorname { get; private set; }
            public Abteilung Abteilung { get; private set; }
            public double Gehalt { get; private set; }
            public DateTime Geburtstag { get; private set; }

            public Mitarbeiter(string name, string vorname, Abteilung abteilung, double gehalt, string geburtstag)
            {
                Name = name;
                Vorname = vorname;
                Abteilung = abteilung;
                Gehalt = gehalt;
                Geburtstag = DateTime.Parse(geburtstag);
            }
        }

        class LinqOperations
        {
            public void ListNames(List<Mitarbeiter> mitarbeiterliste)
            {
                // LINQ query: get all last names
                var nachnamen = mitarbeiterliste.Select(m => m.Name);

                Console.WriteLine("All last names:");

                foreach (var name in nachnamen)
                {
                    Console.WriteLine(name);
                }
            }

            internal void GruppenAbteilung(List<Mitarbeiter> mitarbeiterliste)
            {
                // LINQ query: get all records with grouped by Abteilung
                var gruppen = mitarbeiterliste.GroupBy(m => m.Abteilung);

                Console.WriteLine("Employees grouped by department:\n");

                foreach (var gruppe in gruppen.Where(g => g.Key == Abteilung.Buchhaltung))
                {
                    Console.WriteLine($"Department: {gruppe.Key}");

                    foreach (var mitarbeiter in gruppe.Where(m => m.Abteilung == Abteilung.Buchhaltung))
                    {
                        Console.WriteLine($"   {mitarbeiter.Vorname} {mitarbeiter.Name}");
                    }

                    Console.WriteLine();
                }
            }

            internal void SortingAbteilung(List<Mitarbeiter> mitarbeiterliste)
            {
                var sortierteMitarbeiter = mitarbeiterliste.OrderByDescending(m => m.Abteilung.ToString()).OrderByDescending(m => m.Name).ThenBy(m => m.Vorname);

                foreach (var mitarbeiter in sortierteMitarbeiter)
                {
                    Console.WriteLine($"{mitarbeiter.Abteilung} - {mitarbeiter.Vorname} {mitarbeiter.Name}");
                } // test 1
            }

            internal void SortingGebursTag(List<Mitarbeiter> mitarbeiterliste)
            {
                var augustGeburtstage = mitarbeiterliste
                                        .Where(m => m.Geburtstag.Month == 8)
                                        .OrderByDescending(m => m.Geburtstag.Day);

                foreach (var mitarbeiter in augustGeburtstage)
                {
                    Console.WriteLine($"{mitarbeiter.Vorname} {mitarbeiter.Name}");
                }
                Console.WriteLine();
            }
        }
    }
}
