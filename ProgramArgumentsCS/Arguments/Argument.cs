using System.Collections.Generic;

namespace ProgramArgumentsCS.Arguments
{
  public class Argument
  {
    public Argument()
    {
      Usage = new List<string>();
    }

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

    public List<string> Usage
    {
      get;
      set;
    }
  }
}
