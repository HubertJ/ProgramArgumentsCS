using System.Collections.Generic;
using ProgramArgumentsCS.ArgumentLinks;
using ProgramArgumentsCS.Attributes;
using ProgramArgumentsCS.Interfaces;

namespace ProgramArgumentsCS.Model
{
  public class Verb
  {
    public Verb()
    {
      Arguments = new List<IArgumentLink>();
    }
    
    public static Verb CreateFromAttributes(VerbAttribute details, Help help, IEnumerable<IArgumentLink> arguments)
    {
      Verb newVerb = new Verb()
      {
        Name = details.Name,
        Command = details.Command,
        Description = help.Description,
        Arguments = new List<IArgumentLink>(arguments)
      };

      return newVerb;
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
    
    public string Description
    {
      get;
      set;
    }

    public List<IArgumentLink> Arguments
    {
      get;
      set;
    }
  }
}
