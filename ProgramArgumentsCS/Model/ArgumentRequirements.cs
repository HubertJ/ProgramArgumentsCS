
namespace ProgramArgumentsCS.Model
{
  /// <summary>
  /// Different requirements for command line arguments
  /// </summary>
  public enum ArgumentRequirements
  {
    /// <summary>
    /// Specifies an argument that is mandatory at all times
    /// </summary>
    Mandatory,

    /// <summary>
    /// Specifies an argument that is optional at all times
    /// </summary>
    Optional

    /// <summary>
    /// Specifies an argument that is conditional on the precense of other arguments
    /// </summary>
    //Conditional
  }
}
