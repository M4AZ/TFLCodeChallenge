# TFLCodeChallenge

A simple .NET Core 6.0 console app that displays the status of a given road.

The solution has three main project
  1.  REST Client Class library Project
  2.  Console App Project
  3.  Xunit Test Project 
  
  
## Rest Client Class library Project

This project contains a thin abstraction over the TFL API, which is injected to console app via .NET's built-in DI, 
This is to maintain seperation of concerns and to make the enitre project unit testing friendly.


## Console App Project

.NET CORE console application, fully DI compatible, and easily TDD-capable.

## Xunit Test Project 

A simple Xunit test project with mock TFL client and test cases. 


## FAQ

### How to build the code?

The easiest way to build the code is to directly clone this project from Visual Studio or JetBrains Rider and build using dotnet build command.


### How to run the output?

Navigate to the console app project and execute dotnet run, Atternatively you can directly run the application using the IDE's Run feature  


### How to run any tests that you have written?

Navigate to the Xunit test project and execute dotnet test, Atternatively you can directly run the tests using the IDE's run unit test option in the contextual menu. 


Any assumptions that you’ve made

* The problem statement is well-defined and free of ambiguities.
* The input to the program is valid and follows the expected format.
* The program does not need to handle errors or edge cases that are not explicitly mentioned in the problem statement.
* The program does not need to be optimized for performance or memory usage, unless explicitly stated in the problem statement.
* The program does not need to handle concurrency or multi-threading unless explicitly mentioned in the problem statement.
* The developer has access to all necessary libraries and dependencies to solve the problem efficiently.
* The developer is free to choose any testing framework to solve the problem.
* The program does not need to handle security vulnerabilities or prevent against malicious input.
* Authentication, Logging, Server side caching and other production server configurations is an integral part of an application however for the sake of time it is not included.

## Some Important Noted
* App Settings is included in the project to include API related settings, however while I have regeisted on the 
