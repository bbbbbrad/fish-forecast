# fish-forecast

A Windows Form Application written in C# which is used to forecast the fish activity for the day.
The desktop application requires three inputs which are location latitude, longitude, and the date.

The application uses that data to send an api request and then downloads the request data in json format.
The json data is then deserialized and assigned to its label where the data is then output.
