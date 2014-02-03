using ProgramArgumentsCS.Attributes;
using ProgramArgumentsCS.Model;

namespace ProgramArgumentsCS.Interfaces
{
  public interface IArgumentParserVisitor
  {
    void VisitProgramDetails(ProgramDetails details);

    void VisitVerbs(Verb verbs);

    void VisitArguments(Argument argument);
  }
}
