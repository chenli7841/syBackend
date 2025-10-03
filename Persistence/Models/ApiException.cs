using System;
public class ApiException: Exception
{
    public ApiException(string name, string text):base(text)
    {
        Name = name;
    }
    public string Name { get; set; }
}