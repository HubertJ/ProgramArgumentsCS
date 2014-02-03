using System;
using ProgramArgumentsCS.Interfaces;

namespace ProgramArgumentsCS.Errors.Commands
{
  public class ConsolePrintCommand : IErrorCommand
  {
    #region IErrorCommand Members

    public void Handle(Error error)
    {
      OutputSeverity(error.Severity);

      Console.WriteLine(error.Message);      
    }

    #endregion

    #region Implementation

    private void OutputSeverity(Severity severity)
    {
      Console.ResetColor();
      switch (severity)
      {
        case Severity.Critical:
          Console.ForegroundColor = ConsoleColor.Red;
          break;

        case Severity.Warning:
          Console.ForegroundColor = ConsoleColor.DarkYellow;
          break;

        case Severity.Information:
          Console.ForegroundColor = ConsoleColor.Cyan;
          break;

        default:
          throw new ArgumentException("Invalid serverity provided");
          //break;
      }

      Console.Write("{0}", severity.ToString());
      Console.ResetColor();
      Console.Write(" : ");
    }

    #endregion
  }
}
