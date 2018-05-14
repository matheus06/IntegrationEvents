using RabbitMQ.Client;
using System;
using System.IO;

namespace EventBusRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        bool _disposed;
        IConnection _connection;
        private readonly IConnectionFactory _connectionFactory;

        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
            }
        }

        public bool TryConnect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (IsConnected)
            {
                return true;
            }

            return false;

        }


    }
}
