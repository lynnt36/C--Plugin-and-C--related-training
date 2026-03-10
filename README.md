# Dynamics 365 Plugin and C# related training
C# Plugin training for Dynamics 365 development, and also general C# language training

When installing Visual Studio or if you already have it you can modify what your current Visual studio is able to do in terms of frameworks.
- Open Visual Studio Installer
- You should check the boxes for **ASP.NET and web devlopment**, **Azure development**, **.NET desktop development**. This is what allows your Visual Studio to write C# projects

**SETTING UP A NEW PROJECT**
- Open Visual studio
- click **create a new project**
- Next screen -  type class in the search bar at the top, and select **Class Library (.NET Framework)**
- Next screen - give your project a name and click on the framework dropdown and select **.NET Framework 4.6.2** and then click create
- You should be now in your project.
- In the solution explorer, rename the class1.cs file to something more relatable for e.g TrainingPlugin.cs or whatever you choose.
- Then right click your project at the top level of the solution explorer then click **manage NuGet packages**, this will open another window
- Click browse and search for Microsoft.Xrm then you should see an option **Microsoft.CrmSdk.CoreAssemblies**, click that then click **install**. A few windows will appear, just agree/accept each one. What this does it imports all the references needed and libraries the microsoft SDK uses and needs, which is needed to interact with Dynamics.
- At the top of your .cs file where you will be writing your code you should add a line **using Microsoft.Xrm.Sdk;**. What this does is it imports the Dynamics SDK library, which provides the classes used to build plugins and interact with Dynamics records.
- Your now good to go writing plugins.
