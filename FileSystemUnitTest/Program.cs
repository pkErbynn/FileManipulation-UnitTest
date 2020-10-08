using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.Abstractions;

namespace FileSystemUnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                FileDealer app = serviceProvider.GetService<FileDealer>();
                app.FileProcessor();
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.  
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FileDealer>();
            services.AddScoped<IFileSystem, FileSystem>();
        }
    }

    public class FileDealer
    {
        private readonly IFileSystem _fileSytem;
        public FileDealer (IFileSystem fileSystem)
        {
            _fileSytem = fileSystem;
        }
        public string FileProcessor()
        {
            String filePath = @"C:\Users\Mr. Erbynn\Desktop\ut\test\file.txt";
            Console.WriteLine("file path: " + filePath);
            String folderPath = Path.GetDirectoryName(filePath);
            Console.WriteLine("folder path: " + folderPath);

            if (!_fileSytem.Directory.Exists(folderPath))
                _fileSytem.Directory.CreateDirectory(folderPath);

            if (_fileSytem.File.Exists(filePath))
                _fileSytem.File.Delete(filePath);

            _fileSytem.File.WriteAllText(filePath, "Hello from John Erbynn");
            var content = _fileSytem.File.ReadAllText(filePath);

            Console.WriteLine(content);
            return content;
        }
    }
}
