// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.CommandLine;
using LocalizableStrings = Microsoft.DotNet.Tools.Sln.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    public static class SlnAddParser
    {
        public static readonly Argument ProjectPathArgument = new Argument<IEnumerable<string>>(LocalizableStrings.AddProjectPathArgumentName)
        {
            Description = LocalizableStrings.AddProjectPathArgumentDescription,
            Arity = ArgumentArity.ZeroOrMore,
        };

        public static readonly Option InRootOption = new Option<bool>("--in-root", LocalizableStrings.InRoot);

        public static readonly Option SolutionFolderOption = new Option<string>(new string[] { "-s", "--solution-folder" }, LocalizableStrings.AddProjectSolutionFolderArgumentDescription)
        {
            Argument = new Argument<string>()
        };

        public static Command GetCommand()
        {
            var command = new Command("add", LocalizableStrings.AddAppFullName);

            command.AddArgument(ProjectPathArgument);
            command.AddOption(InRootOption);
            command.AddOption(SolutionFolderOption);

            return command;
        }
    }
}
