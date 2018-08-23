using Microsoft.Extensions.Logging;

namespace TrackerServer
{
    public static class FileLoggerExtention
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            
            return factory;
        }
    }
}