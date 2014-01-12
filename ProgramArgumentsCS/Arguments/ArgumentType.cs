
namespace ProgramArgumentsCS.Arguments
{
  /// <summary>
  /// Specifies the argument types available
  /// </summary>
  public enum ArgumentType
  {
    /// <summary>
    /// A parameter is a variable that can be set on the command line
    /// e.g. cmd>ExampleProgram.exe /directory="C:\Users\HubertJ\"
    /// </summary>
    Parameter,

    /// <summary>
    /// A switch is a conditional flag whose presense specifies true
    /// e.g. cmd>ExampleProgram.exe /verbose 
    /// </summary>
    Switch,

    /// <summary>
    /// A free argument is one that doesn't related to a specific parameter
    /// There can only be one free argument specified for each program
    /// e.g. cmd>ExampleProgram.exe test.txt
    /// </summary>
    Free
  }
}
