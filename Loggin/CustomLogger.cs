using System.Collections.Concurrent;

namespace ApiChikPet.Loggin
{

  public class CustomLogger : ILogger
  {
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;
    public static object _locked = new object();

    public CustomLogger(string name, CustomLoggerProviderConfiguration config)
    {
      loggerName = name;
      loggerConfig = config;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
      return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception exception, Func<TState, Exception, string> formatter)
    {
      string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

      EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
      string caminhoArquivoLog = @"Log.txt";
      lock (_locked)
      {

        using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))

        {
          try
          {
            streamWriter.WriteLine(mensagem);
            streamWriter.Close();
          }
          catch (Exception)
          {
            throw;
          }
        }
      }
    }
  }
}