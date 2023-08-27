using InventorySystem.Data;
using InventorySystem.Data.Enum;
using System.Diagnostics.Metrics;

namespace InventorySystem.Models
{
    public class vmStores
    {
        public StoresDTO Store { set; get; }
        public List<Company> LiCompany { set; get; }

    }
}
