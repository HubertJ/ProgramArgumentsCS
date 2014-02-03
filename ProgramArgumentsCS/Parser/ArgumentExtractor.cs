using System.Collections.Generic;
using System.Reflection;
using ProgramArgumentsCS.ArgumentLinks;
using ProgramArgumentsCS.Attributes;
using ProgramArgumentsCS.Errors;
using ProgramArgumentsCS.Interfaces;
using ProgramArgumentsCS.Model;

namespace ProgramArgumentsCS.Parser
{
  public class ArgumentExtractor<T> : IArgumentExtractor
  {
    #region Construction

    /// <summary>
    /// The constructor for the argument extractor. 
    /// </summary>
    /// <param name="instance">The instance that any arguments shall be set on</param>
    public ArgumentExtractor(T instance)
    {
      _instance = instance;
      _errors = new List<Error>();
      _arguments = new List<IArgumentLink>();
    }

    #endregion

    #region Interface

    /// <summary>
    /// Method to get the arguments extracted from the supplied type
    /// </summary>
    /// <returns>The list of arguments</returns>
    public IEnumerable<IArgumentLink> GetArguments()
    {
      GetProperties();

      GetMethods();

      return _arguments;
    }

    /// <summary>
    /// The errors found when handling this argument link
    /// </summary>
    public IEnumerable<Error> Errors
    {
      get
      {
        return _errors;
      }
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Get arguments that set properties
    /// </summary>
    /// <param name="arguments">The collection of arguments to add to</param>
    private void GetProperties()
    {
      var properties = typeof(T).GetProperties();
      foreach (var property in properties)
      {
        var argumentDetails = (ArgumentDetails)property.GetCustomAttribute(typeof(ArgumentDetails), false);
        if (argumentDetails != null)
        {
          AddArgument(argumentDetails, property);
        }
      }
    }

    /// <summary>
    /// Get arguments that set invoke methods
    /// </summary>
    /// <param name="arguments">The collection of arguments to add to</param>
    private void GetMethods()
    {
      var methods = typeof(T).GetMethods();
      foreach (var method in methods)
      {
        var argumentDetails = (ArgumentDetails)method.GetCustomAttribute(typeof(ArgumentDetails), false);
        if (argumentDetails != null)
        {
          AddArgument(argumentDetails, method);
        }
      }
    }

    /// <summary>
    /// Adds a method argument to the collection
    /// </summary>
    /// <param name="argumentDetails">The details for this argument</param>
    /// <param name="method">The method that shall be called when the argument is found</param>
    private void AddArgument(ArgumentDetails argumentDetails, MethodInfo method)
    {
      Help helpDetails = (Help)method.GetCustomAttribute(typeof(Help), false);
      Argument argument = Argument.CreateFromAttributes(argumentDetails, helpDetails);

      _arguments.Add(new MethodInvocationLink(argument, _instance, method));
    }

    /// <summary>
    /// Adds a property argument to the collection
    /// </summary>
    /// <param name="argumentDetails">The details for this argument</param>
    /// <param name="property">The property to be set from this argument</param>
    private void AddArgument(ArgumentDetails argumentDetails, PropertyInfo property)
    {
      Help helpDetails = (Help)property.GetCustomAttribute(typeof(Help), false);
      Argument argument = Argument.CreateFromAttributes(argumentDetails, helpDetails);

      if (argument.Type == ArgumentType.Parameter)
      {
        _arguments.Add(new PropertyValueLink(argument, _instance, property));
      }
      else
      {
        _arguments.Add(new PropertySwitchLink(argument, _instance, property));
      }
    }

    #endregion 

    #region Fields

    /// <summary>
    /// The instance that will be used to invoke the arguments
    /// </summary>
    T _instance;

    /// <summary>
    /// The collection of argument links
    /// </summary>
    List<IArgumentLink> _arguments;

    /// <summary>
    /// The collection of errors
    /// </summary>
    List<Error> _errors;

    #endregion
  }
}
