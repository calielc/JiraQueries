using System.Collections.Generic;
using System.Threading.Tasks;
using JiraQueries.Bussiness.Models;

namespace JiraQueries.Bussiness.Services
{
    public interface IJiraService
    {
        string Project { get; set; }
        Task<IEnumerable<IssueViewModel>> Load();
    }
}