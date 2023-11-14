using System.Threading.Tasks;
using BillService.Dtos;

namespace BillService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendBillToCommand(BillReadDto plat); 
    }
}