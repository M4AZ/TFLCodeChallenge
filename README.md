# TFLCodeChallenge

A simple .NET Core 6.0 console app that displays the status of a given road using TFL's Open API

## Some Important Notes
* App Settings is included in the project to include API related settings, however while the solution uses AppId and AppKey it has not be tested as the keys are not available on the TFL open API website.

* Unit testing coverage can further be enhanced.

* In this code challenge, TDD, DI, REST, SOC, etc. programming strategies/techniques are demonstrated, however there is a lot of room to improve the overall solution. 


## The solution has three main projects
  1.  REST Client Class library Project
  2.  Console App Project
  3.  Xunit Test Project 
  
  
## Rest Client Class library Project

An abstraction layer over the TFL API, which is injected into the console app via the built-in DI feature of .NET, This is to keep the project separate from its concerns and make unit testing easier.

Additionally a minimal utility class is used over the HTTClient interface for clean fluent API calls.


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

### How to change AppId and AppKey? 
In the root folder of the console app, you will find the appsettings.json file. Edit the ApiSettings.AppId property and ApiSettings.AppKey property to change the AppId and AppKey.


# Assumptions

* The problem statement is well-defined and free of ambiguities.
* The input to the program is valid and follows the expected format.
* The program does not need to handle errors or edge cases that are not explicitly mentioned in the problem statement.
* The program does not need to be optimized for performance or memory usage, unless explicitly stated in the problem statement.
* The program does not need to handle concurrency or multi-threading unless explicitly mentioned in the problem statement.
* The developer has access to all necessary libraries and dependencies to solve the problem efficiently.
* The developer is free to choose any testing framework to solve the problem.
* Authentication, Logging, Server side caching and other production server configurations is an integral part of an application however for the sake of time it is not included.



## Tools/Softwares Used 
* MacBook Pro 2019 OS Ventura 13.1
* Jetbrains Rider .NET IDE 

