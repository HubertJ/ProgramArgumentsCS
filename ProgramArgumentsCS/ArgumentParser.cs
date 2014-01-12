using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ProgramArgumentsCS.Arguments;
using ProgramArgumentsCS.Attributes;
using ProgramArgumentsCS.Errors;

namespace ProgramArgumentsCS
{
  public class ArgumentParser<T>
  {
    #region Construction

    public ArgumentParser(T options, string[] args)
    {
      try
      {
        _options = options;
        _args = args;
        Prepare();
        Parse();
      }
      catch (Exception ex)
      {
        AddError(Severity.Critical, null, ex.Message);
      }
    }

    #endregion

    #region Errors

    public bool CriticalErrors
    {
      get { return _errors.Any(error => error.Severity == Severity.Critical); }
    }

    public bool WarningErrors
    {
      get { return _errors.Any(error => error.Severity == Severity.Warning); }
    }

    public bool InformationalErrors
    {
      get { return _errors.Any(error => error.Severity == Severity.Information); }
    }

    public void DisplayErrors(Severity severity)
    {
      DisplayErrorType(severity, Severity.Critical);
      DisplayErrorType(severity, Severity.Warning);
      DisplayErrorType(severity, Severity.Information);
    }

    private void DisplayErrorType(Severity flags, Severity display)
    {
      if ((flags & display) == display)
      {
        var errors = _errors.Where(error => error.Severity == display);

        foreach (var error in errors)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write(display.ToString());
          Console.ResetColor();
          Console.WriteLine(" {0}", error.Message);
        }
      }
    }

    public IEnumerable<Error> Errors
    {
      get { return _errors; }
    }

    #endregion

    #region Help

    private bool CheckHelp()
    {
      if (_args.Length == 0 || _args.Contains("/?"))
      {
        PendingHelp = true;
        return true;
      }
      return false;
    }

    public bool PendingHelp
    {
      get;
      internal set;
    }

    private void DisplayProgramInformation()
    {
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("{0} ", _programName);
      Console.ResetColor();
      Console.WriteLine(_programVersion);
      Console.WriteLine(_programDescription);
      Console.WriteLine();
    }

    private void DisplayParameterInformation(Argument argument)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("{0, -10}", argument.Name);
      Console.ResetColor();
      Console.WriteLine(": /{0}=", argument.Command);

      if (string.IsNullOrWhiteSpace(argument.Description) == false)
      {
        Console.WriteLine(argument.Description);
        Console.WriteLine();
      }

