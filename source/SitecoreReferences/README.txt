Every project in this solution uses $(SitecoreLibDir) 
parameter for dependent reference path.

This parameter can be updated in the following file:
    SitecoreReferences\Sitecore.Reference.Path.xml
After updating the parameter, restart Visual Studio 
so that project references are reloaded.

Note that .\Lib\Sitecore.Strategy.Contacts.dll is an external reference from:
    https://github.com/adamconn/sitecore-contact-utilities
