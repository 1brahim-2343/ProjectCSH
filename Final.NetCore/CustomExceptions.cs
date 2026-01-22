using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.NetCore
{
    public class WorkerNotFoundException : Exception
    {
        public WorkerNotFoundException(string phoneNo) : base($"Worker with phone number {phoneNo} was not found, make sure that there is no typo")
        {
        }
    }
    public class EmployerNotFoundException : Exception
    {
        public EmployerNotFoundException(string phoneNo) : base($"Employer with phone number {phoneNo} was not found, make sure that there is no typo")
        {
        }
    }

    public class TimingException : Exception
    {
        public TimingException(DateTime startDate, DateTime endDate) : base($"Previous job end date ({endDate.ToString("d")}) can not be earlier than career start date ({startDate.ToString("d")})")
        {
        }
    }
    public class InvalidPhoneNumber : Exception
    {
        public InvalidPhoneNumber(string phoneNo) : base($"{phoneNo} Is not valid Azerbaijani phone number")
        {
        }
    }

    public class EmptyList<T> : Exception where T : class, new()
    {

        public EmptyList(List<T> list) : base($"{nameof(list)} is empty")
        {
        }
    }

}
