using System;
using System.Net;
using Newtonsoft.Json;
using Xunit;
using BangazonAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TestBangazonAPI
{
    public class TestPaymentTypes
    {
        [Fact]
        public async Task Test_Get_All_PaymentTypes()
        {
            using (var client = new APIClientProvider().Client)
            {

                var response = await client.GetAsync("/api/paymentTypes");


                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentTypes = JsonConvert.DeserializeObject<List<PaymentType>>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(paymentTypes.Count > 0);
                Assert.NotNull(paymentTypes[0].Customer);
            }
        }
        [Fact]
        public async Task Test_Get_Single_PaymentType()
        {
            using (var client = new APIClientProvider().Client)
            {
                var response = await client.GetAsync("api/paymentTypes/3");

                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentType = JsonConvert.DeserializeObject<PaymentType>(responseBody);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(3, paymentType.Id);
                Assert.Equal(16797, paymentType.AcctNumber);
                Assert.Equal("diners-club-enroute", paymentType.Name);
                Assert.NotNull(paymentType);

            }
        }

        [Fact]
        public async Task Test_Create_And_Delete_PaymentType()
        {
            using (var client = new APIClientProvider().Client)
            {
                PaymentType newPaymentType = new PaymentType
                {
                    Name = "Visa",
                    AcctNumber = 12345,
                    CustomerId = 1
                };

                var PaymentAsJSON = JsonConvert.SerializeObject(newPaymentType);

                var response = await client.PostAsync(
                    "/api/paymentTypes",
                    new StringContent(PaymentAsJSON, Encoding.UTF8, "application/json")
                );
                string responseBody = await response.Content.ReadAsStringAsync();
                var NewVisa = JsonConvert.DeserializeObject<PaymentType>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.Equal(newPaymentType.Name, NewVisa.Name);
                Assert.Equal(newPaymentType.AcctNumber, NewVisa.AcctNumber);
                Assert.Equal(newPaymentType.CustomerId, NewVisa.CustomerId);

                /*
                    ACT
                */
                var deleteResponse = await client.DeleteAsync($"/api/paymentTypes/{NewVisa.Id}");

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Modify_PaymentType()
        {
            string NewPaymentTypeName = "AmEx";

            using (var client = new APIClientProvider().Client)
            {
                /*
                    PUT section
                 */
                PaymentType ModifiedPayment = new PaymentType
                {
                    Name = NewPaymentTypeName,
                    AcctNumber = 28845,
                    CustomerId = 2
                };
                var ModifiedPaymentAsJSON = JsonConvert.SerializeObject(ModifiedPayment);

                var response = await client.PutAsync(
                    "/api/paymentTypes/2",
                    new StringContent(ModifiedPaymentAsJSON, Encoding.UTF8, "application/json")
                );
                string responseBody = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                /*
                    GET section
                 */
                var GetVisa = await client.GetAsync("/api/paymentTypes/2");
                GetVisa.EnsureSuccessStatusCode();

                string GetPaymentBody = await GetVisa.Content.ReadAsStringAsync();
                PaymentType NewVisa = JsonConvert.DeserializeObject<PaymentType>(GetPaymentBody);

                Assert.Equal(HttpStatusCode.OK, GetVisa.StatusCode);
                Assert.Equal(NewPaymentTypeName, NewVisa.Name);
            }
        }

        [Fact]
        public async Task Test_Get_NonExistant_PaymentType()
        {
            using (var client = new APIClientProvider().Client)
            {
                var response = await client.GetAsync("/api/paymentTypes/9999999");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Delete_NonExistant_PaymentType()
        {
            using(var client = new APIClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/paymentTypes/9999999");
                Assert.False(response.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Delete_PaymentType_On_Order()
        {
            using (var client = new APIClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/paymentTypes/1");

                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }
    }
}