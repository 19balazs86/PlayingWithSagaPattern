# Playing with Saga pattern
This repository contains a WebAPI with an endpoint for initiating a Saga. It is implemented using Rebus.

The Saga pattern is meant for long-lived processes in a distributed environment.

SagaPatternWithRebus project has the following process: Subscribe to newsletter -> Send welcome email -> Welcome email sent -> Send follow up email -> Follow up email sent -> Saga is completed

You can find a docker-compose.yml file that will help you quickly set up PostgresDB and RabbitMQ.

#### Resources

- [Saga pattern](https://microservices.io/patterns/data/saga.html) 📓*Microservice architecture* 
- [Implementing the Saga pattern with Rebus and RabbitMQ](https://www.milanjovanovic.tech/blog/implementing-the-saga-pattern-with-rebus-and-rabbitmq) 📓*Milan's newsletter* 
- [Implementing the Saga pattern with Rebus and RabbitMQ](https://youtu.be/dlXCodLxhag) 📽️*18m -* *Milan* 
- [Getting started with Rebus](https://code-maze.com/rebus-dotnet) 📓*Code-Maze*

##### Other solutions for the Saga pattern

- [Saga overview](https://masstransit.io/documentation/patterns/saga) 📓*MassTransit* 
- **Azure Functions** is also a great fit for long-running transactions. Azure Durable Entities can be used as a Saga orchestrator, and it can hold the Saga data. Azure Functions is serverless and provides many features.