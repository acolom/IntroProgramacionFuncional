using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace lib.tests.Data.Http
{
    public class HttpService
    {
        private readonly HttpClient client;

        public HttpService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Either<BaseError, CustomerResponse>> GetCustomer(string id)
        {
            var url = $"customer/{id}";

            var response = await client.GetAsync(url);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Either<BaseError, CustomerResponse>.Ok(await response.Content.ReadAsAsync<CustomerResponse>()),
                HttpStatusCode.NotFound => Either<BaseError, CustomerResponse>.Failure(new NotFoundError()),
                HttpStatusCode.BadRequest => Either<BaseError, CustomerResponse>.Failure(new BadRequestError()),
                _ => Either<BaseError, CustomerResponse>.Failure(new UnknownResponseError()),
            };
        }
    }
}
