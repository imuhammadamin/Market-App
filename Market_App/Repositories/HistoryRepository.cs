using Market_App.IRepository;
using Market_App.Models;
using Market_App.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.Repositories
{
    internal class HistoryRepository : IHistoryRepository
    {
        public void Create(History history)
        {
            string histories = File.ReadAllText(Constants.HistoriesPath);

            var historiesJson = JsonConvert.DeserializeObject<IList<History>>(histories);

            historiesJson.Add(history);

            File.WriteAllText(Constants.HistoriesPath, JsonConvert.SerializeObject(historiesJson));
        }

        public List<History> GetHistories()
        {
            string json = File.ReadAllText(Constants.HistoriesPath);

            var histories = JsonConvert.DeserializeObject<List<History>>(json);

            return histories;
        }
    }
}
