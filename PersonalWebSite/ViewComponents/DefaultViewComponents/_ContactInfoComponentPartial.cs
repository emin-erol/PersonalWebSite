using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ContactInfoDtos;

namespace PersonalWebSite.ViewComponents.DefaultViewComponents
{
    public class _ContactInfoComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
		public _ContactInfoComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ContactInfos");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactInfoDto>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}
