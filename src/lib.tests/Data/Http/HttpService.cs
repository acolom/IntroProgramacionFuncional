using CSharpFunctionalExtensions;
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

        public async Task<Result<CustomerInfo, BaseError>> GetCustomer(string id)
        {
            var url = $"customer/{id}";

            return await DoCalll(() => client.GetAsync(url))
                .Bind(TreatResponse);

            async Task<Result<CustomerInfo, BaseError>> TreatResponse(HttpResponseMessage response)
            {
                return response.StatusCode switch
                {
                    HttpStatusCode.OK => await response.Content.ReadAsAsync<CustomerInfo>(),
                    HttpStatusCode.NotFound => new NotFoundError(),
                    HttpStatusCode.BadRequest => new BadRequestError(),
                    _ => new UnknownResponseError(),
                };
            }
        }

        public async Task<Result<List<AddressInfo>, BaseError>> GetCustomerAdresses(string id)
        {
            var url = $"customer/{id}/address";
            
            return await DoCalll(() => client.GetAsync(url))
                .Bind(TreatResponse);

            async Task<Result<List<AddressInfo>, BaseError>> TreatResponse(HttpResponseMessage response)
            {
                return response.StatusCode switch
                {
                    HttpStatusCode.OK => await response.Content.ReadAsAsync<List<AddressInfo>>(),
                    HttpStatusCode.NotFound => new NotFoundError(),
                    HttpStatusCode.BadRequest => new BadRequestError(),
                    _ => new UnknownResponseError(),
                };
            }
        }
        public async Task<Result<InvoiceInfo, BaseError>> GetInvoiceInfo(string id, string invoiceId)
        {
            var url = $"customer/{id}/invoices/{invoiceId}";
            
            return await DoCalll(() => client.GetAsync(url))
                .Bind(TreatResponse);


            async Task<Result<InvoiceInfo, BaseError>> TreatResponse(HttpResponseMessage response)
            {
                return response.StatusCode switch
                {
                    HttpStatusCode.OK => await response.Content.ReadAsAsync<InvoiceInfo>(),
                    HttpStatusCode.NotFound => new NotFoundError(),
                    HttpStatusCode.BadRequest => new BadRequestError(),
                    _ => new UnknownResponseError(),
                };
            }
        }

        public async Task<Result<HttpResponseMessage, BaseError>> DoCalll(Func<Task<HttpResponseMessage>> call)
        {
            try
            {
                return await call();
            }
            catch (Exception)
            {
                return new UnknownResponseError();
            }
        }
    }
}
