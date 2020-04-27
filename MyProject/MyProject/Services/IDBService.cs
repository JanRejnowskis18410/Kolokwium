using MyProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IDBService
    {
        GetMedicationResponse GetMedicament(int id);

        // nie udało się zaimplementować, za mało czasu
        //public bool DeletePatient(int id);
    }
}
