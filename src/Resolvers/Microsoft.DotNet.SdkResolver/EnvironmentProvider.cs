// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable

namespace Microsoft.DotNet.DotNetSdkResolver
{
    internal class EnvironmentProvider
    {
        private IEnumerable<string> _searchPaths;

        private readonly Func<string, string> _getEnvironmentVariable;

        public EnvironmentProvider(Func<string, string> getEnvironmentVariable)
        {
            _getEnvironmentVariable = getEnvironmentVariable;
        }

        public string ExecutableExtension
        {
            get
            {
                return Interop.RunningOnWindows ? ".exe" : string.Empty;
            }
        }

        private IEnumerable<string> SearchPaths
        {
            get
            {
                if (_searchPaths == null)
                {
                    var searchPaths = new List<string>();

                    searchPaths.AddRange(
                        _getEnvironmentVariable("PATH")
                        .Split(Path.PathSeparator)
                        .Select(p => p.Trim('"')));

                    _searchPaths = searchPaths;
                }

                return _searchPaths;
            }
        }

        public string GetCommandPath(string commandName)
        {
            var commandNameWithExtension = commandName + ExecutableExtension;
            var commandPath = SearchPaths
                .Where(p => !Path.GetInvalidPathChars().Any(c => p.Contains(c)))
                .Select(p => Path.Combine(p, commandNameWithExtension))
                .FirstOrDefault(File.Exists);

            return commandPath;
        }
    }
}
