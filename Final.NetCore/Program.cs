using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;
// ADD NOTIFICATION LOGIC
namespace Final.NetCore
{

    public class EmployerNotification
    {
        public Guid WorkerId { get; set; }
        public string? WorkerName { get; set; }
        public string? VacancyName { get; set; }

        public EmployerNotification() { }

        public EmployerNotification(Guid workerId, string? workerName, string? vacancyName)
        {
            WorkerId = workerId;
            WorkerName = workerName;
            VacancyName = vacancyName;
        }

        public override string ToString()
        {
            return $"{WorkerName} applied for '{VacancyName}')";
        }
    }
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

        public Dictionary<string, string> Notifications { get; set; } = new Dictionary<string, string>();
        public List<CV> CVs { get; set; }
        public Worker(string? name, string? surname, string? residencyCity, string? phoneNo, int age, List<CV> cvs)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            ResidencyCity = residencyCity;
            PhoneNo = phoneNo;
            Age = age;
            CVs = cvs;
        }


        public void AddNotification(string vacancy)
        {
            if (Notifications.ContainsKey(vacancy))
            {
                Notifications[vacancy] = "Pending (Re-applied)";
            }
            else
            {
                Notifications.Add(vacancy, "Pending");
            }
        }
        public void UpdateApplicationStatus(string vacancy, string status)
        {
            if (Notifications.ContainsKey(vacancy))
            {
                Notifications[vacancy] = status;
            }
        }
        public Worker()
        {

        }

        public void ShowAllCVs()
        {
            int counter = 0;
            CVs.ForEach(cv => Console.WriteLine($"{++counter}. CV' exam score: {cv}"));
        }

        public void AddNewCv(CV cv)
        {
            CVs.Add(cv);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CV was added");
            Console.ResetColor();
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
        public List<EmployerNotification>? Notifications { get; set; }
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

        public void AddNotification(Worker worker, string vacancyName)
        {
            Notifications ??= new List<EmployerNotification>();
            Notifications.Add(new EmployerNotification(worker.Id, worker.Name, vacancyName));
        }

        public void ShowAllVacancies()
        {
            int count = 0;
            Vacancies?.ForEach(v => Console.WriteLine($"{++count}.{v}"));
        }

        public string GetFullName()
        {
            return $"{Name} {Surname}";
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
            if (previousJobEndDate < careerStartDate)
            {
                throw new TimingException(careerStartDate, previousJobEndDate);
            }
            PreviousCompanies = previousCompanies;
            CareerStart = careerStartDate;
            PreviousJobEndDate = previousJobEndDate;
            LangsLevel = langsLevel;
            HasDistinctionDiploma = hasDistinctionDiploma;
        }

        public override string ToString()
        {

            return $"{Score}";
        }
    }


    class Menu
    {
        public static CV CreateCV()
        {
            Console.Write("Enter specialty: ");
            string? specialty = Console.ReadLine();

            Console.Write("Enter school name: ");
            string? schoolName = Console.ReadLine();

            Console.Write("Enter score: ");
            int score = int.Parse(Console.ReadLine() ?? string.Empty);

            Console.Write("Enter skills (comma separated): ");
            List<string>? skills = Console.ReadLine()?.Split(',').ToList();

            Console.Write("Enter previous companies (comma separated): ");
            List<string>? previousCompanies = Console.ReadLine()?.Split(',').ToList();

            Console.Write("Enter start date (YYYY-MM-DD): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

            Console.Write("Enter previous job end date (YYYY-MM-DD): ");
            DateTime previousJobEndDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

            Dictionary<string, string> langsLevel = GetLanguageLevels();

            Console.Write("Do you have a distinction diploma? (y/n): ");
            var diplomaKey = Console.ReadKey(true);
            bool hasDistinctionDiploma = diplomaKey.Key == ConsoleKey.Y;
            Console.WriteLine();

            return new CV(specialty, schoolName, score, skills, previousCompanies,
                          startDate, previousJobEndDate, langsLevel, hasDistinctionDiploma);
        }
        public static Dictionary<string, string> GetLanguageLevels()
        {
            Console.Write("How many languages do you speak? ");
            int langCount = int.Parse(Console.ReadLine() ?? string.Empty);
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
                    throw new ArgumentNullException("Language name and level cannot be empty");
            }

            return langsLevel;
        }
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
            var currentWorker = workers.FirstOrDefault(w => w.PhoneNo == new string("+994" + phoneNo));
            int now = DateTime.Now.Hour;
            if (currentWorker != null)
            {
                if (now >= 6 && now < 12)
                    Console.WriteLine($"Good morning, {currentWorker.Name}");
                else if (now >= 12 && now <= 17)
                {
                    Console.WriteLine($"Good afternoon, {currentWorker.Name}");
                }
                else
                {
                    Console.WriteLine($"Good evening, {currentWorker.Name}");
                }
            }
            else
            {
                throw new WorkerNotFoundException(phoneNo);
            }

            Console.WriteLine("1.View Vacancies\n2.View all CVs\n3.Add CV\n4.View all notifications\nESC to exit");

            var choiceAfterSignUp = Console.ReadKey(true);

            switch (choiceAfterSignUp.Key)
            {
                case ConsoleKey.D1:
                    VacanciesPage(currentWorker);
                    break;
                case ConsoleKey.D2:
                    if (currentWorker.CVs == null || currentWorker.CVs.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no CV at all");
                        Console.ResetColor();
                        Console.WriteLine("Add new?(y/n)");
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            currentWorker.AddNewCv(CreateCV());
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        currentWorker.ShowAllCVs();
                    }
                    Console.WriteLine("Press anything to continue...");
                    Console.ReadKey(true);
                    break;
                case ConsoleKey.D3:
                    currentWorker.AddNewCv(CreateCV());
                    break;
                case ConsoleKey.D4:
                    if (currentWorker.Notifications.Count >= 1)
                        foreach (var item in currentWorker.Notifications)
                        {
                            Console.WriteLine($"{item.Key}, {item.Value}");
                        }
                    else
                        Console.WriteLine("No notificatons");
                    Console.WriteLine("Press anything to continue...");
                    Console.ReadKey(true);
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    break;
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

            CV cv = CreateCV();

            Console.WriteLine();
            string title = "Please re-check your information:";
            title.FullWidthLine();
            Console.WriteLine($"Name: {name} {surname}");
            Console.WriteLine($"City: {residencyCity}");
            Console.WriteLine($"Phone: {phoneNo}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Specialty: {cv.Specialty}");
            Console.Write("\nIs everything correct? (y/n): ");

            var confirmKey = Console.ReadKey(true);
            Console.WriteLine();

            if (confirmKey.Key == ConsoleKey.Y)
            {
                try
                {

                    List<CV> cvs = new List<CV> { cv };
                    Worker newWorker = new Worker(name, surname, residencyCity, phoneNo, age, cvs);

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
                        try
                        {
                            VacanciesPage(newWorker);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else if (choice.Key == ConsoleKey.D2)
                    {

                    }
                    else if (choice.Key == ConsoleKey.Escape)
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
                Thread.Sleep(1000);
            }
        }

        public static void VacanciesPage(Worker currentWorker)
        {
            Console.Clear();
            List<Employer> employers = Program.ReadJsonEmployers();

            var employersWVacancy = employers.Where(e => e?.Vacancies?.Count != 0).ToList();
            if (!employersWVacancy.Any())
            {
                throw new EmptyList<Employer>(employers);
            }
            for (int i = 0; i < employersWVacancy.Count; i++)
            {
                string title = $"{i + 1}. {employersWVacancy[i].GetFullName()}'s vacancies: ";
                title.HalfWidthLine();
                employersWVacancy[i].ShowAllVacancies();
            }
            Console.WriteLine();
            Console.Write($"Choose one employer(input number[1-{employersWVacancy.Count}]: ");
            var choiceEmployer = Console.ReadLine();
            if (int.TryParse(choiceEmployer?.ToString(), out int numEmployer))
            {
                if (numEmployer > employersWVacancy.Count)
                {
                    throw new IndexOutOfRangeException($"Do not enter number greater than {employersWVacancy.Count}");
                }
                var chosenEmployer = employersWVacancy[numEmployer - 1];
                Console.Clear();
                new string($"{chosenEmployer?.GetFullName()}'s vacancies")?.HalfWidthLine();
                chosenEmployer?.ShowAllVacancies();
                Console.Write("Choose one of the available vacancies: ");
                var choiceVacancy = Console.ReadLine();
                if (int.TryParse(choiceVacancy?.ToString(), out int numVacancy))
                {
                    if (numVacancy > chosenEmployer?.Vacancies?.Count)
                    {
                        throw new IndexOutOfRangeException($"Do not enter number greater than {chosenEmployer.Vacancies.Count}");
                    }
                    CV? chosenCV = null;
                    if (currentWorker.CVs.Count > 1)
                    {
                        currentWorker.ShowAllCVs();
                        Console.WriteLine("Choose CV to send: ");
                        var choiceCV = Console.ReadLine();
                        if (int.TryParse(choiceCV, out int numCV))
                        {
                            chosenCV = currentWorker.CVs[numCV - 1];
                        }
                    }
                    else
                    {
                        chosenCV = currentWorker.CVs[0];
                    }
                    var chosenVacancy = chosenEmployer?.Vacancies?[numVacancy - 1];
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Thank you {currentWorker.Name}, request to \"{chosenVacancy}\" vacancy was successfully sent. Used CV(score): {chosenCV}");
                    chosenEmployer?.AddNotification(currentWorker, chosenVacancy ?? "X");
                    currentWorker.AddNotification(chosenVacancy ?? "X");
                    Program.WriteJsonEmployers(employers);
                    Console.ResetColor();
                    Thread.Sleep(2000);
                }
            }

        }


        public static void EmployerSignIn(List<Employer> employers)
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
            var currentEmployer = employers.FirstOrDefault(e => e.PhoneNo == new string("+994" + phoneNo));
            int now = DateTime.Now.Hour;
            if (currentEmployer != null)
            {
                if (now >= 6 && now < 12)
                    Console.WriteLine($"Good morning, {currentEmployer.Name}");
                else if (now >= 12 && now <= 17)
                {
                    Console.WriteLine($"Good afternoon, {currentEmployer.Name}");
                }
                else
                {
                    Console.WriteLine($"Good evening, {currentEmployer.Name}");
                }
            }
            else
            {
                throw new EmployerNotFoundException(phoneNo);
            }

            Console.WriteLine("1.View Notifications\n2.View all Vacancies\n3.Add Vacancy\nESC to exit");

            var choiceAfterSignIn = Console.ReadKey(true);

            switch (choiceAfterSignIn.Key)
            {
                case ConsoleKey.D1:
                    ViewNotifications(currentEmployer, Program.ReadJsonWorkers());
                    break;
                case ConsoleKey.D2:
                    if (currentEmployer.Vacancies == null || currentEmployer.Vacancies.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There are no vacancies");
                        Console.ResetColor();
                        Console.WriteLine("Add new?(y/n)");
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            AddVacancy(currentEmployer);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        currentEmployer.ShowAllVacancies();
                    }
                    Console.WriteLine("Press anything to leav...");
                    Console.ReadKey(true);
                    break;
                case ConsoleKey.D3:
                    AddVacancy(currentEmployer);
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        public static void EmployerSignUp(List<Employer> employers)
        {
            Console.Clear();
            string pageTitle = "Employer Sign Up page";
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

            Console.Write("Enter vacancies (comma separated): ");
            List<string>? vacancies = Console.ReadLine()?.Split(',').ToList();

            Console.WriteLine();
            string title = "Please re-check your information:";
            title.FullWidthLine();
            Console.WriteLine($"Name: {name} {surname}");
            Console.WriteLine($"City: {residencyCity}");
            Console.WriteLine($"Phone: {phoneNo}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Vacancies: {string.Join(", ", vacancies ?? new List<string>())}");
            Console.Write("\nIs everything correct? (y/n): ");

            var confirmKey = Console.ReadKey(true);
            Console.WriteLine();

            if (confirmKey.Key == ConsoleKey.Y)
            {
                try
                {
                    Employer newEmployer = new Employer(name, surname, residencyCity, phoneNo, age, vacancies);

                    employers.Add(newEmployer);
                    Program.WriteJsonEmployers(employers);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nDear {name}, welcome to board! Your registration is complete.");
                    Console.ResetColor();
                    Console.WriteLine("1.Continue to notifications\n2.Return to main page\nESC save&exit");
                    var choice = Console.ReadKey(true);
                    if (choice.Key == ConsoleKey.D1)
                    {
                        try
                        {
                            ViewNotifications(newEmployer, Program.ReadJsonWorkers());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else if (choice.Key == ConsoleKey.D2)
                    {

                    }
                    else if (choice.Key == ConsoleKey.Escape)
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
                Thread.Sleep(1000);
            }
        }

        public static void ViewNotifications(Employer currentEmployer, List<Worker> allWorkers)
        {
            Console.Clear();
            if (currentEmployer.Notifications == null || currentEmployer.Notifications.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No pending applications.");
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            string title = "Job Applications Management";
            title.FullWidthLine();

            for (int i = currentEmployer.Notifications.Count - 1; i >= 0; i--)
            {
                var notification = currentEmployer.Notifications[i];
                Console.WriteLine($"\nApplication #{i + 1}");
                Console.WriteLine($"Candidate: {notification.WorkerName}");
                Console.WriteLine($"Vacancy:   {notification.VacancyName}");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("[A]ccept  [R]eject  [S]kip");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.S) continue;

                var worker = allWorkers.FirstOrDefault(w => w.Id == notification.WorkerId);

                if (worker != null)
                {
                    string status = "";
                    bool processed = false;

                    if (key == ConsoleKey.A)
                    {
                        status = "ACCEPTED! Congratulations.";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Worker Accepted.");
                        processed = true;
                    }
                    else if (key == ConsoleKey.R)
                    {
                        status = "Rejected.";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Worker Rejected.");
                        processed = true;
                    }

                    if (processed)
                    {
                        worker.UpdateApplicationStatus(notification.VacancyName ?? "", status);

                        currentEmployer.Notifications.RemoveAt(i);

                        Program.WriteJsonWorkers(allWorkers);
                        Program.WriteJsonEmployers(Program.ReadJsonEmployers());
                    }
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Error: Worker not found in database (might have been deleted).");
                }
            }


            var allEmployers = Program.ReadJsonEmployers();
            var empToUpdate = allEmployers.FirstOrDefault(e => e.Id == currentEmployer.Id);
            if (empToUpdate != null)
            {
                empToUpdate.Notifications = currentEmployer.Notifications;
                Program.WriteJsonEmployers(allEmployers);
            }

            Console.WriteLine("\nDone processing. Press any key...");
            Console.ReadKey(true);
        }

        public static void AddVacancy(Employer currentEmployer)
        {
            Console.Write("Enter vacancy name: ");
            string? vacancyName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(vacancyName))
            {
                currentEmployer.Vacancies ??= new List<string>();
                currentEmployer.Vacancies.Add(vacancyName);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Vacancy '{vacancyName}' added successfully!");
                Console.ResetColor();
                Thread.Sleep(1500);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vacancy name cannot be empty!");
                Console.ResetColor();
                Thread.Sleep(1500);
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

        public static List<Worker> ReadJsonWorkers()
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


        public static void WriteJsonEmployers(List<Employer> employers)
        {
            var jsonSerializer = new JsonSerializer();
            // StreamWriter by default creates/overwrites, solving the XML tail corruption issue
            using (var sw = new StreamWriter("employers.json"))
            using (var jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                jsonSerializer.Serialize(jw, employers);
            }
        }



        public static List<Employer> ReadJsonEmployers()
        {
            var serializer = new JsonSerializer();
            if (File.Exists("employers.json"))
            {
                using (var sr = new StreamReader("employers.json"))
                using (var jr = new JsonTextReader(sr))
                {
                    var employers = serializer.Deserialize<List<Employer>>(jr);
                    return employers ?? new List<Employer>();
                }
            }
            else
            {
                using (File.Create("employers.json")) { }
                return new List<Employer>();
            }
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
            #region DemoEmployersListForPreventingEmptyVacanciesIssue
            //List<Employer> employers = new List<Employer>
            //{
            //    new Employer(
            //        "Günay",
            //        "Mustafayeva",
            //        "Bakı",
            //        "+994501234567",
            //        45,
            //        new List<string> { "Software Developer", "DevOps Engineer", "QA Tester" }
            //    ),
            //    new Employer(
            //        "Tural",
            //        "Rzayev",
            //        "Gəncə",
            //        "+994552345678",
            //        38,
            //        new List<string> { "Accountant", "Financial Analyst", "Auditor" }
            //    ),
            //    new Employer(
            //        "Sevil",
            //        "Abdullayeva",
            //        "Sumqayıt",
            //        "+994703456789",
            //        42,
            //        new List<string> { "Marketing Manager", "Brand Manager", "Social Media Specialist" }
            //    ),
            //    new Employer(
            //        "Kamran",
            //        "Hüseynov",
            //        "Mingəçevir",
            //        "+994774567890",
            //        48,
            //        new List<string> { "Mechanical Engineer", "Production Manager", "Quality Control Specialist" }
            //    ),
            //    new Employer(
            //        "Nərgiz",
            //        "Məhərrəmova",
            //        "Şəki",
            //        "+994995678901",
            //        41,
            //        new List<string> { "Civil Engineer", "Project Manager", "Site Supervisor" }
            //    )
            //};
            //WriteJsonEmployers(employers);
            #endregion
            while (true)
            {

                List<Worker> workers = ReadJsonWorkers();
                List<Employer> employers = ReadJsonEmployers();
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
                    case ConsoleKey.D2:
                        {
                            Console.WriteLine("1.Sign in");
                            Console.WriteLine("2.Sign up");
                            Console.WriteLine("ESC for returning back");
                            var choiceEmployerSigning = Console.ReadKey(true);
                            if (choiceEmployerSigning.Key == ConsoleKey.D1)
                                try
                                {
                                    Menu.EmployerSignIn(employers);
                                }
                                catch (Exception ex)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(ex.Message);
                                    Console.ResetColor();
                                    Thread.Sleep(2200);
                                }
                            else if (choiceEmployerSigning.Key == ConsoleKey.D2)
                            {
                                Menu.EmployerSignUp(employers);
                            }
                            else if (choiceEmployerSigning.Key == ConsoleKey.Escape)
                            {
                                continue;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }

        }
    }
}
