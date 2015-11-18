using DentalClinic.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Services.Interfaces
{
    public interface ILeaveService
    {
        IEnumerable<LeaveToDisplay> GetAll();
    }
}
