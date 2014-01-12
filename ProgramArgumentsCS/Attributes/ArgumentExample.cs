using System;

namespace ProgramArgumentsCS.Attributes
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
  public class ArgumentExample : Attribute
  {
    #region Construction
    
    /// <summary>
    /// Constructor for the argument example attribute
    /// </summary>
    /// <param name="description">The description of the argument</param>
    public ArgumentExample(string usage)
    {
      Usage = usage;
    }

    #endregion

    #region Interface

    /// <summary>
    /// A sample example
    /// </summary>
    public string Usage
    {
      get;
      set;
    }

    #endregion
  }
}
