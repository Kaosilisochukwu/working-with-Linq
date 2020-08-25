using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rest_with_json_linq
{
    public class RequestFactory : IRequestFactory
    {
        /// <summary>
        /// holds value for the data recieved from the API call
        /// </summary>
        public Root Main { get; set; } = null;

        /// <summary>
        /// This function would retrieve the names of the most active authors (using submission count as the criteria) according to a set threshold.
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns>Names of most active authors</returns>
        public List<string> GetUsernames(int threshold)
        {
            return Main.Data.OrderByDescending(person => person.SubmissionCount)
                            .Where(count => count.SubmissionCount >= threshold)
                            .Select(c => c.Username)
                            .ToList();
        }

        /// <summary>
        /// This function would retrieve the name of the author with the highest comment count.
        /// </summary>
        /// <returns>Author with highest comment count</returns>
        public string GetUsernameWithHighestCommentCount()
        {
            var bestAuthor = Main.Data.OrderByDescending(author => author.CommentCount)
                                      .Select(author => author.Username)
                                      .FirstOrDefault();
            return bestAuthor;
        }

        /// <summary>
        /// This function would retrieve the names of authors sorted by when their record was created according to a set threshold.
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public List<string> GetUsernamesSortedByRecordDate(int threshold)
        {
            var earliestAuthor = Main.Data.Where(author => UnixTimeStampToDateTime(author.CreatedAt) <= UnixTimeStampToDateTime(threshold))
                                          .Select(author => author.Username).ToList();
            return earliestAuthor;
        }


        /// <summary>
        /// Retrieves data from an API call
        /// </summary>
        /// <param name="completeUrl"></param>
        /// <returns>A serialized json object</returns>
        public async Task<Root> getDataTask(string completeUrl)
        {
            //Created a new instance of HttpClient and assigned it to client
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            //sends  a get request to the url and gets a response
            var result = await client.GetAsync(completeUrl);
            result.EnsureSuccessStatusCode();

            //checks if call was successful
            if (result.StatusCode == HttpStatusCode.OK)
            {
                //transforms the content of the return value of the get to a string
                var json = await result.Content.ReadAsStringAsync();

                //sets an option for the Deserializatioin of string content to be case insensitive
                var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                //Deserialize the json to and object of Root assigns it to the Main Property and returns it
                return Main = JsonSerializer.Deserialize<Root>(json, option);
            }
            return Main = null;
        }


        /// <summary>
        /// Gets the utc DateTime of an integer value of seconds
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns>DateTime from Utc time stamp</returns>
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
