using System.Text;
using Application;
using ConsoleUI.Commands.Generate.Command;
using ConsoleUI.Commands.Generate.Crud;
using ConsoleUI.Commands.Generate.Query;
using ConsoleUI.Commands.New;
using Core.ConsoleUI.IoC.SpectreConsoleCli;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

#region Console Configuration

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

#endregion

#region IoC

IServiceCollection services = new ServiceCollection();
services.AddApplicationServices();
TypeRegistrar registrar = new(services);

#endregion

CommandApp app = new(registrar);
app.Configure(config =>
{
    #region Controller

    config.AddBranch(
        name: "generate",
        action: config =>
        {
            config.SetDescription("Generate project elements");

            config
                .AddCommand<GenerateCrudCliCommand>(name: "crud")
                .WithDescription(description: "Generate CRUD operations for new entity")
                .WithExample(args: new[] { "generate", "crud", "User", "BaseDbContext" });
         
        }

    );
    config
    .AddCommand<CreateNewProjectCliCommand>(name: "new")
    .WithDescription(description: "Create a new project")
    .WithExample(args: new[] { "new", "ExampleProject" });
    #endregion
});

AnsiConsole.Write(new FigletText("halaga").LeftJustified().Color(Color.Blue));

return app.Run(args);
