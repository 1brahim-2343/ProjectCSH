using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;

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

                string cleanPhone = value.Replace(" ", "").Replace("-", "");


                if (cleanPhone.Length < 9 || cleanPhone.Length > 16)
                    throw new ArgumentException("Phone number must be between 9 and 16 digits");

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

                string cleanPhone = value.Replace(" ", "").Replace("-", "");


                if (cleanPhone.Length < 9 || cleanPhone.Length > 16)
                    throw new ArgumentException("Phone number must be between 9 and 16 digits");

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
        public DateTime StartDate { get; set; }

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
        public CV(string? specialty, string? schoolName, int score, List<string>? skills, List<string>? previousCompanies, DateTime startDate, DateTime previousJobEndDate, Dictionary<string, string>? langsLevel, bool hasDistinctionDiploma)
        {
            Specialty = specialty;
            SchoolName = schoolName;
            Score = score;
            Skills = skills;
            PreviousCompanies = previousCompanies;
            StartDate = startDate;
            PreviousJobEndDate = previousJobEndDate;
            LangsLevel = langsLevel;
            HasDistinctionDiploma = hasDistinctionDiploma;
        }
    }


    internal class Program
    {
        static void WriteJsonWorkers(List<Worker> workers)
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
            using (var sr = new StreamReader("workers.json"))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    var workers = serializer.Deserialize<List<Worker>>(jr);
                    return workers ?? [];
                }
            }
        }

        static void WriteXMLEmployers(List<Employer> employers)
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
            var xml = new XmlSerializer(typeof(List<Employer>));
            using (var fs = new FileStream("employers.xml", FileMode.OpenOrCreate))
            {
                employers = xml.Deserialize(fs) as List<Employer>;
            }
            return employers ?? [];
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Worker> workers = new List<Worker>{
                new Worker(
                    "Əli",
                    "Məmmədov",
                    "Bakı",
                    "+994501234567",
                    28,
                    new CV(
                        "Software Developer",
                        "Bakı Dövlət Universiteti",
                        450,
                        new List<string> { "C#", "JavaScript", "SQL", "React" },
                        new List<string> { "PASHA Bank", "Kapital Bank" },
                        new DateTime(2018, 6, 15),
                        new DateTime(2024, 12, 31),
                        new Dictionary<string, string> { { "English", "C1" }, { "Russian", "B2" }, { "Turkish", "A2" } },
                        true
                    )
                ),
                new Worker(
                    "Ayşə",
                    "Həsənova",
                    "Gəncə",
                    "+994552345678",
                    34,
                    new CV(
                        "Accountant",
                        "Azərbaycan Dövlət İqtisad Universiteti",
                        520,
                        new List<string> { "Excel", "1C", "Financial Analysis", "Tax Planning" },
                        new List<string> { "Azərsun Holding", "Azerenerji" },
                        new DateTime(2015, 9, 1),
                        new DateTime(2024, 11, 30),
                        new Dictionary<string, string> { { "English", "B2" }, { "Russian", "C1" } },
                        false
                    )
                ),
                new Worker(
                    "Rəşad",
                    "Quliyev",
                    "Sumqayıt",
                    "+994503456789",
                    42,
                    new CV(
                        "Mechanical Engineer",
                        "Azərbaycan Texniki Universiteti",
                        380,
                        new List<string> { "AutoCAD", "SolidWorks", "Project Management" },
                        new List<string> { "SOCAR", "Azərikimya" },
                        new DateTime(2010, 3, 10),
                        new DateTime(2024, 10, 15),
                        new Dictionary<string, string> { { "English", "B1" }, { "Russian", "C2" } },
                        true
                    )
                ),
                new Worker(
                    "Leyla",
                    "Əliyeva",
                    "Bakı",
                    "+994554567890",
                    29,
                    new CV(
                        "Marketing Specialist",
                        "Azərbaycan Dövlət İqtisad Universiteti",
                        495,
                        new List<string> { "Digital Marketing", "SEO", "Content Creation", "Google Analytics" },
                        new List<string> { "Bakcell", "Azercell" },
                        new DateTime(2019, 2, 20),
                        new DateTime(2025, 1, 10),
                        new Dictionary<string, string> { { "English", "C2" }, { "Turkish", "B1" } },
                        true
                    )
                ),
                new Worker(
                    "Elvin",
                    "İbrahimov",
                    "Mingəçevir",
                    "+994505678901",
                    38,
                    new CV(
                        "Civil Engineer",
                        "Azərbaycan Memarlıq və İnşaat Universiteti",
                        410,
                        new List<string> { "Construction Management", "Structural Design", "AutoCAD" },
                        new List<string> { "Akkord", "AzInşaat" },
                        new DateTime(2012, 7, 5),
                        new DateTime(2024, 9, 1),
                        new Dictionary<string, string> { { "English", "B2" }, { "Russian", "B2" } },
                        false
                    )
                )
            };

            WriteJsonWorkers(workers);
            List<Employer> employers = new List<Employer>
            {
                new Employer(
                    "Günay",
                    "Mustafayeva",
                    "Bakı",
                    "+994556789012",
                    45,
                    new List<string> { "Software Developer", "DevOps Engineer", "QA Tester" }
                ),
                new Employer(
                    "Tural",
                    "Rzayev",
                    "Bakı",
                    "+994507890123",
                    52,
                    new List<string> { "Accountant", "Financial Analyst", "Auditor" }
                ),
                new Employer(
                    "Sevil",
                    "Abdullayeva",
                    "Gəncə",
                    "+994558901234",
                    39,
                    new List<string> { "Marketing Manager", "Social Media Specialist", "Brand Manager" }
                ),
                new Employer(
                    "Kamran",
                    "Hüseynov",
                    "Sumqayıt",
                    "+994509012345",
                    48,
                    new List<string> { "Mechanical Engineer", "Production Manager", "Quality Control Specialist" }
                ),
                new Employer(
                    "Nərgiz",
                    "Məhərrəmova",
                    "Bakı",
                    "+994550123456",
                    41,
                    new List<string> { "Civil Engineer", "Project Manager", "Site Supervisor" }
                )
            };

            WriteXMLEmployers(employers);
        }
    }
}
