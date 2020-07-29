# GCommon
A project to provide some extra methods and objects that standard .NET libraries to not provide.

The name of the library comes from my internet alias "Gaalidas" which, itself, is a long story about online game characters and a character in a book series I read as a kid. As it's a common method library, I ended up with "GCommon" as the overall name of the library collection.  From there, the usage of the letter'G' took a life of it's own resulting in class names that start with 'G' to mark them as my own.

It began when I discovered that it was awkward to have to enter "Console.Write" when I wanted to write to the console.  I wanted to have something more easily accessible to use when writing a simple replacement for a DOS Batch file I had already developed that was getting too big and slow for it's purpose.  At the time there was no "using static" for shortening class calls and whatnot.

Next I discovered that I was having to embed Lists and Dictionary collections within themselves to track more than one data item to a specific key.  I wanted to have a Dictionary type class that would allow me to specify optional second, third, fourth and more variables to track with the specified key.  Over many hours of experimentation and a lot of breaks (like weeks of off time between attempts) I finally figured it out.  I now have a collection-style class group I call 'GDict' which can be instantiated like a standard Dictionary, or you can specify a number of additional variables to track in the value type.
