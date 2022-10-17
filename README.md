# Soumith Chittajallu - Experian Coding Exercise 

## Endpoints
* Get all albums = https://localhost:5001/albums
* Get albums by user id = https://localhost:5001/{userid}/albums

## Discussion points/possible improvements 
* We query the photos endpoint by appending each albumid to the query string. There's a limit to how many album id's can be used because at a point,
the url length will exceed its limit. Need to account for this. 
* Add logging functionaliy - log start of a controller request, status codes from API calls, errors. 
* Exception handling - handle failure logic and retry logic for external API calls. 
* Separate Albums returned from the external APIs, and Albums created by the service which contains the Photos
    * e.g. a class named ExternalAlbum which has json annotations but doesn't have Photos field, then another named Album that does have Photos field
    (without json annotations). 
    * methods can clearly be named differently, types can be clearer
    * one class returned to client and another class which is bounded by interface of the external api. That way if the external api changes we can
    easily change the external api json class without impacting the services interface. 
    * also makes it clear that there is an Album that contains Photos, and an ExternalAlbum that does not. 
