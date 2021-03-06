﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using EventBus.EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Subscriber.IntegrationEvents.EventHandling;
using Subscriber.IntegrationEvents.Events;
using System;


namespace Subscriber
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

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

     
            var container = new ContainerBuilder();

            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            ConfigureEventBuss(app);
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ.RabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                
                return new EventBusRabbitMQ.RabbitMQ(rabbitMQPersistentConnection, iLifetimeScope,  eventBusSubcriptionsManager);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddTransient<ValueChangedIntegrationEventHandler>();

        }

        private static void ConfigureEventBuss(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<ValueChangedIntegrationEvent, ValueChangedIntegrationEventHandler>();
        }
    }
}
