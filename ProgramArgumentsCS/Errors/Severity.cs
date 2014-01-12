
namespace ProgramArgumentsCS.Errors
{
  /// <summary>
  /// The severity of the error
  /// </summary>
  public enum Severity
  {
    /// <summary>
    /// Critical errors such as missing mandatory arguments
    /// </summary>
    Critical = 1,

    /// <summary>
    /// Warnings such as duplicate arguments specified
    /// </summary>
    Warning = 2,

    /// <summary>
    /// Information such as deprecated command line arguments
    /// </summary>
    Information = 4
  }
}
