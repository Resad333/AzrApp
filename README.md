 ## Tech stack
 * Language: C#
 * AzureFunction, XUnit, Mock

 -------------------------------------------------------------------------

## Api documentation
Application consists of App.Core, App.Test and AzureFunctionApp projects.
* App.Core contains main core logic Abstraction,Repository,Service,Validator, Entity and  DTO .
* App.Test contains tests for Service layer which contains main logic .
* AzureFunctionApp contains TimeEntryPost.cs which is entry point and it has Run method and this method invoked by HTTP TRIGGER.
   Dynamics parameters can be added to local.settings.json .
   Sample seetings json file: 
   {
     "IsEncrypted": false,
     "Values": {
       "AzureWebJobsStorage": "UseDevelopmentStorage=true",
       "AzureWebJobsDashboard": "UseDevelopmentStorage=true",

       "dynamicsUrl": "https://YOUR_ORG.crm4.dynamics.com",
       "dynamicsUserName": "YOUR@USERNAME.onmicrosoft.com",
       "dynamicsPassword": "YOUR_PASSWORD"
       }
     }
   
   Sample API URL:http://localhost:7071/api/TimeEntryPost
   POST Request body sample:
   {
     "$schema": "http://json-schema.org/draft-04/schema#",
     "type": "object",
     "properties": {
       "StartOn": {
         "type": "11-03-2022",
         "format": "dd-MM-yyyy"
       },
       "EndOn": {
         "type": "19-03-2022",
         "format": "dd-MM-yyyy"
       }
     },
     "required": [
       "StartOn",
       "EndOn"
     ]
   }
