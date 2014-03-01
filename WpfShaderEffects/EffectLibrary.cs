// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System;
using System.Windows;
using System.Reflection;

namespace WpfShaderEffects {

    internal static class Global {

        private static string _assemblyShortName;

        public static Uri MakePackUri(string relativeFile) {
            string uriString = "pack://application:,,,/" + AssemblyShortName + ";component/" + relativeFile;
            return new Uri(uriString);
        }

        private static string AssemblyShortName {
            get {
                if (_assemblyShortName == null) {
                    Assembly a = typeof(Global).Assembly;
                    _assemblyShortName = a.ToString().Split(',')[0];
                }

                return _assemblyShortName;
            }
        }
        
    }

}
