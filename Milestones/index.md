<style>
	section#downloads {
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
		text-align: center;
		width: 33%;
	}
</style>


# SNHU CS-499 Milestones

| [Overview](/CS-499) | Milestones | [Course Outcomes](/CS-499/Course-Outcomes) |

**Table of Contents**

- [Milestone One - Code Review](#milestone-one---code-review)
- [Milestone Two - Software Design & Engineering](#milestone-two---software-design--engineering)
- [Milestone Three - Data Structure & Algorithms](#milestone-three---data-structures--algorithms)
- [Milestone Four - Databases](#milestone-four---databases)

## Milestone One - Code Review

<p class="embed-wrapper">
	<iframe width="560" height="315" src="https://www.youtube.com/embed/Uw7fgN7QZao" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</p>

The video can be found [here](https://youtu.be/Uw7fgN7QZao) on YouTube.

Code reviews are a tool that can be utilized by development teams that provide value in a handful of ways. It can provide a checkpoint for other developers to catch issues that may have been overlooked during development such as null checks, reversed logic gates (e.g. improper boolean operators - `&&` switched with an `||`; wrong comparison operator - `>` instead of `<`; etc.), or ineffecient code flows (e.g. static lists populated inside of a loop). One of the more useful and often overlooked benefits of code reviews is that is provides a way for engineers to share knowledge and skills, such as opportunities to refactor a code block to a distinct method or implement design patterns. 

The original python files can be found [here](https://github.com/mikedarling/CS-499/tree/master/src/Orginal-Project).

Here is a sample of the original python script:
```python
username = "aacuser"
password = "Password4"
shelter = AnimalShelter(username, password)

# class read method must support return of cursor object
#     and accept projection json input
df = pd.DataFrame.from_records(shelter.read({}))

#########################
# Dashboard Layout / View
#########################
app = JupyterDash('SimpleExample')

app.layout = html.Div([
    html.Div(id='hidden-div', style={'display':'none'}),
    html.Center(html.B(html.H1('SNHU CS-340 Dashboard'))),
    html.Hr(),
    dash_table.DataTable(
        id='datatable-id',
        columns=[
            {
            	"name" : i.replace("_", " "),
            	"id": i,
            	"deletable": False,
            	"selectable": True
        	} for i in df.columns
        ],
        #FIXME: Set up the features for your interactive data
        #    table to make it user-friendly for your client
        sort_action= "native"
        filter_action= "native"
        data=df.to_dict('records'),
        page_size=25,
        fixed_rows={
        	'headers': True
        	},
        style_header={
            "height" : 60,
            "text-transform" : "uppercase"
            }
    ),
    html.Br(),
    html.Hr(),
    html.Div(
           id='map-id',
           className='col s12 m6',
           ),
    html.Hr(),
    html.Footer(
        html.Center(
            html.B('Michael Darling | CS-340 | Module 6 Milestone | 2021.12.05')
        )
    )
]);
```

## Milestone Two - Software Design & Engineering

The artifact is an n-tier solution built to deliver an ASP.NET MVC web application. The presentation layer consists of the ASP.NET project itself along with AngularJS to manage the front-end and avoid unnecessary post-backs to the server. The data tier is currently reading the original CSV and parsing the records directly back to entities. The service tier provides all of the communication to the data tier and executes any relevant business logic necessary and constructs the models necessary to the front-end. I’ve been working on it over the course of the past three weeks.

I selected this artifact because it provided a way to showcase an understanding of responsible application architecture. There are a handful of enhancements including the separation of concerns, implementation of dependency injection, improved naming of variables. The observation of separation of concerns provides for a simple replacement of the presentation layer (e.g. a mobile client can be added or could replace the web UI). Likewise, the data repository can be replaced without having to rewrite all of the business logic - this will be demonstrated as part of the third artifact. All dependencies beyond the web tier are managed by a Dependency Injection container so the classes do not need to instantiate the instances of the dependencies and allows them to be passed through the constructor. This also tends to provide more testable classes since a mock implementation of the dependencies can be passed and the testing can focus on the particular class as opposed to the class’s dependencies.

_Demonstration of Seperation of Concerns in the .NET solution_
![Milestone Two - Seperation of Concerns]('/CS-499/assets/SeperationOfConcerns.png')

## Milestone Three - Data Structures & Algorithms

This milestone’s artifact is a continuation of the work submitted for Milestone Two. The biggest change is the addition of a server-side caching mechanism that consists of a cache provider and an abstraction between the service layer and said cache provider. The cache provider can be changed between implementations that leverage standard .NET memory cache, REDIS, or whatever other cache service or library is required. The RepositoryFactory class also has a few basic algorithms. The encapsulated singleton instance is accessed by a publicly available readonly (i.e. there is no setter) property that checks to see if the instance has been initialized before returning the value. It also exposes a Create() method that will return the proper implementation of an interface based on some input value or condition. 

I selected this artifact as a means to demonstrate the ability to implementation of these patterns demonstrates an understanding of well-known and established practices. The ease with which the Cache Provider can be substituted with an alternative implementation, such as REDIS, acknowldeges the potential need for horizontal scaling in production environments and the benefit of centralizing cache and the impact to overall system performance.


## Milestone Four - Databases

This milestone’s artifact is a continuation of the work submitted for Milestone Three. The milestone includes the migration of the data source from the flat CSV file to a normalized code-first EF database, implementation of ASP.NET Membership, a login page, and some role based functionality. Although migrating from the flat file to the database and the decision to use a relational database as opposed to a document-based repository, such as the initial project’s use of MongoDB, carries a performance hit with the increased time for table-spanning queries, the overall benefits outweigh this drawback. Using a flat file is a reasonable choice when only reading from the repository, but it’s the wrong tool for the job if you need to update the dataset. MongoDB, or any other document based repository, helps alleviate this some, but maintains the problem of data maintenance due to lack of normalization. Using Entity Framework also allowed me to rest comfortably with the knowledge that my database is protected from SQL injection attacks and also gave me a way to bolt and Authentication and Authorization framework to restrict access to the application in general and role-restrict selected capabilities.

I selected this artifact to highlight how security can be layered into a system from local file encryption - SQL credentials stored in an encrypted file, network security - SQL instance does not have a public IP and firewall rules to only allow traffic from the application, the implementation of a industry-trusted Identity/Membership platform to enable Authentication and Authorization to the application.