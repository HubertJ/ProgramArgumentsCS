using System.Reflection;

namespace ProgramArgumentsCS.Model
{
  public enum MemberType 
  {
    Property,
    Method
  }

  public class ArgumentMemberLink
  {
    public ArgumentMemberLink(Argument argument, MemberInfo member, MemberType type, object instance)
    {
      Argument = argument;
      Member = member;
      MemberType = type;
      Instance = instance;
    }

    public Argument Argument
    {
      get;
      set;
    }

    public MemberInfo Member
    {
      get;
      set;
    }

    public MemberType MemberType
    {
      get;
      set;
    }

    public object Instance
    {
      get;
      set;
    }
  }
}
