using System;
using ProgramArgumentsCS.Model;

namespace ProgramArgumentsCS.Attributes
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
  public class ArgumentDetails : Attribute
  {
    #region Construction

    /// <summary>
    /// Constructor for the ArgumentDetails class specifiying all of the 
    /// default settings
    /// </summary>
    /// <param name="name">The name of the arguments</param>
    /// <param name="command">The commands line argument that must be used</param>
    /// <param name="requirements">The requirements placed on this argument</param>
    /// <param name="type">The type of command line argument it is</param>
    public ArgumentDetails(string name, string command, ArgumentRequirements requirements, ArgumentType type)
    {
      if (string.Equals(name, "help", StringComparison.InvariantCultureIgnoreCase))
      {
        throw new ArgumentException("The argument help is a special reserved name and cannot be used", "name");
      }

      if (string.Equals(command, "?", StringComparison.InvariantCultureIgnoreCase))
      {
        throw new ArgumentException("The question mark '?' is a special reserved command and cannot be used", "command");
      }

      Name = name;
      Command = command;
      Requirements = requirements;
      Type = type;
    }

    #endregion
    
    #region Interface
    
    /// <summary>
    /// The name of this command line argument
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

    /// <summary>
    /// Specification on whether the argument is required
    /// </summary>
    public ArgumentRequirements Requirements
    {
      get;
      private set;
    }

    /// <summary>
    /// Specifies the type of the command line argument
    /// </summary>
    public ArgumentType Type
    {
      get;
      private set;
    }

    #endregion
  }
}
