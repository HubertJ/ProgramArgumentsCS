using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ProgramArgumentsCS.Errors;
using ProgramArgumentsCS.Interfaces;
using ProgramArgumentsCS.Model;

namespace ProgramArgumentsCS.ArgumentLinks
{
  public class PropertySwitchLink : IArgumentLink
  {
    #region Construction

    /// <summary>
    /// Constructs an instance of the PropertyValueLink class
    /// </summary>
    /// <param name="argument">The argument we will look for</param>
    /// <param name="instance">The instance to set the value on</param>
    /// <param name="property">The property that needs to be set</param>
    public PropertySwitchLink(Argument argument, object instance, PropertyInfo property)
    {
      _instance = instance;
      _argument = argument;
      _property = property;
      _errors = new List<Error>();
    }

    #endregion

    #region IArgumentLink Members

    /// <summary>
    /// Handle the argument and set the property value
    /// </summary>
    /// <param name="args">The collection of command line arguments</param>
    public bool Handle(string[] args)
    {
      bool handled = false;
      try
      {
        string commandString = string.Format("/{0}", _argument.Command);
        bool switchEnabled = args.Contains(commandString);
        _property.SetValue(_instance, switchEnabled);
        handled = true;
      }
      catch (Exception ex)
      {
        AddError(Severity.Critical, ex);
      }
      return handled;
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

    private void AddError(Severity severity, string message)
    {
      _errors.Add(new Error() { Severity = severity, Argument = _argument, Message = message });
    }

    private void AddError(Severity severity, Exception ex)
    {
      StringBuilder sb = new StringBuilder();

      Exception current = ex;

      while (current != null)
      {
        sb.AppendLine(current.Message);
        current = current.InnerException;
      }

      AddError(severity, sb.ToString());
    }
    
    #endregion

    #region Fields

    private object _instance;

    private Argument _argument;

    private PropertyInfo _property;

    private List<Error> _errors;

    #endregion
  }
}
