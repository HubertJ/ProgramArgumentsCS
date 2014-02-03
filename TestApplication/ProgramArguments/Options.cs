using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramArgumentsCS.Attributes;
using ProgramArgumentsCS.Model;

namespace TestApplication.ProgramArguments
{
  public class TimeVerb
  {
    [ArgumentDetails("Seconds", "seconds", ArgumentRequirements.Mandatory, ArgumentType.Parameter)]
    [Help("The number of seconds", "/seconds=10")]
    public int Seconds
    {
      get;
      set;
    }
    
    [ArgumentDetails("Minutes", "minutes", ArgumentRequirements.Mandatory, ArgumentType.Parameter)]
    [Help("The number of minutes", "/minutes=14")]
    public int Minutes
    {
      get;
      set;
    }
    
    [ArgumentDetails("Hours", "hours", ArgumentRequirements.Mandatory, ArgumentType.Parameter)]
    [Help("The number of hours", "/hours=2")]
    public int Hours
    {
      get;
      set;
    }
  }




  public class DownloadVerb
  {
    [ArgumentDetails("Filename", "file", ArgumentRequirements.Mandatory, ArgumentType.Parameter)]
    [Help("The filename to save the download to", "/file=C:\\Download\\WhatWhat")]
    public string Filename
    {
      get;
      set;
    }

    [ArgumentDetails("URL", "url", ArgumentRequirements.Mandatory, ArgumentType.Parameter)]
    [Help("The address to download the file from", "/url=www.google.com/file.zip")]
    public string URL
    {
      get;
      set;
    }

    [ArgumentDetails("Username", "user", ArgumentRequirements.Optional, ArgumentType.Parameter)]
    [Help("The username to authenticate the download", "/user=username")]
    public string Username
    {
      get;
      set;
    }

    [ArgumentDetails("Password", "pass", ArgumentRequirements.Optional, ArgumentType.Parameter)]
    [Help("The password to authenticate the download", "/user=username")]
    public string Password
    {
      get;
      set;
    }
  }


  [ProgramDetails("Test Applicatiopn")]
  [Help("A test application to try the program arguments parser", "cmd>TestApplication verb /arg=ArgumentOne", "cmd>TestApplication verb /arg=ArgumentOne /switch")]
  public class Options
  {
    public Options()
    {
      Time = new TimeVerb();
      Download = new DownloadVerb();
    }

    [ArgumentDetails("Number", "number", ArgumentRequirements.Mandatory, ArgumentType.Parameter)]
    [Help("An argument to specify the number of somethings in a thing", "/number=100", "/number=1000")]
    public int Number
    {
      get;
      set;
    }

    [ArgumentDetails("Optional", "optional", ArgumentRequirements.Optional, ArgumentType.Parameter)]
    [Help("An argument to specify the number of somethings in a thing", "/optional=3.1415", "/optional=41.999999")]
    public double Optional
    {
      get;
      set;
    }

    [ArgumentDetails("Flag", "flag", ArgumentRequirements.Optional, ArgumentType.Switch)]
    [Help("A flag to say if we want to do something with our thing", "/flag")]
    public bool Flag
    {
      get;
      set;
    }

    [ArgumentDetails("Call", "call", ArgumentRequirements.Optional, ArgumentType.Parameter)]
    [Help("A method to call with something or other", "/call=\"This was a message\"")]
    public void MethodTest(string argument)
    {
      Console.WriteLine("MethodTest was called with {0}", argument);
    }

    [Verb("Time", "timer")]
    [Help("Timey wimey thingy majigy whatsita doodle")]
    public TimeVerb Time
    {
      get;
      set;
    }

    [Verb("Download", "download")]
    [Help("Down to the load yo")]
    public DownloadVerb Download
    {
      get;
      set;
    }
  }
}
