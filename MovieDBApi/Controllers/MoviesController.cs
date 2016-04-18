using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace MovieTrailerApi.Controllers
{
    public class MoviesController : ApiController
    {
        private const string _imdbURL = "http://www.imdb.com/xml/find?json=1&nr=1&tt=on&q=%";
        // GET: api/Movies
        public string Get()
        {
            var jsondata = string.Empty;
            using (WebClient client = new WebClient())
            {
                try
                {
                    jsondata = client.DownloadString(_imdbURL);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return jsondata;
        }

        // GET: api/Movies
        public string Get(string title)
        {
            YouTubeService ytservice = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyBQn0LVPvFyMbt-Zgs4i8bcm7U-Obv3KFc" });

            var searchListRequest = ytservice.Search.List("snippet");
            searchListRequest.Q = title + " + Trailer";
            searchListRequest.MaxResults = 1;

            var searchListItems = searchListRequest.Execute();
            return (searchListItems.Items.Count > 0) ? searchListItems.Items[0].Id.VideoId : string.Empty;
        }
    }
}
