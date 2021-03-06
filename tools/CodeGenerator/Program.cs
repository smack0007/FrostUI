﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var repositoryPath = Path.GetFullPath(Path.Combine(Assembly.GetEntryAssembly()!.Location, "..", "..", "..", "..", ".."));
            var controlsPath = Path.Combine(repositoryPath, "src", "FrostUI", "Controls");

            var views = JsonSerializer.Deserialize<Dictionary<string, ControlData>>(File.ReadAllText(Path.Combine(controlsPath, "_controls.json")));

            Console.WriteLine("Generating controls...");
            foreach (var view in views)
            {
                GenerateView(controlsPath, view.Key, view.Value);
            }
        }

        private static void GenerateView(string viewsPath, string viewName, ControlData viewData)
        {
            var viewFileName = Path.Combine(viewsPath, viewName + ".g.cs");
            Console.WriteLine($"{viewName} => {viewFileName}");

            var propertiesArray = viewData.Properties.ToArray();

            var output = new StringBuilder(1024);

            output.AppendLine("#nullable enable");
            output.AppendLine("using System;");
            output.AppendLine("using System.Collections.Generic;");
            output.AppendLine();

            output.AppendLine("namespace FrostUI.Controls");
            output.AppendLine("{");
            output.AppendLine($"\tpublic partial class {viewName} : View");
            output.AppendLine("\t{");
            
            foreach (var property in viewData.Properties)
            {
                output.AppendLine($"\t\tpublic {property.Value.Type} {property.Key} {{ get; }}");
                output.AppendLine();
            }

            output.AppendLine($"\t\tpublic {viewName}(");

            for (int i = 0; i < propertiesArray.Length; i++)
            {
                var property = propertiesArray[i];
                output.Append($"\t\t\t{property.Value.Type} {property.Key.FirstLetterToLower()}");

                if (!property.Value.Required)
                    output.Append($" = {property.Value.Default}");

                if (i < propertiesArray.Length - 1)
                    output.AppendLine(",");
                else
                    output.AppendLine(")");
            }

            output.AppendLine("\t\t\t: base(false)");
            output.AppendLine("\t\t{");
            
            foreach (var property in viewData.Properties)
                output.AppendLine($"\t\t\t{property.Key} = {property.Key.FirstLetterToLower()};");

            output.AppendLine($"\t\t\tInitializeState();");

            output.AppendLine("\t\t}");
            output.AppendLine("\t}");
            output.AppendLine("}");
            output.AppendLine();

            File.WriteAllText(viewFileName, output.ToString());
        }
    }
}
