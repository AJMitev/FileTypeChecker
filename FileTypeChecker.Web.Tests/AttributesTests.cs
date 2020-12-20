namespace FileTypeChecker.Web.Tests
{
    using FileTypeChecker;
    using FileTypeChecker.Web.Attributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;

    [TestFixture]
    public class AttributesTests
    {
        [Test]
        [TestCase("test.bmp", new[] { FileExtension.Bitmap }, true)]
        [TestCase("test.bmp", new[] { FileExtension.Jpg }, false)]
        [TestCase("test.jpg", new[] { FileExtension.Jpg }, true)]
        [TestCase("test.jpg", new[] { FileExtension.Bitmap, FileExtension.Jpg, FileExtension.Gif }, true)]
        [TestCase("test.jpg", new[] { FileExtension.Rar }, false)]
        [TestCase("test.zip", new[] { FileExtension.Zip }, true)]
        public void AllowedTypeAttribute_ShouldAllowOnlyPointedTypes(string fileName, string[] allowedExtensions, bool expectedResult)
        {
            var stream = FileHelpers.ReadFile(fileName);

            var attributeToTest = new AllowedTypesAttribute(allowedExtensions);

            Assert.AreEqual(expectedResult, attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.zip", null)]
        [TestCase("test.zip", new string[] { })]
        public void AllowTypesAttribute_ShouldThrowExceptionIfNoExtensionsProvided(string fileName, string[] allowedExtensions)
        {
            var stream = FileHelpers.ReadFile(fileName);

            var attributeToTest = new AllowedTypesAttribute(allowedExtensions);

            Assert.Throws<InvalidOperationException>(() => attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.zip", new[] { FileExtension.Zip }, false)]
        [TestCase("test.png", new[] { FileExtension.Zip }, true)]
        [TestCase("test.png", new[] { FileExtension.Zip, FileExtension.Xar, FileExtension.Jpg }, true)]
        [TestCase("test.png", new[] { FileExtension.Zip, FileExtension.Png, FileExtension.Jpg }, false)]
        public void ForbidTypesAttribute_ShouldForbidPointedTypes(string fileName, string[] forbidedExtensions, bool expectedResult)
        {
            var stream = FileHelpers.ReadFile(fileName);

            var attributeToTest = new ForbidTypesAttribute(forbidedExtensions);

            Assert.AreEqual(expectedResult, attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.zip", null)]
        [TestCase("test.zip", new string[] { })]
        public void ForbidTypesAttribute_ShouldThrowExceptionIfNoExtensionsProvided(string fileName, string[] allowedExtensions)
        {
            var stream = FileHelpers.ReadFile(fileName);

            var attributeToTest = new ForbidTypesAttribute(allowedExtensions);

            Assert.Throws<InvalidOperationException>(() => attributeToTest.IsValid(stream));
        }


        [Test]
        [TestCase("test.png", false)]
        [TestCase("test.jpg", false)]
        [TestCase("test.bmp", false)]
        [TestCase("test.zip", true)]
        [TestCase("test.7z", true)]
        [TestCase("test.bz2", true)]
        [TestCase("test.gz", true)]
        public void OnlyArchiveAttribute_ShouldReturnValidateIfTheFileIsArchive(string fileName, bool expectedResult)
        {
            var stream = FileHelpers.ReadFile(fileName);

            var attributeToTest = new AllowArchiveOnlyAttribute();

            Assert.AreEqual(expectedResult, attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.png", true)]
        [TestCase("test.jpg", true)]
        [TestCase("test.bmp", true)]
        [TestCase("test.zip", false)]
        public void OnlyImageAttribute_ShouldValidateIfTheFileIsImage(string fileName, bool expectedResult)
        {
            var stream = FileHelpers.ReadFile(fileName);

            var attributeToTest = new AllowImageOnlyAttribute();

            Assert.AreEqual(expectedResult, attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.exe")]
        public void ForbidExecutableFilesAttribute_ShouldReturnInvalidIfTheFileIsExecutable(string fileName)
        {
            var stream = FileHelpers.ReadFile(fileName);

            var attributeToTest = new ForbidExecutableFileAttribute();

            Assert.IsFalse(attributeToTest.IsValid(stream));
        }

        [Test]
        public void OnlyImageAttribute_ShouldValidateMultupleFilesIfTheyAreImages()
        {
            var fileNames = new[] { "test.png", "test.jpg", "test.bmp" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attrebuteToTest = new AllowImageOnlyAttribute();

            Assert.IsTrue(attrebuteToTest.IsValid(files));
        }

        [Test]
        public void OnlyImageAttribute_ShouldReturnReturnFalseIfOneOfFilesIsNotImage()
        {
            var fileNames = new[] { "test.png", "test.jpg", "test.bmp", "test.exe" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attrebuteToTest = new AllowImageOnlyAttribute();

            Assert.IsFalse(attrebuteToTest.IsValid(files));
        }

        [Test]
        public void AllowArchiveOnlyAttribute_ShouldCanValidateMultipleFiles()
        {
            var fileNames = new[] { "test.zip", "test.7z", "test.bz2", "test.gz" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attributeToTest = new AllowArchiveOnlyAttribute();
            Assert.IsTrue(attributeToTest.IsValid(files));
        }

        [Test]
        public void AllowArchiveOnlyAttribute_ShouldReturnTrueIfInputIsNull()
        {
            var attributeToTest = new AllowArchiveOnlyAttribute();
            Assert.IsTrue(attributeToTest.IsValid(null));
        }

        [Test]
        public void AllowArchiveOnlyAttribute_ShouldReturnFalseIfOneOfTheFilesIsNotArchive()
        {
            var fileNames = new[] { "test.zip", "test.7z", "test.jpg", "test.gz" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attributeToTest = new AllowArchiveOnlyAttribute();
            Assert.IsFalse(attributeToTest.IsValid(files));
        }

        [Test]
        public void AllowTypesAttribute_ShouldValidateMultipleFiles()
        {
            var fileNames = new[] { "test.zip", "test.7z", "test.gz" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attributeToTest = new AllowedTypesAttribute(FileExtension.Zip, FileExtension.SevenZ, FileExtension.Gz);
            Assert.IsTrue(attributeToTest.IsValid(files));
        }

        [Test]
        public void AllowTypesAttribute_ShouldReturnFalseIfOneOfTheFilesIsNotAllowed()
        {
            var fileNames = new[] { "test.zip", "test.7z", "test.gz" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attributeToTest = new AllowedTypesAttribute(FileExtension.Zip, FileExtension.SevenZ);
            Assert.IsFalse(attributeToTest.IsValid(files));
        }

        [Test]
        public void ForbidExecutableAttribute_ShouldValidateMultipleFiles()
        {
            var fileNames = new[] { "test.zip", "test.7z", "test.jpg", "test.gz" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attributeToTest = new ForbidExecutableFileAttribute();
            Assert.IsTrue(attributeToTest.IsValid(files));
        }

        [Test]
        public void ForbidExecutableAttribute_ShouldReturnFalseIfOneOfFilesIsExecutable()
        {
            var fileNames = new[] { "test.zip", "test.7z", "test.exe", "test.gz" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attributeToTest = new ForbidExecutableFileAttribute();
            Assert.IsFalse(attributeToTest.IsValid(files));
        }

        [Test]
        public void ForbidTypesAttribute_ShouldValidateMultipleFiles()
        {
            var fileNames = new[] { "test.zip", "test.7z", "test.gz" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attributeToTest = new ForbidTypesAttribute(FileExtension.Jpg);
            Assert.IsTrue(attributeToTest.IsValid(files));
        }

        [Test]
        public void ForbidTypesAttribute_ShouldReturnFalseIfOneOfFilesIsForbiden()
        {
            var fileNames = new[] { "test.zip", "test.jpg", "test.png" };
            var files = FileHelpers.ReadFiles(fileNames);

            var attributeToTest = new ForbidTypesAttribute(FileExtension.Zip);
            Assert.IsFalse(attributeToTest.IsValid(files));
        }
    }
}
