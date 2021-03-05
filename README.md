# RWS Moravia homework 
## Data Converter Console application
Version: 2020-04-08
### Prerequisites

You need an [Azure subscription][azure_sub] and
Storage Account to use the Azure-related features of this application.

To create a new Storage Account, you can use the Azure Portal or the Azure CLI.
Here's an example using the Azure CLI:

```Powershell
az storage account create --name MyStorageAccount --resource-group MyResourceGroup --location westus --sku Standard_LRS
```

Get a connection string to our Azure Storage account.  You can
obtain your connection string from the Azure Portal (click
Access Keys under Settings in the Portal Storage account blade)
or using the Azure CLI with:
```Powershell
az storage account show-connection-string --name <account_name> --resource-group <resource_group>
```
[azure_sub]: https://azure.microsoft.com/

### Initial state of application

```c#
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Moravia.Homework
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

            try
            {
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                var reader = new StreamReader(sourceStream);
                string input = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerializeObject(doc);

            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);


        }
    }
}
```

