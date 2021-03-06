<p align="center">
  <img align="right" src="https://raw.githubusercontent.com/mrousavy/Morph/master/Images/emoji_hammer_and_wrench.png" height="120" />
  <h1 align="left">Morph</h3>
  <p align="left">A fast .NET Standard Class Library for parsing results from an SQL query to .NET objects</p>
  <p align="left">
    <a href="http://nuget.org/packages/Morph/"><img src="https://img.shields.io/badge/nuget-Morph-blue.svg" alt="Morph on NuGet"></a>
    <a href="https://ci.appveyor.com/project/mrousavy/morph"><img src="https://ci.appveyor.com/api/projects/status/k6dd0rtskfjxrw4o?svg=true" alt="Morph build status"></a>
  </p>
  <a href='https://ko-fi.com/F1F8CLXG' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://az743702.vo.msecnd.net/cdn/kofi2.png?v=0' border='0' alt='Buy Me a Coffee at ko-fi.com' /></a>
</p>

## Why?

The default **hardcode-index-way** of creating objects from a _Data Reader_ can be really messy for _large objects_ with _many columns_.

**Morph** aims to _simplify_ this process by **only needing a single line of code** to parse Data Reader results, and by making code bases **easily extensible** with the `ColumnName` Attributes.

A _small_ example:
```cs
if (await reader.ReadAsync()) {
  var person = new Person() {
    FirstName = reader["PERSON_FIRST_NAME"],
    LastName = reader["PERSON_LAST_NAME"],
    Address = reader["PERSON_ADDRESS"]
  };
}
```

With **Morph**'s extension Method:
```cs
var person = await reader.Parse<Person>();
```
_(Requires a `using` directive to the `mrousavy.Morph` namespace)_

Keep in mind to **mark the Members** you want to parse with the `ColumnName` Attribute in `Person`:
```cs
public class Person {
  [ColumnName("PERSON_FIRST_NAME")]
  public string FirstName { get; set; }   // Will be set to "PERSON_FIRST_NAME" (ColumnName parameter) from the DataBase

  [ColumnName]
  public string LastName { get; set; }    // Will be set to "LastName" (Member name) from the Database

  public string Address { get; set; }     // Will be ignored and not initialized by the Parser
}
```

## Build
Build with [.NET Core](https://www.microsoft.com/net/download/core) 2.0 or higher
