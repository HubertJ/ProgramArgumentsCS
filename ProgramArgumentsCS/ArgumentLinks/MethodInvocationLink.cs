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
  public class MethodInvocationLink : IArgumentLink
  {
    #region Construction

    /// <summary>
    /// Constructs an instance of the PropertyValueLink class
    /// </summary>
    /// <param name="argument">The argument we will look for</param>
    /// <param name="instance">The instance to set the value on</param>
    /// <param name="property">The property that needs to be set</param>
    public MethodInvocationLink(Argument argument, object instance, MethodInfo method)
    {
      _instance = instance;
      _argument = argument;
      _method = method;
      _errors = new List<Error>();
    }

    #endregion

    #region IArgumentLink Members

    /// <summary>
    /// Handle the argument and invoke the method
    /// </summary>
    /// <param name="args">The collection of command line arguments</param>
    public bool Handle(string[] args)
    {
      bool handled = false;
      var commandString = string.Format("/{0}=", _argument.Command);
      var stringArgument = args.FirstOrDefault(arg => arg.StartsWith(commandString));

      if (stringArgument != null)
      {
        try
        {
          var argumentValue = stringArgument.Substring(commandString.Length);
          _method.Invoke(_instance, new object[] { argumentValue });
          handled = true;
        }
        catch (Exception ex)
        {
          AddError(Severity.Critical, ex);
        }
      }
      else if (_argument.Requirements == ArgumentRequirements.Mandatory)
      {
        AddError(Severity.Critical, string.Format("The argument {0} has not been found despite being mandatory", _argument.Name));
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

    private MethodInfo _method;

    private List<Error> _errors;

    #endregion
  }
}
