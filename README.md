# sitecore-ingestion-manager
This module adds components that make it easier to get external data into Sitecore xDB. 

You can [watch a video](https://www.youtube.com/watch?v=r2oaZEcOWVA) that shows what this module does.

## Packages
In order to make the various components that make up this module managable, this module is broken up into a number of separate package.

### xDB Ingestion Manager
This package installs the module. After installing this package you have a framework that must be configured before it will do anything. The module is configured exposed under ```/sitecore/system/Modules/xDB Ingestion Manager```.

### Personal Info Connector
This package installs a connector that is able to set data on the contact facets based on the contract ```IContactPersonalInfo``` from URL-encoded strings. The connector can be used for pull and push data handlers.

### Sample Personal Info Connector
This package installs data handlers that populate the contact facet ```Personal```.

1. *Pull Data Handler* - Reads data from a file whose location is specified on the data reader ```File System Data Reader```.
2. Push Data Handler - Reads data from the query string for requests to ```http://[YOUR SITECORE SERVER/-/push/personal```.

### Demandbase Connector
This package adds a new contact facet named ```DemandbaseData```. It also adds the templates and types needed to configure a data reader that can make requests to the Demandbase API.

*NOTE: You need a Demandbase API key in order to use the Demandbase API.*

### Sample Demandbase Connector
This package installs a pull data handler that populates the contact facet named ```DemandbaseData``` using data read from the Demandbase API.

*NOTE: You need a Demandbase API key in order to use the Demandbase API.*

### Bizo Connector
This package adds a new contact facet named ```BizoData```. It also adds the templates and types needed to configure a data hydrator that can populate items that inherit from the interface ```IBizoData```.

*NOTE: You need a Bizo account in order to use the Bizo API.*

### Sample Bizo Connector
This package installs a push data handler that populates the contact facet named ```BizoData``` using data from the query string for requests to ```http://[YOUR SITECORE SERVER/-/push/bizo```.

*NOTE: You need a Bizo account in order to use the Bizo API.*

## Download
The installation packages are available [here](https://github.com/adamconn/sitecore-ingestion-manager/tree/master/sitecore/).