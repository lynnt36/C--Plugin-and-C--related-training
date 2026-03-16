// Imports the core .NET library that provides basic functionality used by C# programs
using System;
// Imports the Dynamics SDK library, which provides the classes used to build plugins and interact with Dynamics records
using Microsoft.Xrm.Sdk;

// A namespace is used to organise code and keep classes grouped together in a project
namespace Training.Plugin
{
    // Defines a plugin class called TrainingPlugin
    // The class implements IPlugin, which is required for all Dynamics plugins
    public class TrainingPlugin : IPlugin
    {
        // The Execute method is the entry point for the plugin.
        // Dynamics calls this method whenever the plugin is triggered.
        // IServiceProvider allows us to request services from the Dynamics platform.
        public void Execute(IServiceProvider serviceProvider)
        {
            // STEP 1: Retrieve the tracing service.
            // Tracing allows developers to write log messages that help debug plugin execution.
            // These messages are stored in the Plugin Trace Log inside Dynamics.
            var tracing = (ITracingService)
                serviceProvider.GetService(typeof(ITracingService));

                tracing.Trace("Plugin execution started.");

            // STEP 2: Retrieve the plugin execution context.
            // The context contains information about the event that triggered the plugin.
            // For example:
            // - which message occurred (Create / Update / Delete)
            // - which entity was affected
            // - which user performed the action
            var context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // STEP 3: Prevent plugin recursion.
            // Depth indicates how many times the plugin has executed during the same operation.
            // If a plugin performs an update that triggers itself again, Depth increases.
            // By exiting when Depth > 1, we prevent infinite loops.
            if (context.Depth > 1)
                return;

            // STEP 4: Ensure the Target record exists.
            // InputParameters is a collection of data passed to the plugin.
            // The "Target" parameter typically contains the record involved in the operation.
            // If Target is missing, we exit to avoid errors
            if (!context.InputParameters.Contains("Target"))
                return;

            // STEP 5: Retrieve the record involved in the operation.
            // InputParameters works like a dictionary.
            // The key "Target" contains the record being created or updated.
            // We cast it to an Entity so we can work with it as a Dynamics record.
            var target = (Entity)context.InputParameters["Target"];

            // Write a trace message so developers can see when the plugin starts running.
            tracing.Trace("Plugin running");

            // STEP 6: Check if the "name" field exists on the record.
            // This is important because during an Update operation,
            // Dynamics only sends fields that were changed.
            if (target.Contains("name"))
            {
                // Retrieve the value of the name field.
                // GetAttributeValue<T>() safely converts the field value to the specified type.
                // In this case we expect the name field to be a string.
                var name = target.GetAttributeValue<string>("name");

                // string.IsNullOrWhiteSpace checks if the value is:
                // - null
                // - an empty string ""
                // - a string containing only spaces
                // This helps ensure the field contains meaningful data.
                if (string.IsNullOrWhiteSpace(name))
                {
                    // Throwing an InvalidPluginExecutionException stops the operation.
                    // Dynamics will display this message to the user.
                    throw new InvalidPluginExecutionException("Name is required.");
                }
            }
            // Trace message indicating the plugin finished successfully
            tracing.Trace("Plugin execution completed.");
        }
    }
}
