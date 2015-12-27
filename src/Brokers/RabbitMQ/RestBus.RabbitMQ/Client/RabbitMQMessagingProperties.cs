﻿using RestBus.Client;
using RestBus.Common.Amqp;
using System;
using System.Collections.Generic;

namespace RestBus.RabbitMQ.Client
{
    /// <summary>
    /// Represents a set of properties which individual messages can present to the RabbitMQ RestBus Client to override
    /// settings in the <see cref="RestBus.Common.Amqp.IMessageMapper"/>, <see cref="RestBus.RabbitMQ.Client.RestBusClient"/> and <see cref="RestBus.Client.RequestOptions"/> classes.
    /// </summary>
    public class RabbitMQMessagingProperties : RequestMessagingProperties
    {
        /// <summary>
        /// Controls if the message is persisted or not.
        /// It has no effect in non-durable (non-persisted) queues.
        /// This overrides the setting specified by <see cref="RestBus.Common.Amqp.IMessageMapper.PersistentMessages"/>.
        /// Set to null to use the setting specified by <see cref="RestBus.Common.Amqp.IMessageMapper.PersistentMessages"/>.
        /// </summary>
        public bool? Persistent { get; set; } //aka DeliveryMode

        /// <summary>
        /// Controls if the message expires in the queue and how long the message stays in the queue before expiring.
        /// This overrides the settings specified by <see cref="RestBus.RabbitMQ.Client.RestBusClient.Timeout"/>, <see cref="RestBus.Common.Amqp.IMessageMapper.GetExpires(System.Net.Http.HttpRequestMessage)"/> and <see cref="RestBus.Client.RequestOptions.Timeout"/>.
        /// Set to <see cref="System.Threading.Timeout.InfiniteTimeSpan"/> to specify that the message never expires.
        /// Set to null, to use the settings specified by <see cref="RestBus.RabbitMQ.Client.RestBusClient.Timeout"/>, <see cref="RestBus.Common.Amqp.IMessageMapper.GetExpires(System.Net.Http.HttpRequestMessage)"/> and <see cref="RestBus.Client.RequestOptions.Timeout"/>
        /// </summary>
        /// <remarks>
        /// The client will still wait for a response within the Timeout period if either <see cref="RestBus.RabbitMQ.Client.RestBusClient.Timeout"/> or <see cref="RestBus.Client.RequestOptions.Timeout"/> is set, even if this property is less than those.
        /// </remarks>
        public TimeSpan? Expiration { get; set; } //Difference between this and RequestOptions.Timeout is that client doesn't use this value to decide how long to wait for a response.

        //TODO: In SendAsync, Make sure ExchangeKind is in the supportedlist in ExchangeInfo.SupportedExchangeKinds

        /// <summary>
        /// Controls what kind of exchange the message is sent to.
        /// This overrides the kind setting specified by <see cref="RestBus.Common.Amqp.IMessageMapper.GetExchangeInfo"/>.
        /// Set to null, to use the kind setting specified by <see cref="RestBus.Common.Amqp.IMessageMapper.GetExchangeInfo"/>.
        /// </summary>
        public ExchangeKind? ExchangeKind { get; set; }

        /// <summary>
        /// Controls the routing key for the message.
        /// This overrides the routing key setting specified by <see cref="RestBus.Common.Amqp.IMessageMapper.GetRoutingKey(System.Net.Http.HttpRequestMessage)"/>.
        /// Set to null to use the routing key setting specified by <see cref="RestBus.Common.Amqp.IMessageMapper.GetRoutingKey(System.Net.Http.HttpRequestMessage)"/>.
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// Controls the headers property for the message.
        /// This is only useful when the exchange is a headers exchange type.
        /// This overrides the headers setting specified by <see cref="RestBus.Common.Amqp.IMessageMapper.GetHeaders(System.Net.Http.HttpRequestMessage)"/>.
        /// Set to null to use the headers setting specified by <see cref="RestBus.Common.Amqp.IMessageMapper.GetHeaders(System.Net.Http.HttpRequestMessage)"/>.
        /// </summary>
        /// <remarks>
        /// This property should not be confused with the <see cref="RestBus.Client.RequestOptions.RequestOptions"/>, which sets the HTTP headers in the message.
        /// </remarks>
        public IDictionary<string, object> Headers { get; set; }

        //TODO: Consider adding a  BasicProperties keyvalue list/dictionary that Client will blindly paste into basic properties after
        //applying other properties in this class
        //The Issue wth this is that RabbitMQ.Client.Framing.BasicProperties does not have a dictionary/key value list/indexer interface where properties can be written to.


    }
}