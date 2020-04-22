# CSV Microservice
A simple utility service for converting json data into a csv file format.

The service runs as an Azure Function and takes a POST request containing the following parameters:

| Parameter           | Description                                                                                                | Type         |
|---------------------|------------------------------------------------------------------------------------------------------------|--------------|
| Records             | The JSON records that you would like to convert into a CSV file. These records must be of the same schema. | Object Array |
| IncludeHeaders      | Option to add a row to the beginning of the file to display the headers.                                   | Boolean      |
| FileName (optional) | The name that you would like to give the CSV file. Defaults to Records.csv.                                | String       |


## Example Request Body

```json
{
    "Records": [
        {
            "name": "Tom",
            "favouriteFood": "Pizza"
        },
        {
            "name": "Jerry",
            "favouriteFood": "Cheese"
        }
    ],
    "FileName": "test.csv",
    "IncludeHeaders": true
}
```

## Hosting the Service

Hosting this service is as simple as pushing it to an Azure Functions application. The simplest way to do this is to publish via Visual Studio, though for more commercial usage I'd recommend doing this via a CI/CD pipeline.