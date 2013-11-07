ourspace
========

Ourspace platform

The OurSpace platform consists of multiple DNN modules that are required in order for the platform to offer all expected functionalities.

All the modules should be installed under a valid DNN v.05.06.01 (238) installation. Other versions of DNN may also be compatible, however this has not been thoroughly tested.

The database script ourspace_specific.sql contains all tables required in order for the modules to function and should be executed prior to module installation.

Follow this steps to install the modules:

1. Install DNN 5.06.01 (238)  (available from here https://dotnetnuke.codeplex.com/releases/view/59419)
2. Add the following lines in your web.config inside the 'appSettings' section:
   
   `<!-- Settings for joinOurspace -->`
   `<add key="OurspaceDomain" value="THE INSTALLATION DOMAIN E.G www.joinourspace.eu" />`
    `<!-- end joinOurspace --> `

   You must adjust the 'value' attribute to match your domain name

3. Run ourspace_specific.sql (You can use 'Host > SQL' to access the database from within the portal)
4. Install the modules using the install zip files found in the packages folder of each module.
5. Import the platform template using the 'OurSpace Platform Template.template' file in order to create all necessary pages
6. Load the web application in a browser
