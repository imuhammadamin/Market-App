using Market_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_App.IRepository
{
    internal interface IHistoryRepository
    {
        void Create(History history);
        List<History> GetHistories();
    }
}
