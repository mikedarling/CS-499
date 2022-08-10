<style>
	section#downloads:empty {
		display: none;
	}

	.inner {
		width:  50%;
	}

	.inner > header, h1 {
		text-align: center;
	}

	p.embed-wrapper {
		text-align: center;
	}

	ol > li > ol {
		margin-left: 20px;
	}

	p {
		color: #484848;
	}

	blockquote {
		font-size: 1rem;
		border-color: #dadada;
	}

	h1 + table {
		margin-top:  1rem;
		margin-bottom: 1rem;
	}

	h1 + table td {
		border: none;
	}
</style>


# SNHU CS-499 Milestones

| [Overview](/CS-499) | Milestones | [Course Outcomes](/CS-499/Course-Outcomes) |

**Table of Contents**

- [Milestone One - Code Review](#milestone-one---code-review)
- [Milestone Two - Software Design & Engineering](#milestone-two---software-design--engineering)
- [Milestone Three - Data Structure & Algorithms](#milestone-three---data-structure--algorithms)
- [Milestone Four - Databases](#milestone-four---databases)

## Milestone One - Code Review

_Code Review Narrative_

<p class="embed-wrapper">
	<iframe width="560" height="315" src="https://www.youtube.com/embed/Uw7fgN7QZao" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</p>

The video can be found [here](https://youtu.be/Uw7fgN7QZao) on YouTube.

## Milestone Two - Software Design & Engineering

_Software Design & Engineering Narrative_

The artifact is an n-tier solution built to deliver an ASP.NET MVC web application. The presentation layer consists of the ASP.NET project itself along with AngularJS to manage the front-end and avoid unnecessary post-backs to the server. The data tier is currently reading the original CSV and parsing the records directly back to entities. The service tier provides all of the communication to the data tier and executes any relevant business logic necessary and constructs the models necessary to the front-end. I’ve been working on it over the course of the past three weeks.

I selected this artifact because it provided a way to showcase an understanding of responsible application architecture. There are a handful of enhancements including the separation of concerns, implementation of dependency injection, improved naming of variables - I left some of the poorly named properties on the model as I’ll be stripping some of them out of the data since they should be represented as calculated values and not fixed data values . The observation of separation of concerns provides for a simple replacement of the presentation layer (e.g. a mobile client can be added or could replace the web UI). Likewise, the data repository can be replaced without having to rewrite all of the business logic - this will be demonstrated as part of the third artifact. All dependencies beyond the web tier are managed by a Dependency Injection container so the classes do not need to instantiate the instances of the dependencies and allows them to be passed through the constructor. This also tends to provide more testable classes since a mock implementation of the dependencies can be passed and the testing can focus on the particular class as opposed to the class’s dependencies.

I believe this artifact covers the fourth outcome pretty well and even touches on aspects of the third. At this time, I don’t foresee a need to alter my plans to cover the course outcomes. Based on what I can see, the first milestone should give coverage to the first two outcomes, the third milestone will cover the third outcome, and the final milestone will cover the fifth outcome. If I’m missing something, I would greatly appreciate any advice or guidance you might have to offer so I can adjust as needed.

Working on this portion of the project has actually been a lot of fun. I was really surprised to see how quickly the CSV parser was able to process the file and return the object array. One of the challenges I faced was trying to manipulate the DateTime properties once they had been passed back to the front-end. I didn’t realize there was a difference between the JSON-serialized format that .NET outputs and what JavaScript expects. This tripped me up for a while and though I was able to get a working solution to display the values and filter by them, the sort doesn’t work. Implementing a custom JSON serializer for the DateTime data type server-side will be a better overall solution than what I currently have in place and I expect it will alleviate the client side issues I was having working with those properties.


## Milestone Three - Data Structures & Algorithms

_Data Structures & Algorithms Narrative_

This milestone’s artifact is a continuation of the work submitted for Milestone Two. The biggest change is the addition of a server-side caching mechanism that consists of a cache provider and an abstraction between the service layer and said cache provider. The cache provider can be changed between implementations that leverage standard .NET memory cache, REDIS, or whatever other cache service or library is required. The RepositoryFactory class also has a few basic algorithms. The encapsulated singleton instance is accessed by a publicly available readonly (i.e. there is no setter) property that checks to see if the instance has been initialized before returning the value. It also exposes a Create() method that will return the proper implementation of an interface based on some input value or condition. The implementation of these patterns demonstrates an understanding of well-known and established practices.


## Milestone Four - Databases

_Databases Narrative_

This milestone’s artifact is a continuation of the work submitted for Milestone Three. The milestone includes the migration of the data source from the flat CSV file to a normalized code-first EF database, implementation of ASP.NET Membership, a login page, and some role based functionality. Although migrating from the flat file to the database and the decision to use a relational database as opposed to a document-based repository, such as the initial project’s use of MongoDB, carries a performance hit with the increased time for table-spanning queries, the overall benefits outweigh this drawback. Using a flat file is a reasonable choice when only reading from the repository, but it’s the wrong tool for the job if you need to update the dataset. MongoDB, or any other document based repository, helps alleviate this some, but maintains the problem of data maintenance due to lack of normalization. Using Entity Framework also allowed me to rest comfortably with the knowledge that my database is protected from SQL injection attacks and also gave me a way to bolt and Authentication and Authorization framework to restrict access to the application in general and role-restrict selected capabilities.