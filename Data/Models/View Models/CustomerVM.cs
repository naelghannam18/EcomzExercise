using EcomzExercise.Data.Models;
using System.Collections.Generic;
using System;

namespace EcomzExercise.Models.View_Models
{
    public class CustomerVM
    {
        public class AddCustomerVM
        {
            public string CustomerFirstName { get; set; }
            public string CustomerLastName { get; set; }
            public string CustomerEmail { get; set; }
            public DateTime CustomerDob { get; set; } = DateTime.Now;
            public string CustomerGender { get; set; }
            public string CustomerPassword { get; set; }
        }

        public class UpdateCustomerVM
        {
            public string CustomerFirstName { get; set; }
            public string CustomerLastName { get; set; }
            public string CustomerEmail { get; set; }
            public DateTime CustomerDob { get; set; } = DateTime.Now;
            public string CustomerGender { get; set; }
            public string CustomerPassword { get; set; }

        }

        public class ListCustomersVM
        {
            public string CustomerFirstName { get; set; }
            public string CustomerLastName { get; set; }
            public string CustomerEmail { get; set; }
            public DateTime CustomerDob { get; set; } = DateTime.Now;
            public string CustomerGender { get; set; }
            public decimal CustomerPoints { get; set; } = 0;

        }

        public class AddCustomerPointsVM
        {
            public string Email { get; set; }
            public decimal Points { get; set; }
        }

        public class ListCuponVM
        {
            public DateTime CuponDateIssued { get; set; }
            public DateTime CuponDateExpiry { get; set; }
            public string CuponCode { get; set; }
            public int CuponDiscount { get; set; }
        }


    }
}
