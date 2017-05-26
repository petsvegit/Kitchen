using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.FridgeAPI
{
    class FridgeAPIProxy : IFridgeAPIProxy
    {

        private RestClient _fridgeClient;

        public FridgeAPIProxy()
        {
            _fridgeClient = new RestClient("http://localhost:1492");
        }
        
        static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

        public bool IsItemAvaiable(string name, double quantity)
        {
            var request = new RestRequest("api/fridge/{name}/{quantity}", Method.GET);
            request.AddUrlSegment("name", name); // replaces matching token in request.Resource
            request.AddUrlSegment("quantity", quantity);

            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(_fridgeClient, request) as RestResponse;
            }).Wait();

            return (JsonConvert.DeserializeObject<bool>(response.Content));
        }



        public void TakeItemFromFridge(string name, FridgeInventoryItemContract item)
        {
            var request = new RestRequest("api/fridge/" + name, Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(item);

            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(_fridgeClient, request) as RestResponse;
            }).Wait();
        }
    }
}
