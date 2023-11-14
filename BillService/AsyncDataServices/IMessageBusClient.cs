using BillService.Dtos;

namespace BillService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewBill(BillPublishedDto billPublishedDto);
    }
}