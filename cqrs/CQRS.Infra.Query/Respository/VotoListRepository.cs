using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Infra.Query.Repositories
{
    public class VotoListRepository : IVotoListRepository
    {

        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly IAmazonDynamoDB _amazonDynamoDB;
        public VotoListRepository(
            IDynamoDBContext dynamoDBContext,
            IAmazonDynamoDB amazonDynamoDB)
        {

            _dynamoDBContext = dynamoDBContext;
            _amazonDynamoDB = amazonDynamoDB;
        }

        public async Task<VotoResponse> FindByIdAsync(int Id)
        {
            try
            {
                var scanconditions = new List<ScanCondition>();
                scanconditions.Add(new ScanCondition("Id", ScanOperator.Equal, Id));

                var response = await _dynamoDBContext.ScanAsync<VotoResponse>(scanconditions).GetNextSetAsync();

                return response.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<object> ListAsync()
        {
            try
            {
                var scanconditions = new List<ScanCondition>();
                scanconditions.Add(new ScanCondition("Id", ScanOperator.NotEqual, 0));

                var response = await _dynamoDBContext.ScanAsync<VotoResponse>(scanconditions).GetNextSetAsync();

                var total = response.OrderBy(v => v.Data).Count();

                var result = from voto in response.AsEnumerable()
                        group voto by voto.Opcao into valores
                        select new
                        {
                            totalDeVotos = total,
                            opcaoVotada = valores.Key,
                            count = valores.Count(),
                            votos = valores.Take(10)
                        };

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
