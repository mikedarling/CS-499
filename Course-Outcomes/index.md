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



# SNHU CS-499 Course Outcomes

| [Overview](/CS-499) | [Milestones](/CS-499/Milestones) | Course Outcomes |

**Table of Contents**

- [Outcome One](#outcome-one)
- [Outcome Two](#outcome-two)
- [Outcome Three](#outcome-three)
- [Outcome Four](#outcome-four)
- [Outcome Five](#outcome-five)


## Outcome One

>Employ strategies for building collaborative environments that enable diverse audiences to support organizational decision making in the field of computer science.

This outcome was supported through the code review submitted as the first milestone. I raised several concerns with various aspects of overall system architecture, development practices, and coding standards. The suggestions are setup in a way that should make them clearly actionable and work items (User Stories, Bugs, PBIs, etc) can be created and refined.


## Outcome Two

> Design, develop, and deliver professional-quality oral, written, and visual communications that are coherent, technically sound, and appropriately adapted to specific audiences and contexts.

This outcome was also demonstrated by the initial milestone. The code review was geared toward fellow engineers and delivered in a constructive manner. The items that were presented as stylistic or preferential in nature were highlighted as such, including items such as the preference for less nesting and the use of “Guard Clauses”.


## Outcome Three

> Design and evaluate computing solutions that solve a given problem using algorithmic principles and computer science practices and standards appropriate to its solution, while managing the trade-offs involved in design choices.

The third milestone demonstrates proficiency in this outcome via use of basic data retrieval decisioning with the addition of server side caching, the abstract IReadableDataRepository Factory, as well as the encapsulation and lazy instantiation of the Singleton instance of the Factory.


## Outcome Four

> Demonstrate an ability to use well-founded and innovative techniques, skills, and tools in computing practices for the purpose of implementing computer solutions that deliver value and accomplish industry-specific goals.

The second milestone demonstrated several aspects of proficiency related to this outcome. The project includes observance of separation of concerns using an n-tier architecture, SOLID coding (S => MVC Controllers are thin, L =>  IDataRepository, I => IDataRepository refactored to IReadableDataRepository, D => Dependencies injected via Unity DI Container), and Gang-of-Four OOP Design Patterns (Abstract Factory, Singleton). 


## Outocome Five

> Develop a security mindset that anticipates adversarial exploits in software architecture and designs to expose potential vulnerabilities, mitigate design flaws, and ensure privacy and enhanced security of data and resources.

The final milestone of the course included the addition of Entity Framework, which helps prevent SQL injections by automatically parameterizing the queries, ASP.NET Membership to apply Authentication and Authorization rules, as well as the protection of the database credentials - the “production” environment’s credentials are not stored in source control and the file that contains them in that environment is also encrypted with keys specific to that App Service host (there’s no server, per se, it’s being hosted as an Azure AppService).