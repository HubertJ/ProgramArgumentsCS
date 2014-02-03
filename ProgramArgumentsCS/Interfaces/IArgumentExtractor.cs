using System.Collections.Generic;
using ProgramArgumentsCS.ArgumentLinks;
using ProgramArgumentsCS.Errors;

namespace ProgramArgumentsCS.Interfaces
{
  public interface IArgumentExtractor
  {
    /// <summary>
    /// Method to get the arguments extracted from the supplied type
    /// </summary>
    /// <returns>The list of arguments</returns>
    IEnumerable<IArgumentLink> GetArguments();

    /// <summary>
    /// The errors found when handling this argument link
    /// </summary>
    IEnumerable<Error> Errors { get; }
  }
}
