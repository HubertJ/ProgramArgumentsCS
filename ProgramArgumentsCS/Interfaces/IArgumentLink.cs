using System.Collections.Generic;
using ProgramArgumentsCS.Errors;

namespace ProgramArgumentsCS.Interfaces
{
  public interface IArgumentLink
  {
    bool Handle(string[] args);

    IEnumerable<Error> Errors { get; }
  }
}
