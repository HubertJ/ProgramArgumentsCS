using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramArgumentsCS.Errors;
using ProgramArgumentsCS.Errors.Commands;
using ProgramArgumentsCS.Parser;
using TestApplication.ProgramArguments;

namespace TestApplication
{
  class Program
  {
    static void Main(string[] _args)
    {
      List<string> args = new List<string>();
      args.Add("download");
      args.Add("/number=100");
      args.Add("/optional=4.223");
      args.Add("/flag");
      args.Add("/call=\"This was a message\"");
      //args.Add("/seconds=14");
      //args.Add("/minutes=46");
      //args.Add("/hours=2");
      args.Add("/file=\"C:\\Fileout.txt\"");
      args.Add("/url=\"www.github.com\"");
      args.Add("/user=\"hubertj\"");
      args.Add("/pass=\"ASuperSecurePassword\"");



      Options options = new Options();
      ArgumentParser<Options> parser = new ArgumentParser<Options>(options, args.ToArray());

      Console.WriteLine("options.Number         : {0}", options.Number);
      Console.WriteLine("options.Optional       : {0}", options.Optional);
      Console.WriteLine("options.Flag           : {0}", options.Flag);
      Console.WriteLine("options.Time.Hours     : {0}", options.Time.Hours);
      Console.WriteLine("options.Time.Minutes   : {0}", options.Time.Minutes);
      Console.WriteLine("options.Time.Seconds   : {0}", options.Time.Seconds);
      Console.WriteLine("options.Download.File  : {0}", options.Download.Filename);
      Console.WriteLine("options.Download.URL   : {0}", options.Download.URL);
      Console.WriteLine("options.Download.User  : {0}", options.Download.Username);
      Console.WriteLine("options.Download.Pass  : {0}", options.Download.Password);







      parser.HandleErrors(Severity.Critical | Severity.Warning | Severity.Information, new ConsolePrintCommand());



      Console.ReadKey();
    }
  }
}
