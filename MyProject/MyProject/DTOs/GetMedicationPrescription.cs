using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DTOs
{
    public class GetMedicationPrescription
    {
        public int IdPerscription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
    }
}
