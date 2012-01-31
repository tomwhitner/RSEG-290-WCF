using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace ZipCodeService
{
    public class ZipCodeService : IZipCodeService
    {
        private const string UnauthorizedUser = "Unauthorized user";
        private const string UnknownZipCode = "Unknown zip code";

        public string Lookup(string caller, string zipcode)
        {
            string result;
            if (IsAuthorized(caller))
            {
                // do lookup
                result = ZipCodes.ContainsKey(zipcode) ? ZipCodes[zipcode] : UnknownZipCode;
            }
            else
            {
                result = UnauthorizedUser;
            }

            LogResults(caller, zipcode, result);

            return result;
        }


        private bool IsAuthorized(string caller)
        {
            return Users.Contains(caller);
        }

        private void LogResults(string caller, string zipcode, string results)
        {
            var tw = new StreamWriter(HostingEnvironment.MapPath("~/log.txt"), true);
            tw.WriteLine(string.Format("Caller: {0}, ZipCode: {1}, Results: {2}", caller, zipcode, results));
            tw.Close();
        }

        private List<string> _users;

        private List<string> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new List<string>();
                    var tr = new StreamReader(HostingEnvironment.MapPath("~/users.txt"));
                    while (!tr.EndOfStream)
                    {
                        _users.Add(tr.ReadLine());
                    }
                    tr.Close();
                }
                return _users;
            }
        }

        private Dictionary<string, string> _zipcodes;

        private Dictionary<string, string> ZipCodes
        {
            get
            {
                if (_zipcodes == null)
                {
                    _zipcodes = new Dictionary<string, string>();
                    var tr = new StreamReader(HostingEnvironment.MapPath("~/zipcodes.txt"));
                    while (!tr.EndOfStream)
                    {
                        var line = tr.ReadLine();
                        if (line != null)
                        {
                            var parts = line.Split('|');
                            _zipcodes.Add(parts[0], parts[1]);
                        }
                    }
                    tr.Close();
                }
                return _zipcodes;
            }
        }
    }
}