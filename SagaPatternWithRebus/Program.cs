using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace SagaPatternWithRebus;

public static class Program
{
    private const string _inputQueue = "rebus-saga-queue";

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        IServiceCollection services   = builder.Services;
        IConfiguration configuration  = builder.Configuration;

        // Add services to the container
        {
            services.AddSingleton<IEmailService, EmailService>();

            services.AddRebus(rebus => configureRebus(rebus, configuration));

            services.AutoRegisterHandlersFromAssemblyOf<EmailService>();
        }

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline
        {
            app.MapGet("/newsletter/{email}", async ([FromRoute] string email, IBus bus) =>
            {
                await bus.Send(new Initiate_SubscribeToNewsletter(email));

                return Results.Accepted();
            });
        }

        writeConsoleLog(app.Logger);

        app.Run();
    }

    private static RebusConfigurer configureRebus(RebusConfigurer rebus, IConfiguration configuration)
    {
        string rabbitMqConnString = configuration.GetConnectionString("RebusRabbitMQ")!;
        string postgresConnString = configuration.GetConnectionString("RebusPostgreSQL")!;

        return rebus
            .Routing(configureRouting)
            .Transport(transport => transport.UseRabbitMq(rabbitMqConnString, _inputQueue))
            .Sagas(saga => saga.StoreInPostgres(postgresConnString, dataTableName: "Sagas", indexTableName: "SagaIndexes"))
            .Timeouts(timeout => timeout.StoreInPostgres(postgresConnString, tableName: "Timeouts"));
    }

    private static void configureRouting(StandardConfigurer<Rebus.Routing.IRouter> router)
    {
        //router.TypeBased().MapAssemblyOf<SendEmailConfirmation>(_inputQueue);

        router
            .TypeBased()
            .Map<FollowUpEmail_Sent>(_inputQueue)
            .Map<FollowUpEmail_Send>(_inputQueue)
            .Map<WelcomeEmail_Send>(_inputQueue)
            .Map<WelcomeEmail_Send>(_inputQueue)
            .Map<Initiate_SubscribeToNewsletter>(_inputQueue)
            .Map<WelcomeEmail_Sent>(_inputQueue);
    }

    private static void writeConsoleLog(ILogger logger)
    {
        new TaskFactory().StartNew(async () =>
        {
            await Task.Delay(1_500);

            logger.LogWarning("RabbitMQ management -> http://localhost:15672");
            logger.LogWarning("Open the URL -> http://localhost:5000/newsletter/balazs@domain.com");
        });
    }
}
