using System;
using Xunit;
using Xunit.Sdk;
using NameSorter;
using System.Collections.Generic;

namespace NameSorterTests
{
    public class NameSorterTest
    {
        [Fact]
        public void TestNormalInputTwoNames()
        {
            NameSorter.NameSorter.Person test1 = new NameSorter.NameSorter.Person("Abby Johnson");
            Assert.Equal("Abby", test1.FirstName);
            Assert.Equal("Johnson", test1.LastName);
            Assert.Equal("Abby Johnson", test1.ToString());
        }

        [Fact]
        public void TestNormalInputThreeNames()
        {
            NameSorter.NameSorter.Person test2 = new NameSorter.NameSorter.Person("Claire Sally Brown");
            Assert.Equal("Claire Sally", test2.FirstName);
            Assert.Equal("Brown", test2.LastName);
            Assert.Equal("Claire Sally Brown", test2.ToString());
        }

        //This doesn't show the exception raised, despite it logging on Console
        //when doing system test.
        [Fact]
        public void TestAbnormalInputOneName()
        {
            var exception = Record.Exception(() => new NameSorter.NameSorter.Person("Lucy"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        //This one also doesn't show, as above
        [Fact]
        public void TestAbnormalInputEmpyString()
        {
            Action emptyStringTest = () => { NameSorter.NameSorter.Person test = new NameSorter.NameSorter.Person(""); };
            var ex = Assert.Throws<ArgumentException>(emptyStringTest);
            Assert.NotNull(ex);
            //Commented out below is another attempt at making the test work!
            /**
            Exception ex = Assert.Throws<ArgumentException>(() => new NameSorter.NameSorter.Person(""));
            Assert.Equal("Must have at least first and last name", ex.Message);
            */
        }

        //This one doesn't work either :(
        [Fact]
        public void TestAbnormalInputNull()
        {
            var exception = Record.Exception(() => new NameSorter.NameSorter.Person(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        //Also ran system tests which ensured capitals don't affect this
        //function.
        [Fact]
        public void TestAlphabetiserNormalInput()
        {

            List<NameSorter.NameSorter.Person> testList = new List<NameSorter.NameSorter.Person>()
            {
                new NameSorter.NameSorter.Person("Sally Jones"),
                new NameSorter.NameSorter.Person("William Brown"),
                new NameSorter.NameSorter.Person("Red Baker"),
                new NameSorter.NameSorter.Person("Thomas Brian Hardy")
            };
            List<string> final = NameSorter.NameSorter.Alphabetise(testList);

            string check = string.Join(", ", final.ToArray());
            Assert.Equal("Red Baker, William Brown, Thomas Brian Hardy, Sally Jones", check);
        }

        //Still doesn't detect thrown exception.
        [Fact]
        public void TestAlphabetiserNoNames()
        {
            List<NameSorter.NameSorter.Person> testList = new List<NameSorter.NameSorter.Person>();
            Action zeroTestList = () => { NameSorter.NameSorter.Alphabetise(testList); };
            var ex = Assert.Throws<ArgumentException>(zeroTestList);
            Assert.NotNull(ex);
        }
    }

}
