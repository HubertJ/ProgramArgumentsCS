
using ProgramArgumentsCS.Arguments;
namespace ProgramArgumentsCS.Errors
{
  /// <summary>
  /// Parser error information 
  /// </summary>
  public struct Error
  {
    /// <summary>
    /// The severity of the error
    /// </summary>
    public Severity Severity
    {
      get;
      set;
    }

    /// <summary>
    /// The user friendly message
    /// </summary>
    public string Message
    {
      get;
      set;
    }

    public Argument Argument
    {
      get;
      set;
    }
  }
}
