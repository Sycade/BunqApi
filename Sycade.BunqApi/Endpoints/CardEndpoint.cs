﻿using Sycade.BunqApi.Model.Cards;
using Sycade.BunqApi.Responses;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class CardEndpoint : Endpoint
    {
        public CardEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<CardDebit> GetByIdAsync(int cardId, Session session)
        {
            return await ApiClient.DoSignedApiRequestAsync<CardDebit>(HttpMethod.Get, $"user/{session.User.Id}/card/{cardId}", session.Token);
        }

        public async Task<CardDebit[]> GetAllAsync(Session session)
        {
            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/card", session.Token);

            return entities.Cast<CardDebit>().ToArray();
        }
    }
}
