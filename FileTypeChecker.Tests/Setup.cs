using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileTypeChecker.Tests
{
    [SetUpFixture]
    public class Setup
    {

        [OneTimeSetUp]
        public void SetUp()
        {
            FileTypeValidator.RegisterCustomTypes(Assembly.GetAssembly(typeof(FileTypeValidatorTests)));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
        }


    }
}
