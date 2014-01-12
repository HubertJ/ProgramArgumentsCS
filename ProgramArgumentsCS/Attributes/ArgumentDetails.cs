using System;
using ProgramArgumentsCS.Arguments;

namespace ProgramArgumentsCS.Attributes
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
  public class ArgumentDetails : Attribute
  {
    #region Construction

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
