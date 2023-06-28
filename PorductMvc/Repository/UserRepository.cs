using PorductMvc.ViewModel;

namespace PorductMvc.Repository
{
    public class UserRepository 
    {
        Uri baseaddress = new Uri("https://localhost:44397/api/User");
        HttpClient client = new HttpClient();
        public UserRepository()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }





        public HttpResponseMessage GetResponse(string url)
        {
            return client.GetAsync(url).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return client.PostAsJsonAsync(url, model).Result;
        }
    }
}
 
