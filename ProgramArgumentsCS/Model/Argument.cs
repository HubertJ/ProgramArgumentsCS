using System.Collections.Generic;
using ProgramArgumentsCS.Attributes;

namespace ProgramArgumentsCS.Model
{
  public class Argument
  {
    #region Construction

    public Argument()
    {
      Usage = new List<string>();
    }

    public static Argument CreateFromAttributes(ArgumentDetails details, Help help)
    {
      Argument newArgument = new Argument()
      {
        Name = details.Name,
        Command = details.Command,
        Requirements = details.Requirements,
        Type = details.Type, 
        Description = help.Description, 
        Usage = help.Usage
      };

      return newArgument;
    }

    #endregion

    #region Interface

    public string Name
    {
      get;
      set;
    }

    public string Command
    {
      get;
      set;
    }

    public ArgumentRequirements Requirements
    {
      get;
      set;
    }

    public ArgumentType Type
    {
      get;
      set;
    }

    public string Description
    {
      get;
      set;
    }

    public IEnumerable<string> Usage
    {
      get;
      set;
    }

    #endregion
  }
}
