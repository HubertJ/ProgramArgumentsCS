using ProgramArgumentsCS.Errors;

namespace ProgramArgumentsCS.Interfaces
{
  public interface IErrorCommand
  {
    void Handle(Error error);
  }
}
