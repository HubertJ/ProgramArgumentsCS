using System;

namespace ProgramArgumentsCS.Attributes
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
  public class ArgumentHelp : Attribute
  {
    #region Construction

    /// <summary>
    /// Constructor for the argument help attribute
    /// </summary>
    /// <param name="description">The description of the argument</param>
    public ArgumentHelp(string description)
    {
      Description = description;
    }

    #endregion

    #region Interface

    /// <summary>
    /// A user friendly description of the argument
    /// </summary>
    public string Description
    {
      get;
      set;
    }

    #endregion
  }
}
