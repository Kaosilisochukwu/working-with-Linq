using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_with_json_linq
{
    public interface IRequestFactory
    {
        Root Main { get; set; }

        Task<Root> getDataTask(string completeUrl);
        List<string> GetUsernames(int threshold);
        List<string> GetUsernamesSortedByRecordDate(int threshold);
        string GetUsernameWithHighestCommentCount();
    }
}