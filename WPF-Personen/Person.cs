using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Personen;
internal class Person
{
  public required string Firstname { get; set; }
  public required string Lastname { get; set; }
  public DateTime Birthdate { get; set; }
  public bool IsStudent { get; set; }
  public bool HasDriversLicense { get; set; }

  public override string ToString() => $"{Lastname} {Firstname} - {Birthdate} [{(IsStudent ? "Student" : "Teacher")}{(HasDriversLicense ? ", Driver" : "")}]";
  public string ToCsvString() => $"{Firstname};{Lastname};{IsStudent};{HasDriversLicense};{Birthdate.ToShortDateString()}";

  public static Person Parse(string line)
  {
    string[] parts = line.Split(';');
    return new Person()
    {
      Firstname = parts[0],
      Lastname = parts[1],
      Birthdate = DateTime.Parse(parts[4]),
      IsStudent = bool.Parse(parts[2]),
      HasDriversLicense = bool.Parse(parts[3]),
    };
  }
  public static bool TryParse(string line, out Person? person)
  {
    string[] parts = line.Split(';');
    try
    {
      person = new()
      {
        Firstname = parts[0],
        Lastname = parts[1],
        Birthdate = DateTime.Parse(parts[4]),
        IsStudent = bool.Parse(parts[2]),
        HasDriversLicense = bool.Parse(parts[3]),
      };
      return true;
    }
    catch (Exception)
    {
      person = null;
      return false;
    }
  }
}
