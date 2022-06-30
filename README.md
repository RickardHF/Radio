# Radio Free Europe

## Setup 

In order to run this you need to be able to run .NET6 

If you want to test the api with the test client you need to run both the `\RadioFEAPI\RadioFEAPI\RadioFEAPI.csproj` and the `\RadioFEAPI\RadioFETestApp\RadioFETestApp.csproj` at the same time. 

Make sure that the port number in the uri for the variable `RadioFEApi` under `ConnectionStrings` in file `\RadioFEAPI\RadioFETestApp\appsettings.json` is set to the correct port number. (This should be the same as is given to the `RadioFEAPI` when running.

## Disclaimers 

Especcially the test application has not been given much attention. This is ofcourse not meant as a permanent solution and part of the main task and was because of that not given the same amount of attention as the API.

The test api is intended to be used in this fashon: using the same ID for all the available tasks (setting left/right values and getting difference). Also when one has submitted a value, this should not be changed before one do the diff check. The visible representation is dependent on these values remaining the same as what one has submitted in order for it to display correctly.

I was not quite sure what is the intended purpose of the result of the diff check, which could have changed how I implemented it. 
I decided to return the parts of each text which are the same (difference can be deducted from that). 

## Improvement

There are many thing which can be improved on this API. I will list those that come to mind here, however there could be more.

### Add authentication

As of now if this api had been in production on a server where it would be available, everyone could have acceessed it if they wanted. That could create security issues for us, and also result in a far bigger load which in turn would increase cost and availability.

### Dickerize

Depending on how this is intended to be used it could be useful to configure it to run with docker so it can be run as a container instance etc.

### Permanent storage?

I'm not quite sure how this would work in a production setting, however if one is interested in keeping the data submitted for a longer period of time, even if the API goes down, then one should look into creating some permanent storage

### More unit tests

The unit tests only covers a part (the most important part imo) of the solution, and should be extended to cover a larger part.

### Diff check

The diff check for the files is quite quick solution that should be changed to accomodate the need and intended purpose for the data. This could entail a change in the return object

### Remove Base64 encryption?

I don't see a good use for encoding the data into the API to base64 (disclaimer : I don't know the usecase and could be wrong) however it adds extra overlay and it's easier to work with json, both for BE and FE.

If one is afraid of sniffing then it would be better to encode the data with some encryption method.

Alternatively some structure like this:
```
{
  "encryptedData": "<Base64 (or other) string>"
}
```

### Retrive currently "saved" data

Currently there is no possibility to fetch the data that is stored for either left nor right side from the API, which could be useful for the user.

### Support for different encodings

Currently UTF8 is the onøy supported encoding
