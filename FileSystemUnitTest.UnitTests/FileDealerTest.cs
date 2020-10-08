using AutoFixture;
using Moq;
using System;
using System.IO.Abstractions;
using Xunit;
using FileSystemDemo;

namespace FileSystemDemo.UnitTests
{
    public class FileDealerTest
    {
        public readonly Mock<IFileSystem> _fileSystem;
        public readonly Fixture _fixture;

        public FileDealerTest()
        {
            _fixture = new Fixture();
            _fileSystem = new Mock<IFileSystem>();

            _fileSystem.Setup(f => f.Directory.CreateDirectory(It.IsAny<String>())).Verifiable();
            _fileSystem.Setup(f => f.File.Delete(It.IsAny<String>())).Verifiable();
            _fileSystem.Setup(f => f.File.WriteAllText(It.IsAny<String>(), It.IsAny<String>())).Verifiable();
            _fileSystem.Setup(f => f.File.ReadAllText(It.IsAny<String>())).Returns(_fixture.Create<String>());
        }

        [Fact]
        public void FileDealer_WhenFolderDoesNotExists_ReturnsContents()
        {
            _fileSystem.Setup(f => f.Directory.Exists(It.IsAny<String>())).Returns(false);
            var fileDealer = new FileDealer(_fileSystem.Object);
            var result = fileDealer.FileProcessor();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<String>(result);

            _fileSystem.Verify(f => f.Directory.CreateDirectory(It.IsAny<String>()), Times.Once);
            _fileSystem.Verify(f => f.File.Delete(It.IsAny<String>()), Times.Never);
            _fileSystem.Verify(f => f.File.WriteAllText(It.IsAny<String>(), It.IsAny<String>()), Times.Once);
            _fileSystem.Verify(f => f.File.ReadAllText(It.IsAny<String>()), Times.Once);
        }

        [Fact]
        public void FileDealer_WhenFolderExists_FileDoesnNotExists_ReturnsContents()
        {
            _fileSystem.Setup(f => f.Directory.Exists(It.IsAny<String>())).Returns(true);
            _fileSystem.Setup(f => f.File.Exists(It.IsAny<String>())).Returns(false);
            var fileDealer = new FileDealer(_fileSystem.Object);
            var result = fileDealer.FileProcessor();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<String>(result);

            _fileSystem.Verify(f => f.Directory.CreateDirectory(It.IsAny<String>()), Times.Never);
            _fileSystem.Verify(f => f.File.Delete(It.IsAny<String>()), Times.Never);
            _fileSystem.Verify(f => f.File.WriteAllText(It.IsAny<String>(), It.IsAny<String>()), Times.Once);
            _fileSystem.Verify(f => f.File.ReadAllText(It.IsAny<String>()), Times.Once);
        }

        [Fact]
        public void FileDealer_WhenFolderExists_FileExists_ReturnsContents()
        {
            _fileSystem.Setup(f => f.Directory.Exists(It.IsAny<String>())).Returns(true);
            _fileSystem.Setup(f => f.File.Exists(It.IsAny<String>())).Returns(true);
            var fileDealer = new FileDealer(_fileSystem.Object);
            var result = fileDealer.FileProcessor();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<String>(result);

            _fileSystem.Verify(f => f.Directory.CreateDirectory(It.IsAny<String>()), Times.Never);
            _fileSystem.Verify(f => f.File.Delete(It.IsAny<String>()), Times.Once);
            _fileSystem.Verify(f => f.File.WriteAllText(It.IsAny<String>(), It.IsAny<String>()), Times.Once);
            _fileSystem.Verify(f => f.File.ReadAllText(It.IsAny<String>()), Times.Once);
        }

    }
}
