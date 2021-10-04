using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cqrs.Domain.NoSQL
{
    public class UpdateNOSQL : IUpdateNOSQL
    {
        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly IAmazonDynamoDB _amazonDynamoDB;
        public UpdateNOSQL(
            IDynamoDBContext dynamoDBContext,
            IAmazonDynamoDB amazonDynamoDB)
        {

            _dynamoDBContext = dynamoDBContext;
            _amazonDynamoDB = amazonDynamoDB;
        }

        public async Task UpdateAsync(VotoResponse voto)
        {
            try
            {
                await _dynamoDBContext.SaveAsync(voto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public static class InitialConfig
    {

        public static void PrepareTablesEnvironments(IAmazonDynamoDB amazonDynamoDB)
        {
            var createTableRequests = new List<CreateTableRequest>();
            createTableRequests.Add(DefaultTableNoSql());


            foreach (var createTable in createTableRequests)
            {
                GetTableDescription(createTable.TableName, createTable, amazonDynamoDB);
            }

        }


        public static TableDescription GetTableDescription(string tableName, CreateTableRequest createTable, IAmazonDynamoDB amazonDynamoDB)
        {
            TableDescription result = null;
            try
            {
                var response = amazonDynamoDB.DescribeTableAsync(tableName).Result; 
                result = response.Table;
            }
            catch (AggregateException e)
            {
                CreateTable_async(createTable, amazonDynamoDB);
            }

            return result;
        }

        public static bool CreateTable_async(CreateTableRequest request, IAmazonDynamoDB amazonDynamoDB)
        {

            try
            {
                var makeTbl = amazonDynamoDB.CreateTableAsync(request).Result;
            }
            catch (Exception e)
            {
                throw;
            }

            return true;
        }


        public static CreateTableRequest DefaultTableNoSql()
        {
            var voto = new Voto();
            return new CreateTableRequest()
            {
                AttributeDefinitions = new List<AttributeDefinition>()  {
                    new AttributeDefinition()
                    {
                        AttributeName = "Guid",
                        AttributeType = ScalarAttributeType.S
                    },
                    new AttributeDefinition()
                    {
                        AttributeName = $"{nameof(voto.Data)}",
                        AttributeType = ScalarAttributeType.S
                    },

                },
                KeySchema = new List<KeySchemaElement>() {
                     new KeySchemaElement()
                    {
                        AttributeName = "Guid",
                        KeyType = KeyType.HASH
                    },
                    new KeySchemaElement()
                    {
                        AttributeName = $"{nameof(voto.Data)}",
                        KeyType = KeyType.RANGE
                    },
                },
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 10
                },
                TableName = nameof(VotoResponse)

            };
        }
    }
}

