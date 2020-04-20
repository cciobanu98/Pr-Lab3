using Newtonsoft.Json;
using Notepad.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notepad.Core
{
    public class Client : IClient
    {
        private Uri baseAddress;
        private string proxyAdress = "108.177.235.174";
        private int port = 3128;
        private IWebProxy proxy;
        CookieContainer cookieContainer = new CookieContainer();

        public Client()
        {
            baseAddress = new Uri("https://scribz.net");
            proxy = new WebProxy
            {
                Address = new Uri($"http://{proxyAdress}:{port}"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = true,
            };
        }

        public async Task<bool> CheckConnection()
        {
            try
            {
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, Proxy = proxy })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    //var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, ""));
                    var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Options, ""));
                    result.EnsureSuccessStatusCode();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task CreateNote(NoteViewModel note)
        {
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, Proxy = proxy })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("Title", note.Title),
                    new KeyValuePair<string, string>("Body", note.Body)
                });
                var result = await client.PostAsync("/Note/StoreNote", content);
            }
        }

        public async Task DeleteNote(string id)
        {
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, Proxy = proxy })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var content = new FormUrlEncodedContent(new[]
                   {
                    new KeyValuePair<string, string>("id", id),
                });
                var result = await client.PostAsync("/Note/DeleteNote", content);
            }
        }

        public async Task EditNote(NoteViewModel note)
        {
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, Proxy = proxy })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var notescontent = new 
                {
                    ID = int.Parse(note.Id),
                    Title =note.Title,
                    Body= note.Body,
                    Selection = note.Body.Length
                };
                var st = new
                {
                    n = notescontent,
                    force = false,
                    modBinary = DateTimeOffset.Now.Ticks
                };
                var json = JsonConvert.SerializeObject(st);
                var result = await client.PostAsync("/Note/StoreNote", new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }

        public async Task<NoteViewModel> GetNote(string id)
        {
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, Proxy = proxy })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", id),
                });
                var result = await client.PostAsync("/Note/LoadNote", content);
                result.EnsureSuccessStatusCode();
                var noteJson = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                var notej = JsonConvert.DeserializeObject<JsonNote>(noteJson);
                return notej.Note;
            }
        }

        public async Task<IEnumerable<NoteViewModel>> GetNotes()
        {
            var notes = new List<NoteViewModel>();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, Proxy = proxy })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var result = await client.PostAsync("/Note/LoadStubs", null);
                result.EnsureSuccessStatusCode();
                var stubs = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                JsonResponse json = JsonConvert.DeserializeObject<JsonResponse>(stubs);
                foreach(var note in json.Stubs)
                {
                    notes.Add(new NoteViewModel { Id = note.ID, Title = note.T });
                }
            }
            return notes;
        }

        public async Task<bool> Login(LoginViewModel login)
        {
            try
            {
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, Proxy = proxy })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("UserName", login.Email),
                    new KeyValuePair<string, string>("Password", login.Password)
                });
                    var result = await client.PostAsync("/login", content);
                    result.EnsureSuccessStatusCode();
                    var html = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return IsLogged(html);
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        private bool IsLogged(string html)
        {
            var regex = new Regex(@"<a href=""\/n"" tabindex=""5"" class=""personal notes"" title=""Edit My Scribz™"">(.|\n)*?<\/a>");
            var match = regex.Match(html);
            var valu = match.Value;
            return !string.IsNullOrEmpty(valu);
        }
    }
}
