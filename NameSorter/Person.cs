using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace NameSorter
{
    public class Person
    {
        string[] first;

        public Person(string name)
        {
            try
            {
                if (name != null)
                {
                    string[] full = name.Split(' ');
                    if (full.Length >= 2)
                    {
                        LastName = full.Last();
                        first = full.Take(full.Length - 1).ToArray();
                        FirstName = String.Join(" ", first);
                    }
                    else
                    {
                        throw new ArgumentException("Must have at least first and last name", name);
                    }
                }
                else
                {
                    throw new ArgumentException("Parameter cannot be null", nameof(name));
                }
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}
