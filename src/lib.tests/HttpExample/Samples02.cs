using lib.tests.Data.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Xunit;

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
                onSuccess: (customer) => Console.WriteLine(customer),
                onFailure: treatError);


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

        [Fact]
        public async Task Test_Api_Multiple_Call()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://someservice.net")
            };
            var service = new HttpService(httpClient);


            var customerResult = await service.GetCustomer("1");
            var addressResult = await service.GetCustomerAdresses("1");
            var infoResult = await service.GetInvoiceInfo("1", "InvoiceId");


            var result = from customer in customerResult
                         from address in addressResult
                         from invoice in infoResult
                         select new QuoteVM(customer, address, invoice);

            result.Match(
                quote => Console.Write(quote),
                onFailure: treatError
            );


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
