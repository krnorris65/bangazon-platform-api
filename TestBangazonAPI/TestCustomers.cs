using System;
using System.Net;
using Newtonsoft.Json;
using Xunit;
using BangazonAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestBangazonAPI
{
    public class TestCustomers
    {
        [Fact]
        public async Task Test_Get_All_Customers()
        {
            using (var client = new APIClientProvider().Client)
            {

                var response = await client.GetAsync("/api/customers");


                string responseBody = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<Customer>>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(customers.Count > 0);
            }
        }

        [Fact]
        public async Task Test_Get_All_Customers_Search()
        {
            using(var client = new APIClientProvider().Client)
            {
                var response = await client.GetAsync("/api/customers?q=ib");

                string responseBody = await response.Content.ReadAsStringAsync();

                var customers = JsonConvert.DeserializeObject<List<Customer>>(responseBody);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Liè", customers[0].FirstName);
                Assert.Equal("Dible", customers[0].LastName);

            }
        }

        [Fact]
        public async Task Test_Get_Single_Customers()
        {
            using (var client = new APIClientProvider().Client)
            {

                var response = await client.GetAsync("/api/customers/2");


                string responseBody = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(2, customer.Id);
                Assert.Equal("Liè", customer.FirstName);
                Assert.Equal("Dible", customer.LastName);

            }
        }
    }
}
