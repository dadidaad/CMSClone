using CMSClone.Client.Features;
using CMSClone.Shared;
using CMSClone.Shared.DTOs;
using CMSClone.Shared.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CMSClone.Client.HttpRepositories.Implements
{
    public class CourseJoinHttpRepository : ICourseJoinHttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public CourseJoinHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, ReferenceHandler = ReferenceHandler.Preserve };
        }

        public async Task<PagingRespone<CourseJoinDTO>> GetCourseJoin(RequestParameters requestParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = requestParameters.PageNumber.ToString(),
                ["searchTerm"] = requestParameters.SearchTerm == null ? "" : requestParameters.SearchTerm,
                ["orderBy"] = requestParameters.OrderBy
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("coursejoin/getcoursesjoin", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingRespone<CourseJoinDTO>
            {
                Items = JsonSerializer.Deserialize<List<CourseJoinDTO>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };
            return pagingResponse;
        }

        public async Task<List<CourseJoinDTO>> GetTeacherInCourse(Guid courseId)
        {
            var response = await _client.GetAsync("coursejoin/getcoursesjoinwithteacher?courseId=" + courseId, CancellationToken.None);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            return JsonSerializer.Deserialize<List<CourseJoinDTO>>(content, _options);
        }
    }
}
