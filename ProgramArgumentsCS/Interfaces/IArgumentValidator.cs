
using System.Collections.Generic;
using ProgramArgumentsCS.Model;

namespace ProgramArgumentsCS.Interfaces
{
  public interface IArgumentValidator
  {
    void Validate(IEnumerable<Verb> verbs, IEnumerable<IArgumentLink> arguments);
  }
}
