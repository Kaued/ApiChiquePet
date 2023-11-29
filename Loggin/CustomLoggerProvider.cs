using System.Collections.Concurrent;

namespace ApiChikPet.Loggin
{

  public class CustomLoggerProvider : ILoggerProvider
  {

    readonly CustomLoggerProviderConfiguration loggerConfiguration;
    readonly ConcurrentDictionary<string, CustomLogger> loggers = new ConcurrentDictionary<string, CustomLogger>();

    
    public CustomLoggerProvider(CustomLoggerProviderConfiguration config){
      loggerConfiguration=config;
    }
    public ILogger CreateLogger(string categoryName)
    {
      return loggers.GetOrAdd(categoryName, name=> new CustomLogger(name, loggerConfiguration));
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}