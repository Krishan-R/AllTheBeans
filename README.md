# All The Beans API

This project is built with C# and .NET 9, utilising the Web SDK in order to build out a basic, yet scalable, API.

I decided on this framework due to my familiarity with it from my professional career, and generally find it enjoyable
to write and develop.

I have tried to build this project out as if I were writing for my job where it would be deployed to customers, however
there are a few small bits that I would've included if more time were spent.

## Project Setup

To run and debug this project:

1. Clone the repository
2. Restore the NuGet packages, either with Visual Studio or the `dotnet restore` command
3. Ensure that configuration is correct in the `appsettings.json`, particularly the database connection string.
4. Run/Debug with Visual Studio
5. The application should be available at [http://localhost:8080](http://localhost:8080)

To run with Docker/Podman compose:

1. Ensure you have Docker/Podman compose installed
2. Clone the repository
3. From the root of the repository run the command `docker compose -f compose.yaml up -d`
4. The application should be available at [http://localhost:8080](http://localhost:8080)

This will either pull or build the relevant images before spinning up containers.

> Note, you may have issues running the MS SQL database container from an ARM based device.

If you make any changes to the project, you may need to delete the existing image to force docker compose to rebuild.

This can be done with the following command:

```docker image rm allthebeans```

## Database

As I am familiar with MS SQL database through my current workplace, I decided on using this database rather than another
such as PostgreSQL or MySQL.

Although I don't have a great deal of experience with Entity Framework, I decided on using this to handle my database
migrations and seeding. This proved to be a bit of a learning curve, however the project is in a better state than
without.

I have experience working with SQL projects and DACPACs however decided against this as I wanted to expand my knowledge
into EF Core.

Although it is not best practise for the connection string to use the `sa` user, I did this out of simplicity for this
project. Ideally, there should be a limited user account (or service account) to connect to the database.

## API and Controllers

I decided on using controllers for the API rather than the newer 'minimal' APIs that .NET encourages. This is because I
find that they make it much clearer to the engineers the intention of the code, and where it can easily be found. This
means that they should be able to scale better as more endpoints are added and controllers are expanded.

Each endpoint in the controller has basic error handling, and does not contain any business logic. This is instead
handled by the relevant service which is called by the controller.

I made the conscious design choice when making the controllers to make the application `async`. This is best decided
early on in the project as it can be annoying introducing async/await to an existing codebase. Having asynchronous code
allows for better thread management than synchronous code, allowing for better scaling. Although this makes very
little (or no) difference at this stage of the project, introducing it now will reduce headache in the future.

In the future, this controller could be expanded to include authentication and authorisation to ensure that endpoints
are only accessible to the correct users/roles/permissions.

Another possibility is to include pagination to API results as more beans are added to the database. This could end up
improving performance, both for the database and any UI calling the API.

## Services

The services are where the business logic is executed. For most of the endpoints and the methods that they call, there
is very little business logic required, and so these methods directly call the repository to retrieve the data from the
database.

For the Bean of the Day logic however, there is extra logic in order to determine the new Bean of the Day, ensuring that
it does not use the bean chosen for yesterday. This was initially implemented in the repository however I wasn't happy
with this and refactored the logic and updated the tests to move it to the service. This makes the project more
hexagonal, allowing for switching the repository implementation out for another in the future if required.

In these methods, I have included logs that could help identify and solve issues or bugs, however because these methods
are very basic I did not want to overload the logging.

## Repositories

The BeanRepository is where all the database interaction occurs. As I had scaffolded the project and database with
EntityFramework Core, I have used the `DbContext` to retrieve and update the database. This is something that I am not
extremely familiar with, and am much more comfortable writing raw SQL in the form of stored procedures when interacting
with databases.

If I had more time, I would have made another implementation of the `IBeanRepository` using SQL to demonstrate my
abilities. This can however be slightly seen in the `BeanTestBase` in the integration test project with some very basic
SQL.

## Testing

I have included both a Unit and Integration test project to ensure code coverage and to verify that the logic is working
as expected.

The unit test project is built upon NUnit 3 and Moq, as this is what I use on a daily basis at my workplace.

The integration tests utilise the Testcontainers package to spin up containers with a database server where tests are
ran against. I have configured this so that each test has its own database container, ensuring a clean database without
any potential data conflicts. This also allows tests to run in parallel to speed up test execution. I am a huge fan of
this package and try to utilise it whenever possible for integration tests.

Ideally, the unit tests should be ran on every PR as part of the gate, and then both unit and integration tests should
be ran as part of the main build.

## Observability

I have included some basic observability tools into this project - logging, metrics, traces, and health checks.

Logging is fairly basic as this is handled and configured by the WebApplicationBuilder. In the past, my teams have used
packages such as Serilog or Log4Net to enhance logging capabilities, however I decided against that as the Microsoft
logging framework was suitable for this project.

I have included basic metrics using the OpenTelemetry instrumentation. This is an extremely useful, yet often
underappreciated, tool where engineers can track many different metrics. In the future, this project can be expanded out
to include custom metrics, such as the number of times errors are generated.

Tracing is by far my favourite observability tool. There is basic tracing setup with the AspNetCore Instrumentation,
however this really shines when adding in custom activities/spans and tags to help navigate debugging deployed systems.
Context propagation across different services is an amazing benefit when using traces, and I would have certainly
implemented this if I had time to also complete the frontend task.

I have added in a basic health check to ensure that the API can communicate with the database. This is something fairly
minor yet useful, especially when deploying to an environment such as kubernetes. I have also forwarded the health check
to the metrics server to give history as to when the database is not accessible from the service.
