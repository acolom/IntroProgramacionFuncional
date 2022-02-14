using lib.tests.Data.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace lib.tests.HttpExample
{
    public class Samples02
    {
        public async Task Test_Api_Call()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://someservice.net")
            };
            var service = new HttpService(httpClient);

            
            var customerEither = await service.GetCustomer("pepe");

            customerEither.Match(
                success: (customer) => Console.WriteLine(customer),
                error: treatError);

            
            void treatError(BaseError error)
            {
                switch (error)
                {
                    case NotFoundError notFound:
                        Console.WriteLine($"NotFound: {notFound}");
                        break;
                    case BadRequestError badRequest:
                        Console.WriteLine($"BadRequest: {badRequest}");
                        break;
                    case UnknownResponseError unknownResponse:
                        Console.WriteLine($"UnknownResponse: {unknownResponse}");
                        break;
                    default:
                        throw new Exception("Unknown type");
                }
            }

        }
    }
}