      if (argument.Usage.Count > 0)
      {
        foreach (var usage in argument.Usage)
        {
          Console.WriteLine("/{0}={1}", argument.Command, usage);
        }
        Console.WriteLine();
      }        
    }

    public void DisplayHelp()
    {
      string divider = new string('=', 79);
      Console.WriteLine(divider);

      DisplayProgramInformation();

      Console.WriteLine(divider);
      Console.WriteLine();
      
      var allArguments = _propertyArguments.Keys.Union(_methodArguments.Keys);      
      foreach (var argument in allArguments)
      {
        DisplayParameterInformation(argument);
      }
      
      Console.WriteLine();
      Console.WriteLine(divider);
    }

    #endregion

    private void Parse()
    {
      if (CheckHelp() == false)
      {
        foreach (var argument in _propertyArguments)
        {
          FindAndSet(argument);
        }

        foreach (var argument in _methodArguments)
        {
          FindAndCall(argument);
        }
      }
    }

    private void FindAndCall(KeyValuePair<Argument, MethodInfo> argument)
    {
      var commandString = string.Format("/{0}=", argument.Key.Command);
      var stringArgument = _args.FirstOrDefault(arg => arg.StartsWith(commandString));

      if (stringArgument != null)
      {
        try
        {
          var argumentValue = stringArgument.Substring(commandString.Length);
          var method = argument.Value;
          
          method.Invoke(_options, new object[]{argumentValue});
        }
        catch (Exception ex)
        {
          AddError(Severity.Critical, argument.Key, ex.Message);
        }
      }
      else if (argument.Key.Requirements == ArgumentRequirements.Mandatory)
      {
        AddError(Severity.Critical, argument.Key, string.Format("The argument {0} has not been found despite being mandatory", argument.Key.Name));
      }
    }

    private void FindAndSet(KeyValuePair<Argument, PropertyInfo> argument)
    {
      var commandString = string.Format("/{0}=", argument.Key.Command);
      var stringArgument = _args.FirstOrDefault(arg => arg.StartsWith(commandString));

      if (stringArgument != null)
      {
        try
        {
          var argumentValue = stringArgument.Substring(commandString.Length);
          var property = argument.Value;
          property.SetValue(_options, Convert.ChangeType(argumentValue, property.PropertyType), null);
        }
        catch (Exception ex)
        {
          AddError(Severity.Critical, argument.Key, ex.Message);
        }
      }
      else if (argument.Key.Requirements == ArgumentRequirements.Mandatory)
      {
        AddError(Severity.Critical, argument.Key, string.Format("The argument {0} has not been found despite being mandatory", argument.Key.Name));
      }
    }

    private void AddError(Severity severity, Argument argument, string message)
    {
      _errors.Add(new Error() { Severity = severity, Argument = argument, Message = message });
    }
    
    private void Prepare()
    {
      GetProgramDetails();

      GetArguments();
      
      Validate();
    }

    private void GetProgramDetails()
    {
      var details = (ProgramDetails[])_options.GetType().GetCustomAttributes(typeof(ProgramDetails), false);

      if (details.Length != 1)
      {
        throw new InvalidOperationException("The program options class supplied does not specify a details attribute");
      }

      _programName = details[0].Name;
      _programPublisher = details[0].Publisher;
      _programVersion = details[0].Version;
      _programDescription = details[0].Description;
    }

    private void Validate()
    {
      CheckDuplicateCommands();

      CheckOneFreeArgument();
    }

    private void CheckOneFreeArgument()
    {
      var allArguments = _propertyArguments.Keys.Union(_methodArguments.Keys);

      var freeArguments = allArguments.Where(arg => arg.Type == ArgumentType.Free)
                                      .ToArray();

      if (freeArguments.Length > 0)
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Multiple free arguments found in options");
        foreach (var free in freeArguments)
        {
          sb.AppendFormat("Free Argument : {0}", free.Name)
            .AppendLine();
        }
        throw new InvalidOperationException(sb.ToString());
      }
    }

    private void CheckDuplicateCommands()
    {
      var allArguments = _propertyArguments.Keys.Union(_methodArguments.Keys);

      var duplicates = allArguments.GroupBy(arg => arg.Command)
                                   .Where(group => group.Count() > 1)
                                   .Select(group => group.Key)
                                   .ToArray();

      if (duplicates.Length > 0)
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Duplicate commands found in options");
        foreach (var duplicate in duplicates)
        {
          sb.AppendFormat("Duplicate : {0}", duplicate)
            .AppendLine();
        }
        throw new InvalidOperationException(sb.ToString());
      }
    }

    private void GetArguments()
    {
      Type options = typeof(T);
      var properties = options.GetProperties();
      foreach (var property in properties)
      {
        if (IsArgumentMember(property) == true)
        {
          AddArgumentProperty(property);
        }
      }

      var methods = options.GetMethods();
      foreach (var method in methods)
      {
        if (IsArgumentMember(method) == true)
        {
          AddArgumentMethod(method);
        }
      }
    }

    private bool IsArgumentMember(MemberInfo member)
    {
      return member.GetCustomAttributes(typeof(ArgumentDetails), false).Length == 1;
    }

    private Argument ExtractArgument(MemberInfo member)
    {
      Argument arg = new Argument();

      // Should always have ONE details attribute
      var details = (ArgumentDetails[])member.GetCustomAttributes(typeof(ArgumentDetails), false);
      arg.Name = details[0].Name;
      arg.Command = details[0].Command;
      arg.Requirements = details[0].Requirements;
      arg.Type = details[0].Type;

      // Should have AT MOST ONE help attribute
      var help = (ArgumentHelp[])member.GetCustomAttributes(typeof(ArgumentHelp), false);
      if (help.Length == 1)
      {
        arg.Description = help[0].Description;
      }

      // Can have ANY NUMBER of examples (although too many would look stupid)
      var examples = (ArgumentExample[])member.GetCustomAttributes(typeof(ArgumentExample), false);
      foreach (var example in examples)
      {
        arg.Usage.Add(example.Usage);
      }

      return arg;
    }

    private void AddArgumentProperty(PropertyInfo property)
    {
      Argument arg = ExtractArgument(property);

      _propertyArguments.Add(arg, property);
    }

    private void AddArgumentMethod(MethodInfo method)
    {
      Argument arg = ExtractArgument(method);

      _methodArguments.Add(arg, method);
    }

    #region Fields

    private T _options;

    private string[] _args;

    private List<Error> _errors = new List<Error>();

    private Dictionary<Argument, MethodInfo> _methodArguments = new Dictionary<Argument, MethodInfo>();

    private Dictionary<Argument, PropertyInfo> _propertyArguments = new Dictionary<Argument, PropertyInfo>();

    private string _programName;

    private string _programDescription;

    private string _programPublisher;

    private string _programVersion;

    #endregion
  }
}
