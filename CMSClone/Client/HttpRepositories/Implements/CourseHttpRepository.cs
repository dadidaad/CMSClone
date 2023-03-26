using CMSClone.Shared.Models;
using System.Text.Json;
using System.Text;
using CMSClone.Shared;
using CMSClone.Client.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace CMSClone.Client.HttpRepositories.Implements
{
    public class CourseHttpRepository : ICourseHttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public CourseHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task CreateCourse(CourseDTO courseDTO)
        {
            var content = JsonSerializer.Serialize(courseDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("courses", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<PagingRespone<CourseDTO>> GetCourseByCreator(RequestParameters requestParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = requestParameters.PageNumber.ToString(),
                ["searchTerm"] = requestParameters.SearchTerm == null ? "" : requestParameters.SearchTerm,
                ["orderBy"] = requestParameters.OrderBy
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("courses/getcoursesforcreator", queryStringParam));

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingRespone<CourseDTO>
            {
                Items = JsonSerializer.Deserialize<List<CourseDTO>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };
            return pagingResponse;
        }
    }
}
