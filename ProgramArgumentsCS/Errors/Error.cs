
using ProgramArgumentsCS.Model;

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

    /// <summary>
    /// The argument that resulted in this error if valid
    /// </summary>
    public Argument Argument
    {
      get;
      set;
    }
  }
}
