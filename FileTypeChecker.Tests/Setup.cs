using NUnit.Framework;
using System.Reflection;

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