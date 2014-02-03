using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramArgumentsCS.Attributes;

namespace ProgramArgumentsCS.Model
{
  public class Program
  {
    public Program()
    {
    }

    public static Program CreateFromAttributes(ProgramDetails details, Help help)
    {
      Program newProgram = new Program()
      {
        Name = details.Name,
        Description = help.Description,
        Publisher = details.Publisher
      };

      return newProgram;
    }

    public string Name
    {
      get;
      set;
    }

    public string Description
    {
      get;
      set;
    }

    public string Publisher 
    { 
      get; 
      set; 
    }
  }
}
