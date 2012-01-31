using System;

namespace ZipCodeService
{
    public class ZipCodeService : IZipCodeService
    {
        private const string UnauthorizedUser = "Unauthorizeduser";
        private const string UnknownZipCode = "Unknown zip code";

        public string Lookup(string caller, string zipcode)
        {
            string result = null;
            if (IsAuthorized(caller))
            {
                // do lookup

                result = UnknownZipCode;


            }
            else
            {
                result = UnauthorizedUser;
            }

            LogResults(result);

            return result;
        }


        private bool IsAuthorized(string caller)
        {
            return true;
        }

        private void LogResults(string results)
        {
            // log it somehow
        }
    }
}
