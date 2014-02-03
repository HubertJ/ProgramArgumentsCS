using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramArgumentsCS.Attributes
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
  public class VerbAttribute : Attribute
  {
    #region Construction

    /// <summary>
    /// Consturctor for the very attribute. Sets the name and description
    /// </summary>
    /// <param name="name">The name of the verb</param>
    /// <param name="command">The command of the verb</param>
    public VerbAttribute(string name, string command)
    {
      Name = name;
      Command = command;
    }

    #endregion
    
    #region Interface
    
    /// <summary>
    /// The name of this verb
    /// </summary>
    public string Name
    {
      get;
      private set;
    }

    /// <summary>
    /// The argument as specified on the command line
    /// </summary>
    public string Command
    {
      get;
      private set;
    }

    #endregion
  }
}
