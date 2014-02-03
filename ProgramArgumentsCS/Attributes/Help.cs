using System;

namespace ProgramArgumentsCS.Attributes
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
  public class Help : Attribute
  {
    #region Construction

    /// <summary>
    /// Constructor for the argument help attribute
    /// </summary>
    /// <param name="description">The description of the argument</param>
    public Help(string description, params string[] usage)
    {
      Description = description;
      Usage = usage;
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

    /// <summary>
    /// A sample example
    /// </summary>
    public string[] Usage
    {
      get;
      set;
    }

    #endregion
  }
}
