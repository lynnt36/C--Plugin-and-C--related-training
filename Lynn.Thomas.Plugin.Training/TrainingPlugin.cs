// Imports the core .NET library that provides basic functionality used by C# programs
using System;
// Imports the Dynamics SDK library, which provides the classes used to build plugins and interact with Dynamics records
using Microsoft.Xrm.Sdk;
// A namespace is used to organise code and keep classes grouped together in a project
namespace Training.Plugin
{
    // public  → allows Dynamics to access and run this class
    // class   → defines a container for our plugin code
    // : IPlugin → indicates this class implements the Dynamics plugin interface
    public class TrainingPlugin : IPlugin
    {
        // public  → allows Dynamics to access this method
        // void    → the method performs actions but does not return a value
        // Execute → the method that Dynamics calls when the plugin is triggered
        // IServiceProvider serviceProvider → provides access to services like context and tracing
        public void Execute(IServiceProvider serviceProvider)
        {
            // Get the plugin execution context from Dynamics
            // This object contains details about the current operation, such as which message triggered the plugin
            var context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // InputParameters contains data sent to the plugin by Dynamics
            // "Target" usually represents the record being created or updated
            // If Target is not present, stop the plugin to prevent errors
            if (!context.InputParameters.Contains("Target"))
                return;

            // Get the "Target" record from the InputParameters collection
            // Convert it to an Entity type so we can access its fields
            var target = (Entity)context.InputParameters["Target"];

            // target.Contains("name") → checks if the "name" field exists on the record
            // This prevents errors because Update messages only include fields that changed
            if (target.Contains("name"))
            {
                // GetAttributeValue<string>() → retrieves the field value and converts it to a string
                // "name" → the logical name of the field in Dynamics
                var name = target.GetAttributeValue<string>("name");
            }

            // Throw a plugin exception to stop the record from being saved
            // The message provided will be shown to the user in Dynamics
            throw new InvalidPluginExecutionException("Name is required.");
        }
    }
}
