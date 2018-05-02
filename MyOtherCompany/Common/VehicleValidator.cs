using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Common;

namespace MyOtherCompany.Common
{
    public class VehicleValidator
    {
        public const int RegistrationNumberMaxLenght = 10;

        /// <summary>
        /// Validates a registration number.
        /// </summary>
        /// <param name="registrationNumber">The registrationnumber to validate</param>
        /// <param name="errorMessages">out parmeter with an array of errormessages if any</param>
        /// <returns>bool valid, out errorMessages</returns>
        public static bool ValidRegistrationNumber(string registrationNumber, out string[] errorMessages)
        {
            bool valid = true;
            List<string> errorMsg = new List<String>();
            if (string.IsNullOrEmpty(registrationNumber))
            {
                valid = false;
                errorMsg.Add("The registration number can not be empty.");
                errorMessages = errorMsg.ToArray();
                return valid;
            }
            if (registrationNumber.Length > RegistrationNumberMaxLenght)
            {
                valid = false;
                errorMsg.Add("The registration number can not be longer than " + RegistrationNumberMaxLenght + " characters.");
            }
            // Only accept A-Z 0-9
            // Do not use for performace reasons
            /* if (!Regex.IsMatch(registrationNumber, @"^[A-Z0-9]*$"))
             {
                 valid = false;
                 errorMsg.Add("The registration number can only contain A-Z 0-9.");
             }
             */
            if (!registrationNumber.WhiteList("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"))
            {
                valid = false;
                errorMsg.Add("The registration number can only contain A-Z 0-9.");
            }
            string[] blackList = new string[] { "BAD", "FORBIDDEN", "ILLEGAL", "UNALLOWED" };
            if (registrationNumber.BlackList(blackList))
            {
                valid = false;
                errorMsg.Add("The registration number can not contain any forbidden word.");
            }
            errorMessages = errorMsg.ToArray();
            return valid;
        }
    }
}
