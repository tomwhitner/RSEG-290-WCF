Homework #1 - Tom Whitner

The ZipCodeService accepts a caller (string) and zipcode (string) and returns:
- Unauthorized user, if the caller is not authorized
- Unknown Zipcode, if the zipcode is not in the "database"
- The City, State associated with the zipcode otherwise

In addition, it attempts to log all calls made to it.

The list of authorized users is stored in the users.txt file.
The zipcode "database" is stored in the sipcodes.txt file.
The log is written to the log.txt file.
Each of these files resides in the main host directory (e.g. same location as web.config).
