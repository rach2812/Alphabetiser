using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

/** An application to sort names alphabetically from a txt file.
 * 
 * Rachel Quilligan, February 2018
 * 
 * */
namespace NameSorter
{
    public class NameSorter
    {
        //A class to easily access first and last names to prioritise
        //alphabetical ordering.
        public class Person
        {
            private string[] first;

            public Person(string name)
            {
                try
                {
                    if (name != null)
                    {
                        string[] full = name.Split(' ');
                        try
                        {
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
                        catch (ArgumentException e)
                        {
                            System.Console.WriteLine(e.ToString());
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

            public string FirstName { get; }

            public string LastName { get; }

            public override string ToString()
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        //The method that sorts Person objects alphabetically.
        public static List<string> Alphabetise(List<Person> names)
        {
            try
            {
                if (names.Count == 0)
                {
                    throw new ArgumentException("No names to sort.");
                }
                else
                {
                    IEnumerable<Person> alphaNames = names.OrderBy(person => person.LastName).ThenBy(person => person.FirstName);
                    List<string> final = new List<string>();
                    foreach (Person person in alphaNames)
                    {
                        final.Add(person.ToString());
                    }
                    return final;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static void Main(string[] args)
        {
            if (args.Any())
            {
                var path = args[0].ToString();
                if (File.Exists(path))
                {
                    using (StreamReader file = File.OpenText(path))
                    {
                        string line;
                        List<Person> names = new List<Person>();
                        while ((line = file.ReadLine()) != null)
                        {
                            //This ensures weird capitals don't mess
                            //with the alphabetical order.
                            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                            line = textInfo.ToTitleCase(line.ToLower());
                            names.Add(new Person(line));
                        }

                        List<string> final = Alphabetise(names);

                        foreach (string s in final) 
                        {
                            Console.WriteLine(s);
                        }
                        string finalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sorted-names-list.txt");
                        File.WriteAllLines(finalPath, final);
                    }
                }
                else
                {
                    Console.Write("Please enter a valid file name.");
                }
            }
            else 
            {
                Console.Write("Please enter a file to be sorted.");
            }
        }
    }
}