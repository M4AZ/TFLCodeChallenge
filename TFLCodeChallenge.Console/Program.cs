// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using TFLCodeChallenge;

try
{
    var app = Startup.Init();
    
    var runner = app.GetRequiredService<IRoadStatus>();
    
    string? input = Console.ReadLine();

    var result = runner.Run(input).Result;

    return result;
}
catch (Exception e)
{
    Console.WriteLine("Exception occured: " + e.Message);
    Console.ReadKey();
}

return 1;
