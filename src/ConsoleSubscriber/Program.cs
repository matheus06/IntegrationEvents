using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConsoleSubscriber.IntegrationEvents.EventHandling;
using EventBus;
using EventBus.EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Subscriber.IntegrationEvents.Events;
using System;


namespace ConsoleSubscriber
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var services = new ServiceCollection();
            
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "nvr1010",
                    UserName = "metalurgia",
                    Password = "metalurgia"
                };

                return new DefaultRabbitMQPersistentConnection(factory);
            });

            RegisterEventBus(services);

            var builder = new ContainerBuilder();
            builder.Populate(services);
            var container = builder.Build();
            

            using (var scope = container.BeginLifetimeScope())
            {
                var eventBus = scope.Resolve<IEventBus>();
                eventBus.Subscribe<ValueChangedIntegrationEvent, ValueChangedIntegrationEventHandler>();
            }

        }

        private static void RegisterEventBus(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ.RabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQ.RabbitMQ(rabbitMQPersistentConnection, iLifetimeScope, eventBusSubcriptionsManager);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddTransient<ValueChangedIntegrationEventHandler>();

        }
    }
    
}
