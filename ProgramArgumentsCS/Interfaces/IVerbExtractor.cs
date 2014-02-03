using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramArgumentsCS.Errors;
using ProgramArgumentsCS.Model;

namespace ProgramArgumentsCS.Interfaces
{
  public interface IVerbExtractor
  {
    /// <summary>
    /// Method to get the arguments extracted from the supplied type
    /// </summary>
    /// <returns>The list of arguments</returns>
    IEnumerable<Verb> GetVerbs();

    /// <summary>
    /// The errors found when handling this argument link
    /// </summary>
    IEnumerable<Error> Errors { get; }
  }
}
