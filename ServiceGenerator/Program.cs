﻿/*
Copyright 2011 Google Inc

Licensed under the Apache License, Version 2.0(the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.ComponentModel;
using Google.Apis.Samples.Helper;
using Google.Apis.Tools.CodeGen;

namespace Google.Apis.Samples.CmdServiceGenerator
{
    /// <summary>
    /// This example is a command line service generator, which uses discovery and the CodeGen library
    /// to create a strongly typed binding for the specified service. You can either use this example,
    /// or use the pregenerated service files found on the Google APIs page.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// User input for this example
        /// </summary>
        [Description("service")]
        public class ServiceDescription
        {
            [Description("service name")] 
            public string ServiceName = "discovery";
            [Description("service version")]
            public string ServiceVersion = "v1";
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Optional: [serviceName serviceVersion]</param>
        static void Main(string[] args)
        {
            CommandLine.EnableExceptionHandling();
            CommandLine.DisplayGoogleSampleHeader("Service Generator");

            // Parse parameters
            var description = args.Length >= 2
                                  ? new ServiceDescription { ServiceName = args[0], ServiceVersion = args[1] }
                                  : CommandLine.CreateClassFromUserinput<ServiceDescription>();

            string formalServiceName = GeneratorUtils.UpperFirstLetter(description.ServiceName);
            string fileName = string.Format(
                "{0}.{1}.cs", formalServiceName, description.ServiceVersion.Replace(".", '_'));

            // Generate Service
            string serviceNamespace = string.Format(
                "Google.Apis.{0}.{1}", formalServiceName, description.ServiceVersion);
            CommandLine.WriteLine("^9 Generating service ...");
            GoogleServiceGenerator.GenerateService(description.ServiceName,
                                                   description.ServiceVersion, serviceNamespace,
                                                   "CSharp",
                                                   fileName);
            CommandLine.WriteLine("^9 Service generated in ^4" + fileName + "^9!");

            // ..and we are done!
            CommandLine.PressAnyKeyToExit();
        }
    }
}