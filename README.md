#Redburn Technical Test


##Developer Notes
This has been developed and tested using Visual Studio 2017. There are 4 projects in this solution which can be loaded using RunPathTechnicalTest.sln. Running the solution will load up all the required projects and you should see messages appear in the console showing some flow of messages. There is one posted invalid message which will flash up in red on the console - this is by design.

Firstly there's a common class library. While not really necessary in a project of this size I feel its a good habit to try and split out common components.

Then there is the core api project. This listens for gets and a posts of an api trade. I've had to make some assumptions on what a trade object might look like and have put my assumptions in Common.OMSTradeData. This project publishes a message onto a 3rd party NServiceBus which is used to guarantee that all messages are processed regardless of any failure of connection to the SQL backend. I had numerous issues getting this 3rd party working with netcore - it seems they only really support it in >netcore3 which I didn't use as this would require updating the whole project to Visual Studio 2019. For production this would be necessary as I've found myself commenting out bits of code to get it running. The other option (and probably better with some more time and better clarity of requirements) would be to look at other messaging libraries - there are many of them and Microsoft are very keen on pushing their Azure offerings here.

There's then the queue processing project. This listens on the NServiceBus for trade messages and submits them to the sql database using Entity Framework. I'm using a code first model here and so the database should be created automatically when required. The connection string for this defaults to a local SQL instance on a database called Redburn_Lofty using windows authentication. This will need to be a SystemAdmin account as I haven't done anything clever with setting security options. This is coded into the context object - obviously going liev would strip this into an application file which may or may not be shared with other components.

Lastly there's the client app. This reads json data from any .txt file in the \mock folder and posts it to the api. It also issues a get and presents the return result. This is a console app and it just used as the test harness. I orignally dabbled with creating an xUnit test library instead of this, but felt it was better for the purposes of this demo to have something with a bit more UI.

##General Notes
You may notice there's a 5th project. This isn't used but I've left it as its an embyonic (and buggy) Nodejs project to mimic the console app. I wasn't sure how much time to spend on this or if I should have written the whole application in this.
Caching has been implemented in the middle to backend layers. Its possible to configure caching at the api request layer so that data is returned depending on the parameter values passed in. However from experience this is inconsistent depending on calling method, e.g. when running in Visual Studio its ignored the caching, while PostMan does not. I suspect this is just my environment being setup badly (and Google agrees). By taking more programatic control I know it will cache the way I want it to. I've arbitraily set it as a minute, but in production you'd probably want to be cleverer and may choose to look at sqldepends or such like to automatically dump the cache when the underlying data changes.


##Test Scenario

The purpose of this test is to assess the candidate’s abilities in areas we consider key to the type of projects we undertake.

Scenario
You are required to design and build a prototype system to store data from a stream.
The system must offer guaranteed delivery and dead letter capture of malformed or unexpected messages.
You should create a mock feed that includes the most relevant data you would expect from an OMS trade data feed, the data for the mock should be stored in json format.
Features
1)	Data should be stored in Sql Server in a normalized form
2)	The system should be built on microservices principles
3)	The data should be made available via a RESTful API
4)	The data should be cached for a limited period when called from the API
Tech Stack
We would like you to use .net core, Sql Server and optionally Node.js for this project. You may include other technologies.



