namespace Final.NetCore
{
    class Worker
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
    }
    class Employer
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
    }

    class CV
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
                if (value!=null && (value.ContainsValue("A1")  || value.ContainsValue("A2") ||
                   value.ContainsValue("B1") || value.ContainsValue("B2") ||
                   value.ContainsValue("C1") || value.ContainsValue("C2")))
                    _languagesLevel = value;
                
            }
        }

        public bool HasDistinctionDiploma { get; set; }


    }
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
