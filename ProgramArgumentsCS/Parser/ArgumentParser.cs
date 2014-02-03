using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ProgramArgumentsCS.Model;
using ProgramArgumentsCS.Attributes;
using ProgramArgumentsCS.Errors;
using ProgramArgumentsCS.Interfaces;
using ProgramArgumentsCS.ArgumentLinks;

namespace ProgramArgumentsCS.Parser
{
  public class ArgumentParser<OptionsClass>
  {
    #region Construction

    /// <summary>
    /// Constructor for the argument parser class
    /// </summary>
    /// <param name="options">The options class to populate with the command line arguments</param>
    /// <param name="args">The command line arguments</param>
    public ArgumentParser(OptionsClass options, string[] args)
    {
      _options = options;
      _args = args;

      _argumentLinks = new List<IArgumentLink>();
      _verbs = new List<Verb>();

      // May throw exceptions if the T options class is not set up correctly. 
      // This is to ensure the correct usage. 
      Prepare();

      try
      {
        Parse();
      }
      catch (Exception ex)
      {
        AddError(Severity.Critical, null, ex.Message);
      }
    }

    
    #endregion

    #region Interface

    /// <summary>
    /// Property returning the existence of critical errors such as parse
    /// errors, missing mandatory values
    /// </summary>
    public bool CriticalErrors
    {
      get { return _errors.Any(error => error.Severity == Severity.Critical); }
    }

    /// <summary>
    /// Property returning the existence of warning errors such as the use of a
    /// final deprecated command line argument or duplicate arguments 
    /// </summary>
    public bool WarningErrors
    {
      get { return _errors.Any(error => error.Severity == Severity.Warning); }
    }

    /// <summary>
    /// Property returning the existence of informational errors such as the use 
    /// of a newly deprecated command line argument 
    /// </summary>
    public bool InformationalErrors
    {
      get { return _errors.Any(error => error.Severity == Severity.Information); }
    }

    /// <summary>
    /// Method to allow custom handling of any errors if they exist. 
    /// </summary>
    /// <param name="severity"></param>
    /// <param name="action"></param>
    public void HandleErrors(Severity severity, IErrorCommand command)
    {
      foreach (var error in _errors)
      {
        if ((severity & error.Severity) == error.Severity)
        {
          command.Handle(error);
        }        
      }
    }

    /// <summary>
    /// Property returing whether the help should be displayed
    /// </summary>
    public bool PendingHelp
    {
      get;
      internal set;
    }

    /// <summary>
    /// Method to display the help
    /// </summary>
    public void DisplayHelp()
    {
      throw new NotImplementedException("The display help functionality has not been implemented yet");
    }

    #endregion

    #region Implementation


    /// <summary>
    /// Checks whether the user of the application has requested the help or 
    /// if they have tried to run the application with no command line 
    /// arguments in which case they probably need the help...
    /// </summary>
    /// <returns></returns>
    private bool CheckRequestHelp()
    {
      // What happens if all the arguments are optional?!
      // Maybe we should only show the help if it is explicitly requested????
      if (_args.Length == 0 || _args.Contains("/?"))
      {
        PendingHelp = true;
        return true;
      }
      return false;
    }

    /// <summary>
    /// Adds an error to the collection of errors.
    /// </summary>
    /// <param name="severity">The severity of the error being added</param>
    /// <param name="argument">The argument that is associated with the error</param>
    /// <param name="message">The message for the error</param>
    private void AddError(Severity severity, Argument argument, string message)
    {
      _errors.Add(new Error() { Severity = severity, Argument = argument, Message = message });
    }
    
    /// <summary>
    /// Prepare the program arguments for the options object 
    /// </summary>
    private void Prepare()
    {
      GetProgramDetails();

      ArgumentExtractor<OptionsClass> argumentExtractor = new ArgumentExtractor<OptionsClass>(_options);
      var optionArguments = argumentExtractor.GetArguments();
      _argumentLinks.AddRange(optionArguments);

      VerbExtractor<OptionsClass> verbExtractor = new VerbExtractor<OptionsClass>(_options);
      var optionVerbs = verbExtractor.GetVerbs();
      _verbs.AddRange(optionVerbs);
      
      Validate();
    }

    private void Validate()
    {
      
    }

    private void Parse()
    {
      FindVerb();

      foreach (var argument in _argumentLinks)
      {
        argument.Handle(_args);
        _errors.AddRange(argument.Errors);
      }
    }

    private void FindVerb()
    {
      foreach (Verb verb in _verbs)
      {
        if (_args.Contains(verb.Command))
        {
          _argumentLinks.AddRange(verb.Arguments);
          break;
        }
      }
    }

    private void GetProgramDetails()
    {
      var details = (ProgramDetails)typeof(OptionsClass).GetCustomAttribute(typeof(ProgramDetails), false);
      var help = (Help)typeof(OptionsClass).GetCustomAttribute(typeof(Help), false);
      if (details != null)
      {
        _program = Program.CreateFromAttributes(details, help);
      }
      else
      {
        throw new InvalidOperationException("The program options class supplied does not specify a details attribute.");
      }
    }
    
    #endregion

    #region Fields

    private OptionsClass _options;

    private string[] _args;

    private List<Error> _errors = new List<Error>();

    private Program _program;

    private List<Verb> _verbs;
    
    private List<IArgumentLink> _argumentLinks;

    #endregion
  }
}
