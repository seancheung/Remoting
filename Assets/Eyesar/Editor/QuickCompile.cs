// ************************
// Author: Sean Cheung
// Create: 2016/06/28/15:53
// Modified: 2016/06/28/18:14
// ************************

using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eyesar
{
    internal static class QuickCompile
    {
        [MenuItem("Assets/Compile Scripts")]
        private static void Compile()
        {
            var scripts =
                Selection.objects.OfType<TextAsset>()
                    .Select(txt => AssetDatabase.GetAssetPath(txt))
                    .Where(
                        path =>
                            string.Equals(Path.GetExtension(path), ".cs", StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();
            if (scripts.Length == 0)
                return;

            var references =
                Selection.objects.Select(dll => AssetDatabase.GetAssetPath(dll))
                    .Where(
                        path =>
                            string.Equals(Path.GetExtension(path), ".dll", StringComparison.InvariantCultureIgnoreCase)).ToArray();

            var output = EditorUtility.SaveFilePanel("Build Assembly", Directory.GetCurrentDirectory(), "Eyesar", "dll");
            if (string.IsNullOrEmpty(output))
                return;

            while (Path.GetFullPath(output).StartsWith(Path.Combine(Directory.GetCurrentDirectory(), "Assets")))
            {
                var opt = EditorUtility.DisplayDialogComplex("Output path is in Assets folder!",
                    "This might cause conflicts. Continue?", "Yes", "Change", "Cancel");
                if (opt == 0)
                    break;
                if (opt == 2)
                    return;
                output = EditorUtility.SaveFilePanel("Build Assembly", Directory.GetCurrentDirectory(), "Eyesar", "dll");
                if (string.IsNullOrEmpty(output))
                    return;
            }

            var logs = EditorUtility.CompileCSharp(scripts, references,
                EditorUserBuildSettings.activeScriptCompilationDefines,
                output);
            if (logs.Length == 0)
                EditorUtility.RevealInFinder(output);
            else
                foreach (var log in logs)
                    Debug.LogError(log);
        }

        [MenuItem("Assets/Compile Scripts", true)]
        private static bool ValidCompile()
        {
            return Selection.objects.Length > 0;
        }
    }
}