using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using cqrs.Domain.NoSQL;
using CQRS.Application.Command;
using CQRS.Application.Commands.Votos;
using CQRS.Application.Queries.Votos;
using CQRS.Consumer;
using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;
using CQRS.Infra.Command.Repositories;
using CQRS.Infra.Query.Repositories;
using CQRS.Publisher;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CQRS.CrossCutting.IoC
{
    public static class ConfigureIOC
    {
        public static IServiceCollection Load(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMassTransit(masstransit =>
            {
                masstransit.AddConsumer<VotoConsumer>();

                masstransit.UsingRabbitMq((context, busFactoryConfigurator) =>
                {

                    busFactoryConfigurator.Host("rabbitmq://localhost:5672/");

                    busFactoryConfigurator.ReceiveEndpoint("voto_queue", endpointCfg =>
                    {
                        endpointCfg.ConfigureConsumer<VotoConsumer>(context);
                    });

                });
            });
 
            var connect = Configuration.GetConnectionString("cqrs-db");
            services.AddDbContext<EntityContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .EnableSensitiveDataLogging()
                    .UseNpgsql(connect, x => x.MigrationsAssembly("CQRS.Infra.Command"))
                    .EnableDetailedErrors(true)
                    .EnableSensitiveDataLogging(true);
            });


            EndpointConvention.Map<VotoResponse>(new Uri("rabbitmq://localhost:5672/voto_queue"));

            services.AddMassTransitHostedService();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
            });


            var credentials = new BasicAWSCredentials
                  (
                      Configuration.GetSection("DynamoDb").GetValue<string>("AccessKey"),
                      Configuration.GetSection("DynamoDb").GetValue<string>("SecretKey")
                  );
            var config = new AmazonDynamoDBConfig()
            {
                ServiceURL = Configuration.GetSection("DynamoDb").GetValue<string>("LocalServiceUrl")
            };

            var client = new AmazonDynamoDBClient(credentials, config);
            services.AddSingleton<IAmazonDynamoDB>(client);
            InitialConfig.PrepareTablesEnvironments(client);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IVotoRepository, VotoRepository>();
            services.AddScoped<IVotoPublisher, VotoPublisher>();

            services.AddScoped<IDynamoDBContext, DynamoDBContext>();
            services.AddScoped<IUpdateNOSQL, UpdateNOSQL>();
            services.AddTransient<IVotoListRepository, VotoListRepository>();

            //MediatR
            services.AddMediatR(typeof(GetVotoQueryHandler).Assembly);
            services.AddMediatR(typeof(ListVotosQueryHandler).Assembly);
            services.AddMediatR(typeof(CreateVotoCommandHandler).Assembly);
    
            return services;
        }
    }
}

