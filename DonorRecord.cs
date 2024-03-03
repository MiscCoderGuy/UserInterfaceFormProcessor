using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterfaceFormProcessor
{
    class DonorRecord
    {
        // Class Parameters

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string[] Donors { get; set; }

        public DonorRecord()
        {
            // Class Constructor
        }

        public DonorRecord(string firstName, string lastName, string address1, string address2, 
            string city, string state, int zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            ZipCode = zipCode;

            Donors.Append(FirstName + ',' + LastName + ',' + Address1 + ',' + Address2 + ',' + City + ',' +
                State + ',' + ZipCode);
        }
    }
}
