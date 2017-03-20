# SummaryLib
 
This is a library written in C# for http://smmry.com/


#Example

The library uses a Builder pattern, therefore you can adapt your Summary object like this
```cs
var sum = new Summary();
sum.ApiKey(_apikey)
    .Url(_url)
    .SentenceCount(_number);
```
Then you can just get the JSON using .GetJSON();

Note: Every parameter is optional except your API key.

#TODO

  1) Deserialize the JSON to make the data more user-friendly
  2) Work on design pattern
  3) Create cleaner implementations

Otherwise the Library is pretty straighforward and fully functional.
