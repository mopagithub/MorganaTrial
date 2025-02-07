# MorganaTrial
Morgana Trial



Overview of the solution
========================
Create a .Net 9 solution containig two projects: Umbraco CMS and Web API (UmbracoBridge).
Umbraco CMS is a pre-builded project that brings some endpoints to test with the UmbracoBridge project.

Steps to set up and run the solution locally
============================================
1. Download the 5 files in your local machine, in a specfic folder (for example Umbraco projects).
UmbracoBridge.zip.001
UmbracoBridge.zip.002
UmbracoBridge.zip.003
UmbracoBridge.zip.004
UmbracoBridge.zip.005

2. Merge the zip files (you can use 7-zip) to have the UmbracoBridge.zip file

3. Unzip all the files inside the folder, you will have created the folder UmbracoBridge

4. Open the solution .../UmbracoBridge/UmbracoBridge.sln with Visual studio 2022.

5. In Visual Studio 2022 check the two projects will run (right clic on the solution and select "Configure Startup projects ..." and select the option "Multiple startup projects").

6. Execute the solution.

5. Install Bruno (Opensource API Client) in your machine to test the endpoints later.

Example requests and responses for the Web API endpoints
========================================================
When you run your solution, take note of the ports of execution (in my case was localhost:44380 for the UmbracoCMS and localhost:7171 for the UmbracoBridge project).

To test the endpoints using Bruno, first it's needed to generate the access token.
Open a GET request and in the section of Auth select OAuth 2.0, then in the section of "Grant Type" select the option "Client Credentials".

Then, put the next values in this labels:
Access Token URL: https://localhost:44380/umbraco/management/api/v1/security/back-office/token
Client ID: umbraco-back-office-123
Client Secret: 123

Before pressing the button "Get Access Token", you need to press the keys Ctrl + ,(comma), then a Preferences window is going to display, then select the option General and disable "SSL/TLS Certificae Verification" and press the button Save. 

Now, when you press the button "Get Access Token" it is going to show a Response with this format:
{
  "access_token": "crTmq7h6dtK0-gaZbZIZJZmml3vGbCrnLBKZCZBKP9o",
  "token_type": "Bearer",
  "expires_in": 300
}
Note: Take note of the expiration time of the token.

With this access token you can now test this endpoints:
GET: https://localhost:44380/umbraco/management/api/v1/health-check-group
In the Auth, select the option Bearer Token and in the Token textbox paste the "access token" showed before and execute the request. It will show the next Response
{
  "total": 6,
  "items": [
    {
      "name": "Configuration"
    },
    {
      "name": "Data Integrity"
    },
    {
      "name": "Live Environment"
    },
    {
      "name": "Permissions"
    },
    {
      "name": "Security"
    },
    {
      "name": "Services"
    }
  ]
}

GET: https://localhost:7171/Umbraco/Healthcheck
For this request, doesn't need to configure any option, because internally in the code is going to generate an access_token and it will show the same as the https://localhost:44380/umbraco/management/api/v1/health-check-group , just execute the request and the Response will be:
{
  "total": 6,
  "items": [
    {
      "name": "Configuration"
    },
    {
      "name": "Data Integrity"
    },
    {
      "name": "Live Environment"
    },
    {
      "name": "Permissions"
    },
    {
      "name": "Security"
    },
    {
      "name": "Services"
    }
  ]
}

POST: https://localhost:44380/umbraco/management/api/v1/document-type
In the Auth, select the option Bearer Token and in the Token textbox paste the "access token" generated and in the option Body paste something like this, in JSON format:
{
  "alias": "ddd10",
  "name": "nnn",
  "description": "ddd",
  "icon": "icon-iii",
  "allowedAsRoot": true,
  "variesByCulture": true,
  "variesBySegment": true,
  "collection": null,
  "isElement": true
}
In the upper right of the response it will show "201 Created" and in the tab Headers will show the umb-generated-resource variable that has the value of the id created.
...
umb-generated-resource	f9f57c6c-4723-41e7-ba28-d47972baba6f
...

POST: https://localhost:7171/Umbraco/DocumentType
For this request, doesn't need to configure any option, only the Body, because internally in the code is going to generate an access_token and show the POST: https://localhost:44380/umbraco/management/api/v1/document-type. 
The Body
{
  "alias": "ddd20",
  "name": "nnn",
  "description": "ddd",
  "icon": "icon-iii",
  "allowedAsRoot": true,
  "variesByCulture": true,
  "variesBySegment": true,
  "collection": null,
  "isElement": true
}

Just execute the request and the Response will show:
{
  "status": "Created",
  "idGenerated": "c56ba97a-a44e-41ec-8a37-e0687592c13f"
}

DELETE: https://localhost:44380/umbraco/management/api/v1/document-type/f9f57c6c-4723-41e7-ba28-d47972baba6f
In the Auth, select the option Bearer Token and in the Token textbox paste the "access token" generated. The Id to delete is f9f57c6c-4723-41e7-ba28-d47972baba6f.
The Response will be in blank but in the upper right it will show 200 OK.

DELETE: https://localhost:7171/Umbraco/DocumentType/3fa85f64-5717-4562-b3fc-2c963f66afa6
For this request, doesn't need to configure any option, only add the Id (3fa85f64-5717-4562-b3fc-2c963f66afa6) and in the Response will show:
Deleted

In every request if it happen something, the response it will be an "Error".

Instructions for verifying the setup
===================================
First, check that both projects are running, execute for example the GET requests with Bruno to check if we can have response.
