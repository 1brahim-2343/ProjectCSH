using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;
// ADD NOTIFICATION LOGIC
namespace Final.NetCore
{

    public class Worker
    {
        public Guid Id { get; set; }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");

                if (value.Length < 2)
                    throw new ArgumentException("Name must be at least 2 characters");

                _name = value;
            }
        }

        private string? _surname;
        public string? Surname
        {
            get { return _surname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Surname cannot be empty");

                if (value.Length < 2)
                    throw new ArgumentException("Surname must be at least 2 characters");

                _surname = value;
            }
        }

        private string? _residencyCity;
        public string? ResidencyCity
        {
            get { return _residencyCity; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Residency city cannot be empty");

                _residencyCity = value.Trim();
            }
        }

        private string? _phoneNo;
        public string? PhoneNo
        {
            get { return _phoneNo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone number cannot be empty");

                string cleanPhoneNo = value.Replace(" ", "").Replace("-", "");


                if (!cleanPhoneNo.IsValidAzerbaijaniPhone())
                    throw new InvalidPhoneNumber(value);

                _phoneNo = value;
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 18)
                    throw new ArgumentException("Worker must be at least 18 years old");

                if (value > 65)
                    throw new ArgumentException("Age cannot be more than 65");

                _age = value;
            }
        }

        public CV? cV { get; set; }
        public Worker(string? name, string? surname, string? residencyCity, string? phoneNo, int age, CV cv)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            ResidencyCity = residencyCity;
            PhoneNo = phoneNo;
            Age = age;
            cV = cv;
        }

        public Worker()
        {

        }
    }
    public class Employer
    {
        public Guid Id { get; set; }
        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");

                if (value.Length < 2)
                    throw new ArgumentException("Name must be at least 2 characters");

                _name = value;
            }
        }

        private string? _surname;
        public string? Surname
        {
            get { return _surname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Surname cannot be empty");

                if (value.Length < 2)
                    throw new ArgumentException("Surname must be at least 2 characters");

                _surname = value;
            }
        }

        private string? _residencyCity;
        public string? ResidencyCity
        {
            get { return _residencyCity; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Residency city cannot be empty");

                _residencyCity = value.Trim();
            }
        }

        private string? _phoneNo;
        public string? PhoneNo
        {
            get { return _phoneNo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone number cannot be empty");

                string cleanPhoneNo = value.Replace(" ", "").Replace("-", "");


                if (!cleanPhoneNo.IsValidAzerbaijaniPhone())
                    throw new InvalidPhoneNumber(value);

                _phoneNo = value;
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 18 || value > 65)
                    throw new ArgumentException("Worker must be between 18 and 65 years old");

                _age = value;
            }
        }
        public List<string>? Vacancies { get; set; }

        public Employer()
        {

        }
        public Employer(string? name, string? surname, string? residencyCity, string? phoneNo, int age, List<string>? vacancies)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            ResidencyCity = residencyCity;
            PhoneNo = phoneNo;
            Age = age;
            Vacancies = vacancies;
        }
    }

    public class CV
    {
        private string? _specialty;

        public string? Specialty
        {
            get { return _specialty; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Specialty cannot be empty");
                _specialty = value;
            }
        }

        private string? _schoolName;

        public string? SchoolName
        {
            get { return _schoolName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("School name cannot be empty");
                _schoolName = value;
            }
        }

        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                if (value <= 100 || value >= 700)
                    throw new ArgumentException("Score must be between 100 and 700");
                _score = value;
            }
        }
        public List<string>? Skills { get; set; }

        public List<string>? PreviousCompanies { get; set; }
        public DateTime CareerStart { get; set; }

        public DateTime PreviousJobEndDate { get; set; }

        private Dictionary<string, string>? _languagesLevel;

        public Dictionary<string, string>? LangsLevel
        {
            get { return _languagesLevel; }
            set
            {
                if (value != null && (value.ContainsValue("A1") || value.ContainsValue("A2") ||
                   value.ContainsValue("B1") || value.ContainsValue("B2") ||
                   value.ContainsValue("C1") || value.ContainsValue("C2")))
                    _languagesLevel = value;

            }
        }

        public bool HasDistinctionDiploma { get; set; }


        public CV()
        {

        }
        public CV(string? specialty, string? schoolName, int score, List<string>? skills, List<string>? previousCompanies, DateTime careerStartDate, DateTime previousJobEndDate, Dictionary<string, string>? langsLevel, bool hasDistinctionDiploma)
        {
            Specialty = specialty;
            SchoolName = schoolName;
            Score = score;
            Skills = skills;
            if(previousJobEndDate < careerStartDate)
            {
                throw new TimingException(careerStartDate, previousJobEndDate);
            }
            PreviousCompanies = previousCompanies;
            CareerStart = careerStartDate;
            PreviousJobEndDate = previousJobEndDate;
            LangsLevel = langsLevel;
            HasDistinctionDiploma = hasDistinctionDiploma;
        }
    }


    class Menu
    {
        public static void WorkerSignIn(List<Worker> workers)
        {
            Console.Clear();
            Console.WriteLine("Enter phone number(disregard+994)");
            string phoneNo = string.Empty;
            try
            {
                phoneNo = Program.DashedInput();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            var worker = workers.FirstOrDefault(w => w.PhoneNo == new string("+994" + phoneNo));
            int now = DateTime.Now.Hour; // ADD NOTIFICATION LOGIC
            if (worker != null)
            {
                if (now >= 6 && now < 12)
                    Console.WriteLine($"Good morning, {worker.Name}");
                else if (now >= 12 && now <= 17)
                {
                    Console.WriteLine($"Good afternoon, {worker.Name}");
                }
                else
                {
                    Console.WriteLine($"Good evening, {worker.Name}");
                }
            }
            else
            {
                throw new WorkerNotFoundException(phoneNo);
            }
        }

        public static void WorkerSignUp(List<Worker> workers)
        {
            Console.Clear();
            string pageTitle = "Sign Up page";
            pageTitle.FullWidthLine();
            Console.Write("Enter name: ");
            string? name = Console.ReadLine();

            Console.Write("Enter surname: ");
            string? surname = Console.ReadLine();

            Console.Write("Enter residence city: ");
            string? residencyCity = Console.ReadLine();

            Console.Write("Enter phone number(example: +99450...): ");
            string? phoneNo = Console.ReadLine();

            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter specialty: ");
            string? specialty = Console.ReadLine();

            Console.Write("Enter school name: ");
            string? schoolName = Console.ReadLine();

            Console.Write("Enter score: ");
            int score = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter skills (comma separated): ");
            string? skills = Console.ReadLine();
            List<string>? skillsList = skills?.Split(',').ToList();

            Console.Write("Enter previous companies (comma separated): ");
            string? previousCompanies = Console.ReadLine();
            List<string>? previousCompaniesList = previousCompanies?.Split(',').ToList();

            Console.Write("Enter career start date (YYYY-MM-DD): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            Console.Write("Enter previous job end date (YYYY-MM-DD): ");
            DateTime previousJobEndDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            Console.Write("How many languages do you know? ");
            int langCount = int.Parse(Console.ReadLine() ?? "1");
            Dictionary<string, string> langsLevel = new Dictionary<string, string>();

            for (int i = 0; i < langCount; i++)
            {
                Console.Write($"Language {i + 1}: ");
                string? lang = Console.ReadLine();
                Console.Write($"Level (A1/A2/B1/B2/C1/C2): ");
                string? level = Console.ReadLine();

                if (lang != null && level != null)
                    langsLevel.Add(lang, level);
                else
                {
                    throw new ArgumentNullException("Language or Level of the language can not be null");
                }
            }

            Console.Write("Do you have a distinction diploma? (y/n): ");
            var diplomaKey = Console.ReadKey(true);
            bool hasDistinctionDiploma = false;
            if (diplomaKey.Key == ConsoleKey.Y)
            {
                hasDistinctionDiploma = true;
            }
            Console.WriteLine();
            string title = "Please re-check your information:";
            title.FullWidthLine();
            Console.WriteLine($"Name: {name} {surname}");
            Console.WriteLine($"City: {residencyCity}");
            Console.WriteLine($"Phone: {phoneNo}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Specialty: {specialty}");
            Console.Write("\nIs everything correct? (y/n): ");

            var confirmKey = Console.ReadKey(true);
            Console.WriteLine();

            if (confirmKey.Key == ConsoleKey.Y)
            {
                try
                {
                    CV cv = new CV(specialty, schoolName, score, skillsList, previousCompaniesList,
                                  startDate, previousJobEndDate, langsLevel, hasDistinctionDiploma);

                    Worker newWorker = new Worker(name, surname, residencyCity, phoneNo, age, cv);

                    workers.Add(newWorker);
                    Program.WriteJsonWorkers(workers);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nDear {name}, welcome to board! Your registration is complete.");
                    Console.ResetColor();
                    Console.WriteLine("1.Continue to vacancies\n2.Return to main page\nESC save&exit");
                    var choice = Console.ReadKey(true);
                    if (choice.Key == ConsoleKey.D1)
                    {
                        //Vacancies Menu
                    }
                    else if (choice.Key == ConsoleKey.D2)
                    {

                    }
                    else if(choice.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            else
            {
                Console.WriteLine("\nRegistration cancelled.");
            }
        }
    }
    internal class Program
    {
        public static void WriteJsonWorkers(List<Worker> workers)
        {
            var jsonSerializer = new JsonSerializer();
            using (var sw = new StreamWriter("workers.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    jsonSerializer.Serialize(jw, workers);
                }
            }
        }

        static List<Worker> ReadJsonWorkers()
        {
            var serializer = new JsonSerializer();
            if (File.Exists("workers.json"))
            {
                using (var sr = new StreamReader("workers.json"))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        var workers = serializer.Deserialize<List<Worker>>(jr);
                        return workers ?? [];
                    }
                }
            }
            else
            {
                File.Create("workers.json");
                return [];
            }
        }

        public static void WriteXMLEmployers(List<Employer> employers)
        {
            var xml = new XmlSerializer(typeof(List<Employer>));
            using (var fs = new FileStream("employers.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, employers);
            }
        }

        static List<Employer> ReadXMLEmployers()
        {
            List<Employer>? employers = null;
            if (!File.Exists("employers.xml") || new FileInfo("employers.xml").Length == 0)
            {
                return [];
            }

            var xml = new XmlSerializer(typeof(List<Employer>));
            using (var fs = new FileStream("employers.xml", FileMode.OpenOrCreate))
            {
                employers = xml.Deserialize(fs) as List<Employer>;
            }
            return employers ?? [];


        }

        public static string DashedInput()
        {
            StringBuilder phoneNumber = new StringBuilder("---------");
            for (int index = 0; index < phoneNumber.Length; index++)
            {
                var key = Console.ReadKey(true);
                if (int.TryParse(key.KeyChar.ToString(), out int num))
                {

                    phoneNumber[index] = key.KeyChar;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    phoneNumber[--index] = '-';
                    index--;
                }
                else
                {
                    throw new ArgumentException("Input must be digit(0-9)");
                }
                Console.Clear();
                Console.WriteLine($"Phone NO: {phoneNumber}");
            }
            return phoneNumber.ToString();
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                List<Worker> workers = ReadJsonWorkers();
                List<Employer> employers = ReadXMLEmployers();
                Console.Clear();
                Console.WriteLine("Choose: ");
                Console.WriteLine("1. Worker");
                Console.WriteLine("2. Employer");

                var choice = Console.ReadKey(true);

                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("1.Sign in");
                        Console.WriteLine("2.Sign up");
                        Console.WriteLine("ESC for returning back");
                        var choiceSigning = Console.ReadKey(true);
                        if (choiceSigning.Key == ConsoleKey.D1)
                            try
                            {
                                Menu.WorkerSignIn(workers);
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                                Thread.Sleep(2200);
                            }
                        else if (choiceSigning.Key == ConsoleKey.D2)
                        {
                            Menu.WorkerSignUp(workers);
                        }
                        else if (choiceSigning.Key == ConsoleKey.Escape)
                        {
                            continue;
                        }


                        break;
                    default:
                        break;
                }
            }

        }
    }
}
