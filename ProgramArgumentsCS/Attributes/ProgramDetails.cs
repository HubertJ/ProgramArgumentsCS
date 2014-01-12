using System;

namespace ProgramArgumentsCS.Attributes
{
  /// <summary>
  /// A simple attribute to decorate the the programs option class to provide
  /// information about the program such as the name and a brief description. 
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class ProgramDetails : Attribute
  {
    #region Construction

    /// <summary>
    /// The constructor to set all the program details
    /// </summary>
    /// <param name="name">The name of the program</param>
    /// <param name="description">The description of the program (what its for)</param>
    /// <param name="version">The version number</param>
    /// <param name="publisher">The publisher</param>
    public ProgramDetails(string name, string description, string version, string publisher)
    {
      Name = name;
      Description = description;
      Version = version;
      Publisher = publisher;
    }

    #endregion

    #region Interface

    /// <summary>
    /// The name of the program
    /// </summary>
    public string Name
    {
      get;
      set;
    }

    /// <summary>
    /// The description of the program
    /// </summary>
    public string Description
    {
      get;
      set;
    }

    /// <summary>
    /// The version number of the program
    /// </summary>
    public string Version
    {
      get;
      set;
    }

    /// <summary>
    /// The publisher of the progrma
    /// </summary>
    public string Publisher
    {
      get;
      set;
    }

    #endregion
  }
}
