using System;
using System.IO;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Automation.Tests
{
    public class RecipeTests : CommandLineTool
    {
        [UnitTest]
        [TestGroup("Recipe")]
        public void CanLoadProject()
        {
            Project project = "./test_package-project.csproj.xml".FromXmlFile<Project>();
        }
        
        [UnitTest]
        [TestGroup("Recipe")]
        public void CanChangeProjectReferenceToPackageReference()
        {
            string testProjectFileName = $"./test_{6.RandomLetters()}_project-package.csproj";
            File.Copy("./test_project-package.csproj.xml", testProjectFileName);
            Recipe recipe = Recipe.FromProject(testProjectFileName);
            recipe.ReferencesProject(testProjectFileName, "bam.net.core").IsTrue();
            
            recipe.ReferenceAsPackage("bam.net.core");
            
            recipe.ReferencesProject(testProjectFileName, "bam.net.core").IsFalse();
            recipe.ReferencesPackage(testProjectFileName, "bam.net.core").IsTrue();
            
            File.Delete(testProjectFileName);
        }

        [UnitTest]
        [TestGroup("Recipe")]
        public void CanChangePackageReferenceToProjectReference()
        {
            string testProjectFileName = $"./test_{4.RandomLetters()}_package-project.csproj";
            File.Copy("./test_package-project.csproj.xml", testProjectFileName);
            Recipe recipe = Recipe.FromProject(testProjectFileName);
            recipe.ReferencesPackage(testProjectFileName, "bam.net.core").IsTrue();
            
            recipe.ReferenceAsProject("bam.net.core", "../some/path/");
            
            recipe.ReferencesPackage(testProjectFileName, "bam.net.core").IsFalse();
            recipe.ReferencesProject(testProjectFileName, "../some/path/bam.net.core.csproj");
            
            File.Delete(testProjectFileName);
        }
    }
}