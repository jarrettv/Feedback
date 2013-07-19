namespace Aaa.Common
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    public static class RandomData
    {
        private static readonly XDocument dataDocument;
        private static readonly Random random;

        static RandomData()
        {
            if (null == dataDocument)
            {
                using (Stream datafileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Aaa.Common.Data.xml"))
                {
                    if (datafileStream != null)
                    {
                        XmlTextReader xmlReader = new XmlTextReader(datafileStream);
                        dataDocument = XDocument.Load(xmlReader);
                    }
                }
            }
            random = new Random();
        }

        /// <summary>
        /// Random Number Generator
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <example>int myInt = GetRandomInt(5, 1000); // gives in random integer between 5 and 1000</example>
        public static int Int(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// Gets a random ssn with or without dashes.
        /// </summary>
        /// <param name="withDashes"></param>
        /// <returns></returns>
        public static string Ssn(bool withDashes)
        {
            string ssn = random.Next(111111111, 999999999).ToString();
            if (withDashes) return ssn.Substring(0, 3) + "-" + ssn.Substring(3, 2) + "-" + ssn.Substring(5, 4);
            return ssn;
        }

        /// <summary>
        /// Returns a string representing a common name prefix, e.g. Mr., Mrs., Dr., etc.
        /// </summary>
        public static string NamePrefix()
        {
            var randomPrefix = dataDocument.Descendants("data").Descendants("prefixes").Descendants("prefix").ToList().Random(random);
            return randomPrefix.Value;
        }

        /// <summary>
        /// Returns a string representing a persons last name, randomly selected from actual first names.
        /// </summary>
        public static string FirstName()
        {
            var randomName = dataDocument.Descendants("data").Descendants("names").Descendants("first").ToList().Random(random);
            return randomName.Value;
        }

        /// <summary>
        /// Returns a string representing a persons middle name, randomly selected from actual first names.
        /// </summary>
        public static string MiddleName(double nullRate)
        {
            if (nullRate > random.NextDouble()) return null;
            if (0.3 > random.NextDouble()) return LastName();
            return FirstName();
        }

        /// <summary>
        /// Returns a string representing a persons last name, randomly selected from actual last names.
        /// </summary>
        public static string MiddleNameOrInitial(double nullRate, double intialRate)
        {
            if (nullRate > random.NextDouble()) return null;
            if (intialRate > random.NextDouble()) return MiddleInitial(nullRate);
            return MiddleName(nullRate);
        }

        /// <summary>
        /// Returns a string representing a middle initial (no period).
        /// </summary>
        public static string MiddleInitial(double nullRate)
        {
            if (nullRate > random.NextDouble()) return null;
            return MiddleName(0).Substring(0, 1);
        }

        /// <summary>
        /// Returns a string representing a middle initial with a period.
        /// </summary>
        public static string MiddleInitial(double nullRate, double periodRate)
        {
            if (nullRate > random.NextDouble()) return null;
            string mi = MiddleName(0).Substring(0, 1);
            if (periodRate > random.NextDouble()) mi = mi + ".";
            return mi;
        }

        /// <summary>
        /// Returns a string representing a persons last name, randomly selected from actual last names.
        /// </summary>
        public static string AliasName(double nullRate)
        {
            if (nullRate > random.NextDouble()) return null;
            var randomName = dataDocument.Descendants("data").Descendants("names").Descendants("first").ToList().Random(random);
            return randomName.Value;
        }

        /// <summary>
        /// Returns a string representing a persons last name, randomly selected from actual last names.
        /// </summary>
        public static string LastName()
        {
            var randomName = dataDocument.Descendants("data").Descendants("names").Descendants("last").ToList().Random(random);
            return randomName.Value;
        }

        /// <summary>
        /// Returns a string representing a persons full name, randomly selected from actual first and last names
        /// </summary>
        public static string FullName()
        {
            return string.Format("{0} {1}", FirstName(), LastName());
        }

        /// <summary>
        /// Returns a string representing a common name suffix, e.g. III, Jr., M.D., etc.
        /// </summary>
        public static string NameSuffix()
        {
            var randomSuffix = dataDocument.Descendants("data").Descendants("suffixes").Descendants("suffix").ToList().Random(random);
            return randomSuffix.Value;
        }

        /// <summary>
        /// Returns a random company name based on a list of fake company names
        /// </summary>
        public static string CompanyName()
        {
            var randomName = dataDocument.Descendants("data").Descendants("companies").Descendants("company").Descendants("name").ToList().Random(random);
            return randomName.Value;
        }

        /// <summary>
        /// Returns a string representing a street address, e.g. 123 First Ave.
        /// </summary>
        public static string StreetAddress()
        {
            var randomStreet = dataDocument.Descendants("data").Descendants("addresses").Descendants("streetNames").Descendants("streetName").ToList().Random(random);
            var randomStreetSuffix = dataDocument.Descendants("data").Descendants("addresses").Descendants("streetSuffixes").Descendants("streetSuffix").ToList().Random(random);
            return string.Format("{0} {1} {2}", random.Next(1, 1999), randomStreet.Value, randomStreetSuffix.Value);
        }

        /// <summary>
        /// Returns a random name that could be a city
        /// </summary>
        public static string City()
        {
            var randomCity = dataDocument.Descendants("data").Descendants("cities").Descendants("city").Descendants("name").ToList().Random(random);
            return randomCity.Value;
        }

        /// <summary>
        /// Returns a real US/Canadian State name at random, e.g Texas
        /// </summary>
        public static string StateName()
        {
            var randomState = dataDocument.Descendants("data").Descendants("states").Descendants("state").Descendants("name").ToList().Random(random);
            return randomState.Value;
        }

        /// <summary>
        /// Returns a real US/Canadian State code at random, e.g TX
        /// </summary>
        public static string StateCode()
        {
            var randomState = dataDocument.Descendants("data").Descendants("states").Descendants("state").Descendants("code").ToList().Random(random);
            return randomState.Value;
        }

        /// <summary>
        /// Returns a Random 5 digits between 11111 and 99999 to use for a zip code
        /// </summary>
        /// <remarks>Unlikely to produce many real zipcodes that the postoffice would recognize</remarks>
        public static string ZipCode()
        {
            return random.Next(11111, 99999).ToString();
        }

        /// <summary>
        /// Returns a real country name at random
        /// </summary>
        public static string Country()
        {
            var randomCountry = dataDocument.Descendants("data").Descendants("countries").Descendants("country").Descendants("name").ToList().Random(random);
            return randomCountry.Value;
        }

        /// <summary>
        /// Returns a real looking email address
        /// </summary>
        public static string EmailAddress()
        {
            return EmailAddress(FirstName(), LastName());
        }

        public static string EmailAddress(string first, string last)
        {
            var randomDomain = dataDocument.Descendants("data").Descendants("domainNames").Descendants("domainName").ToList().Random(random);
            var randomDomainSuffix = dataDocument.Descendants("data").Descendants("domainNameSuffixes").Descendants("suffix").ToList().Random(random);
            return string.Format("{0}.{1}@{2}.{3}", first, last, randomDomain.Value, randomDomainSuffix.Value);
        }

        /// <summary>
        /// Returns a 10 digit phone number in the format (###) ###-####
        /// </summary>
        /// <remarks>Area codes are unlikely to be real</remarks>
        public static string Phone(double areaCode800Rate)
        {
            StringBuilder phone = new StringBuilder();
            int areaCode = (areaCode800Rate > random.NextDouble()) ? 800 : random.Next(100, 800);
            int divCode = random.Next(101, 1000);
            int number = random.Next(1, 10000);

            return String.Format("{0:(###) ###-####}", areaCode, divCode, number);
        }

        /// <summary>
        /// Returns a birthdate within the given age range.
        /// </summary>
        /// <param name="minAge">The inclusive mas age</param>
        /// <param name="maxAge">The exclusive max age.</param>
        /// <returns></returns>
        public static DateTime BirthDate(int minAge, int maxAge)
        {
            return DateTime.Today.AddYears(-random.Next(minAge, maxAge));
        }

        /// <summary>
        /// Gets a random date based on today.
        /// </summary>
        /// <param name="daysBeforeToday"></param>
        /// <param name="daysAfterToday"></param>
        /// <returns></returns>
        public static DateTime Date(int daysBeforeToday, int daysAfterToday)
        {
            return DateTime.Today.AddDays(random.Next(-daysBeforeToday, daysAfterToday));
        }

        /// <summary>
        /// Gets a random date based on now.
        /// </summary>
        /// <param name="hoursBeforeNow"></param>
        /// <param name="hoursAfterNow"></param>
        /// <returns></returns>
        public static DateTime DateAndTime(int hoursBeforeNow, int hoursAfterNow)
        {
            var min = DateTime.Now.AddHours(-hoursBeforeNow).Ticks / TimeSpan.TicksPerMinute;
            var max = DateTime.Now.AddHours(hoursAfterNow).Ticks / TimeSpan.TicksPerMinute;

            var minutes = random.Next((int)min, (int)max);

            return new DateTime(minutes * TimeSpan.TicksPerMinute);
        }
    }
}
