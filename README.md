# Routes

If you have questions, comments, recommendations or wishes, please leave them here.

#### Task

The test task consists of two parts, the main part, and a bonus part. We suggest tackling the bonus part once the main objective of the service has been achieved.

The task is to build a JSON over HTTP API endpoint that takes as input two IATA/ICAO airport codes and provides as output a route between these two airports so that:

The route consists of at most 4 legs/flights (that is, 3 stops/layovers, if going from A->B, a valid route could be A->1->2->3->B, or for example A->1->B etc.) and;

The route is the shortest such route as measured in kilometers of geographical distance.

For the bonus part, extend your service so that it also allows changing airports during stops that are within 100km of each other. For example, if going from A->B, a valid route could be A->1->2=>3->4->B, where “2=>3” is a change of airports done via ground. These switches are not considered as part of the legs/layover/hop count, but their distance should be reflected in the final distance calculated for the route.

Notes:

The weekdays and flight times are not important for the purposes of the test task - you are free to assume that all flights can depart at any required time

You are free to choose any publicly available airport and flight/route database

You are free to choose to use any open-source libraries

You are free to choose any programming language (TypeScript/Node is preferred, but not mandatory)

You can ask additional questions

#### Implementation

ASP.NET Core and .NET 6.
Route search is done using Dijkstra algorithm with Fibonacci heap (Application project).
CSV files with airports and routes information are located in Persistence project.

#### Tests

NUnit, Moq
Test project contains unit and integration tests. The majority of tests are written to make sure the main functionality is working properly. All the code is not covered with unit tests. The example of class fully covered with unit tests is RouteFinder (class itself: Application project; unit tests: Test project/Unit/Application/RouteFinderTests).

#### API

Example GET request: http://localhost:5179/routes/?source=DME,UUDD&destination=FCO,LIRF (when debugging from VS) or
http://localhost:5000/routes/?source=DME,UUDD&destination=FCO,LIRF (when starting exe from bin folder).
Tested GET request with Postman.
If returned route distance is decimal.MaxValue (79,228,162,514,264,337,593,543,950,335), there is no connection between airports.
