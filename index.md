<style>
	.inner {
		width:  50%;
	}

	.inner > header {
		text-align: center;
	}

	p.embed-wrapper {
		text-align: center;
	}
</style>

# SNHU CS-499 ePortfolio

_Overview_

**Table of Contents**

1. [Professional Assessment](#professional-assessment)
2. [Milestones](#milestones)
  1. [Milestone One - Code Review](#milestone-one---code-review)
  2. [Milestone Two - Software Design & Engineering](#milestone-two---software-design--engineering)
  3. [Milestone Three - Data Structure & Algorithms](#milestone-three---data-structure--algorithm)
  4. [Milestone Four - Databases](#milestone-four---databases)
3. [Course Outcomes](#course-outcomes)
  1. [Outcome One](#outcome-one)
  2. [Outcome Two](#outcome-two)
  3. [Outcome Three](#outcome-three)
  4. [Outcome Four](#outcome-four)
  5. [Outcome Five](#outcome-five)

## Professional Assessment

_Professional Assessment_



## Milestones


### Milestone One - Code Review

_Code Review Narrative_

<p class="embed-wrapper">
	<iframe width="560" height="315" src="https://www.youtube.com/embed/Uw7fgN7QZao" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</p>

### Milestone Two - Software Design & Engineering

_Software Design & Engineering Narrative_

The artifact is an n-tier solution built to deliver an ASP.NET MVC web application. The presentation layer consists of the ASP.NET project itself along with AngularJS to manage the front-end and avoid unnecessary post-backs to the server. The data tier is currently reading the original CSV and parsing the records directly back to entities. The service tier provides all of the communication to the data tier and executes any relevant business logic necessary and constructs the models necessary to the front-end. I’ve been working on it over the course of the past three weeks.

I selected this artifact because it provided a way to showcase an understanding of responsible application architecture. There are a handful of enhancements including the separation of concerns, implementation of dependency injection, improved naming of variables - I left some of the poorly named properties on the model as I’ll be stripping some of them out of the data since they should be represented as calculated values and not fixed data values . The observation of separation of concerns provides for a simple replacement of the presentation layer (e.g. a mobile client can be added or could replace the web UI). Likewise, the data repository can be replaced without having to rewrite all of the business logic - this will be demonstrated as part of the third artifact. All dependencies beyond the web tier are managed by a Dependency Injection container so the classes do not need to instantiate the instances of the dependencies and allows them to be passed through the constructor. This also tends to provide more testable classes since a mock implementation of the dependencies can be passed and the testing can focus on the particular class as opposed to the class’s dependencies.

I believe this artifact covers the fourth outcome pretty well and even touches on aspects of the third. At this time, I don’t foresee a need to alter my plans to cover the course outcomes. Based on what I can see, the first milestone should give coverage to the first two outcomes, the third milestone will cover the third outcome, and the final milestone will cover the fifth outcome. If I’m missing something, I would greatly appreciate any advice or guidance you might have to offer so I can adjust as needed.

Working on this portion of the project has actually been a lot of fun. I was really surprised to see how quickly the CSV parser was able to process the file and return the object array. One of the challenges I faced was trying to manipulate the DateTime properties once they had been passed back to the front-end. I didn’t realize there was a difference between the JSON-serialized format that .NET outputs and what JavaScript expects. This tripped me up for a while and though I was able to get a working solution to display the values and filter by them, the sort doesn’t work. Implementing a custom JSON serializer for the DateTime data type server-side will be a better overall solution than what I currently have in place and I expect it will alleviate the client side issues I was having working with those properties.


### Milestone Three - Data Structures & Algorithms

_Data Structures & Algorithms Narrative_

This milestone’s artifact is a continuation of the work submitted for Milestone Two. The biggest change is the addition of a server-side caching mechanism that consists of a cache provider and an abstraction between the service layer and said cache provider. The cache provider can be changed between implementations that leverage standard .NET memory cache, REDIS, or whatever other cache service or library is required. The RepositoryFactory class also has a few basic algorithms. The encapsulated singleton instance is accessed by a publicly available readonly (i.e. there is no setter) property that checks to see if the instance has been initialized before returning the value. It also exposes a Create() method that will return the proper implementation of an interface based on some input value or condition. The implementation of these patterns demonstrates an understanding of well-known and established practices.


### Milestone Four - Databases

_Databases Narrative_

This milestone’s artifact is a continuation of the work submitted for Milestone Three. The milestone includes the migration of the data source from the flat CSV file to a normalized code-first EF database, implementation of ASP.NET Membership, a login page, and some role based functionality. Although migrating from the flat file to the database and the decision to use a relational database as opposed to a document-based repository, such as the initial project’s use of MongoDB, carries a performance hit with the increased time for table-spanning queries, the overall benefits outweigh this drawback. Using a flat file is a reasonable choice when only reading from the repository, but it’s the wrong tool for the job if you need to update the dataset. MongoDB, or any other document based repository, helps alleviate this some, but maintains the problem of data maintenance due to lack of normalization. Using Entity Framework also allowed me to rest comfortably with the knowledge that my database is protected from SQL injection attacks and also gave me a way to bolt and Authentication and Authorization framework to restrict access to the application in general and role-restrict selected capabilities.


## Course Outcomes



### Outcome One

>Employ strategies for building collaborative environments that enable diverse audiences to support organizational decision making in the field of computer science.

This outcome was supported through the code review submitted as the first milestone. I raised several concerns with various aspects of overall system architecture, development practices, and coding standards. The suggestions are setup in a way that should make them clearly actionable and work items (User Stories, Bugs, PBIs, etc) can be created and refined.


### Outcome Two

> Design, develop, and deliver professional-quality oral, written, and visual communications that are coherent, technically sound, and appropriately adapted to specific audiences and contexts.

This outcome was also demonstrated by the initial milestone. The code review was geared toward fellow engineers and delivered in a constructive manner. The items that were presented as stylistic or preferential in nature were highlighted as such, including items such as the preference for less nesting and the use of “Guard Clauses”.


### Outcome Three

> Design and evaluate computing solutions that solve a given problem using algorithmic principles and computer science practices and standards appropriate to its solution, while managing the trade-offs involved in design choices.

The third milestone demonstrates proficiency in this outcome via use of basic data retrieval decisioning with the addition of server side caching, the abstract IReadableDataRepository Factory, as well as the encapsulation and lazy instantiation of the Singleton instance of the Factory.


### Outcome Four

> Demonstrate an ability to use well-founded and innovative techniques, skills, and tools in computing practices for the purpose of implementing computer solutions that deliver value and accomplish industry-specific goals.

The second milestone demonstrated several aspects of proficiency related to this outcome. The project includes observance of separation of concerns using an n-tier architecture, SOLID coding (S => MVC Controllers are thin, L =>  IDataRepository, I => IDataRepository refactored to IReadableDataRepository, D => Dependencies injected via Unity DI Container), and Gang-of-Four OOP Design Patterns (Abstract Factory, Singleton). 


### Outocome Five

> Develop a security mindset that anticipates adversarial exploits in software architecture and designs to expose potential vulnerabilities, mitigate design flaws, and ensure privacy and enhanced security of data and resources.

The final milestone of the course included the addition of Entity Framework, which helps prevent SQL injections by automatically parameterizing the queries, ASP.NET Membership to apply Authentication and Authorization rules, as well as the protection of the database credentials - the “production” environment’s credentials are not stored in source control and the file that contains them in that environment is also encrypted with keys specific to that App Service host (there’s no server, per se, it’s being hosted as an Azure AppService).