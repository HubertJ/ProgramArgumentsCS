using System;
using System.Collections.Generic;
using System.Reflection;
using ProgramArgumentsCS.ArgumentLinks;
using ProgramArgumentsCS.Attributes;
using ProgramArgumentsCS.Errors;
using ProgramArgumentsCS.Interfaces;
using ProgramArgumentsCS.Model;

namespace ProgramArgumentsCS.Parser
{
  public class VerbExtractor<T> : IVerbExtractor
  {
    #region Construction

    /// <summary>
    /// The constructor for the argument extractor. 
    /// </summary>
    /// <param name="instance">The instance that any arguments shall be set on</param>
    public VerbExtractor(T instance)
    {
      _instance = instance;
      _errors = new List<Error>();
      _verbs = new List<Verb>();
    }

    #endregion

    #region IVerbExtractor Members

    public IEnumerable<Verb> GetVerbs()
    {
      var properties = typeof(T).GetProperties();
      foreach (var property in properties)
      {
        VerbAttribute verb = (VerbAttribute)property.GetCustomAttribute(typeof(VerbAttribute), false);

        if (verb != null)
        {
          AddVerb(verb, property);
        }
      }

      return _verbs;
    }

    public IEnumerable<Error> Errors
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

    #region Implementation
    
    private void AddVerb(VerbAttribute verbAttribute, PropertyInfo property)
    {
      Type extractor = typeof(ArgumentExtractor<>);
      var generic = extractor.MakeGenericType(property.PropertyType);

      ConstructorInfo constructor = generic.GetConstructor(new Type[] { property.PropertyType });

      IArgumentExtractor argumentExtractor = (IArgumentExtractor)constructor.Invoke(new object[] { property.GetValue(_instance) });

      IEnumerable<IArgumentLink> links = argumentExtractor.GetArguments();

      var helpDetails = (Help)property.GetCustomAttribute(typeof(Help), false);
      _verbs.Add(Verb.CreateFromAttributes(verbAttribute, helpDetails, links));
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
    List<Verb> _verbs;

    /// <summary>
    /// The collection of errors
    /// </summary>
    List<Error> _errors;

    #endregion
  }
}
