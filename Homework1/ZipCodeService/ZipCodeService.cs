using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using ZipCodeService.Properties;
using System.Diagnostics;

namespace ZipCodeService
{
    /// <summary>
    /// Implementation of the IZipCodeService interface.
    /// </summary>
    public class ZipCodeService : IZipCodeService
    {
        /// <summary>
        /// Looks us a zipcode for authorized users
        /// </summary>
        /// <param name="caller">The user; will verify caller is autorized</param>
        /// <param name="zipcode">The zipcode to lookup</param>
        /// <returns>City, state for specified zipcode or error description.</returns>
        public string Lookup(string caller, string zipcode)
        {
            string result;

            // check that caller is authorized
            if (IsAuthorized(caller))
            {
                // do lookup
                result = ZipCodes.ContainsKey(zipcode) ? ZipCodes[zipcode] : Resources.UNKOWN_ZIPCCODE_MSG;
            }
            else
            {
                // unauthorized user message
                result = Resources.UNAUTHORIZED_USER;
            }

            // log call
            LogResults(caller, zipcode, result);

            return result;
        }

        private bool IsAuthorized(string caller)
        {
            return Users.Contains(caller);
        }

        private void LogResults(string caller, string zipcode, string results)
        {
            string logFilePath = HostingEnvironment.MapPath(Settings.Default.LOGFILE);
            if (!string.IsNullOrEmpty(logFilePath))
            {
                StreamWriter sw = null;
                try
                {
                    // open log with text writer and write log entry
                    sw = new StreamWriter(logFilePath, true);
                    sw.WriteLine(string.Format(Resources.LOG_MESSAGE_FORMAT, caller, zipcode, results));
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw = null;
                    }
                }
            }
            else
            {
                // warn that log is not written, but do not prevent successful service operation.
                Trace.WriteLine(Resources.LOG_FILE_ERROR_MSG);
            }
        }

        private List<string> _users;

        private List<string> Users
        {
            get
            {
                // only load users when needed, lazy load
                if (_users == null)
                {
                    StreamReader sr = null;
                    try
                    {
                        string userFilePath = HostingEnvironment.MapPath(Settings.Default.USERFILE);
                        if (!string.IsNullOrEmpty(userFilePath))
                        {
                            _users = new List<string>();
                            // open users file and read with stream reader
                            sr = new StreamReader(userFilePath);
                            while (!sr.EndOfStream)
                            {
                                _users.Add(sr.ReadLine()); // add user to list
                            }
                        }
                        else
                        {
                            throw new ApplicationException(Resources.USER_FILE_ERROR_MSG);
                        }
                    }
                    finally
                    {
                        if (sr != null)
                        {
                            sr.Close(); // ensure reader is closed
                            sr = null;
                        }
                    }
                }
                return _users;
            }
        }

        private Dictionary<string, string> _zipcodes;

        private Dictionary<string, string> ZipCodes
        {
            get
            {
                // only load zip codes when needed, lazy load
                if (_zipcodes == null)
                {
                    StreamReader sr = null;
                    try
                    {
                        string zipcodeFilePath = HostingEnvironment.MapPath(Settings.Default.ZIPCODEFILE);
                        if (!string.IsNullOrEmpty(zipcodeFilePath))
                        {
                            _zipcodes = new Dictionary<string, string>();
                            // open zip code file and read with stream reader
                            sr = new StreamReader(zipcodeFilePath);
                            while (!sr.EndOfStream)
                            {
                                var line = sr.ReadLine();
                                if (line != null)
                                {
                                    var parts = line.Split('|'); // split string to zip code and city, state 
                                    _zipcodes.Add(parts[0], parts[1]); // add pair to dictionary
                                }
                            }
                        }
                        else
                        {
                            throw new ApplicationException(Resources.ZIPCODE_FILE_ERROR_MSG);
                        }
                    }
                    finally
                    {
                        if (sr != null)
                        {
                            sr.Close(); // ensure reader is closed
                            sr = null;
                        }
                    }
                }
                return _zipcodes;
            }
        }
    }
}