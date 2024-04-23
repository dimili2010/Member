using System.Text;
using MemberClient.Models;
using Newtonsoft.Json;

namespace MemberClient.APIServices
{
    public class MemberService : IMemberService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MemberService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> DeleteMemberAsync(int memberId)
        {
            var httpClient = _httpClientFactory.CreateClient("MemberClient");
            var requestUrl = $"/api/Member/{memberId}";
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
           
            return true;
        }

        public async Task<MemberViewModel> GetMemberAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("MemberClient");
            var requestUrl = $"/api/Member/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var member = JsonConvert.DeserializeObject<MemberViewModel>(content);
           
            return member;
        }

        public async Task<IEnumerable<MemberViewModel>> GetMembersAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("MemberClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Member");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var memberList = JsonConvert.DeserializeObject<List<MemberViewModel>>(content);

            if (memberList == null || memberList.Count == 0)                          
                return [];            
            return memberList;            
        }

        public async Task<MemberViewModel> InsertMemberAsync(MemberViewModel member)
        {
            var httpClient = _httpClientFactory.CreateClient("MemberClient");            
            var httpContent = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Member")
            {
                Content = httpContent
            };

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var insertedMember = JsonConvert.DeserializeObject<MemberViewModel>(responseContent);            
            return insertedMember;
        }

        public async Task<MemberViewModel> UpdateMemberAsync(MemberViewModel member)
        {
            var httpClient = _httpClientFactory.CreateClient("MemberClient");
            var httpContent = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Member")
            {
                Content = httpContent
            };

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var updatedMember = JsonConvert.DeserializeObject<MemberViewModel>(responseContent);
            return updatedMember;
        }
    }
}
