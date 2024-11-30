# Playing with Saga pattern
This repository contains a WebAPI with an endpoint for initiating a Saga, implemented with Rebus.

The Saga pattern is meant for long-lived processes in a distributed environment.

SagaPatternWithRebus project has the following process: Subscribe to newsletter -> Send welcome email -> Welcome email sent -> Send follow up email -> Follow up email sent -> Saga is completed

You can find a docker-compose.yml file that will help you quickly set-up PostgresDB and RabbitMQ.

#### Resources

- [Saga pattern](https://microservices.io/patterns/data/saga.html) 📓*Microservice architecture*
- [Implementing the Saga pattern with Rebus and RabbitMQ](https://www.milanjovanovic.tech/blog/implementing-the-saga-pattern-with-rebus-and-rabbitmq) 📓*Milan's newsletter*
- [Implementing the Saga pattern with Rebus and RabbitMQ](https://youtu.be/dlXCodLxhag) 📽️*18m -* *Milan*
- [Error handling with compensating transactions](https://youtu.be/FPVzevl6Ri8) 📽️*15m -* *Milan*
- 📓Code-Maze
  - [Getting started with Rebus](https://code-maze.com/rebus-dotnet)
  - [Saga pattern using Rebus and RabbitMQ](https://code-maze.com/dotnet-implementing-the-saga-pattern-using-rebus-and-rabbitmq)


#### Other solutions for the Saga pattern

- [Saga overview](https://masstransit.io/documentation/patterns/saga) 📓*MassTransit*
  - [Implementing the Saga pattern with MassTransit and RabbitMQ](https://www.milanjovanovic.tech/blog/implementing-the-saga-pattern-with-masstransit) 📓*Milan's newsletter*
- [Saga](https://wolverine.netlify.app/guide/durability/sagas.html) using [Wolverine](https://wolverine.netlify.app) *(next generation .NET Mediator and Message Bus)* and [MartenDB](https://martendb.io) *(Transactional DocumentDB and EventStore on PostgreSQL)*
  - Using Saga in my repository: [PlayingWithWolverineMarten](https://github.com/19balazs86/PlayingWithWolverineMarten)
- **Azure Functions** is also a great fit for long-running transactions. Azure Durable Entities can be used as a Saga orchestrator, and it can hold the Saga data. Azure Functions is serverless and provides many features.

##### Other

- [Stateless library to make State Machines and visualize with UmlDotGraph](https://khalidabuhakmeh.com/state-machines-light-switches-and-space-travel-with-stateless-and-dotnet-8) 📓*Khalid Abuhakmeh*