using System.Text.RegularExpressions;
using Application.Features.Generate.Constants;
using Core.CodeGen.File;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Helpers;
using Domain.Constants;

namespace Application.Features.Generate.Rules;

public class GenerateBusinessRules
{
    public async Task EntityClassShouldBeInhreitEntityBaseClass(string projectPath, string entityName)
    {
        string[] fileContent = await File.ReadAllLinesAsync(
            PlatformHelper.SecuredPathJoin(projectPath, "Pars.RM.RetailApi.MobileStoreApi.Data\\DataModel\\RetailManagementContext", "Table", $"tb_{entityName}.cs")
        );

        string entityBaseClassNameSpaceUsingTemplate = await File.ReadAllTextAsync(
            PlatformHelper.SecuredPathJoin(
                DirectoryHelper.AssemblyDirectory,
                Templates.Paths.Crud,
                "Lines",
                "EntityBaseClassNameSpaceUsing.cs.sbn"
            )
        );
        Regex entityBaseClassRegex = new(@$"public\s+class\s+{entityName}\s*:\s*Entity\s*");
        bool isExists =
            fileContent.Any(line =>  fileContent.Any(entityBaseClassRegex.IsMatch));

       
    }

    public Task FileShouldNotBeExists(string filePath)
    {
        if (Directory.Exists(filePath))
            throw new BusinessException(GenerateBusinessMessages.FileAlreadyExists(filePath));
        return Task.CompletedTask;
    }
}
