using System.Threading.Tasks;
using ChargeMe.API.DataBase;
using Microsoft.Azure.Cosmos;
using telemetry_api.Import.Model;

namespace telemetry_api.Import.Service
{
    public interface IImportService
    {
        public Task UploadTelemetry(MoistureSQL val);
    }
    public class ImportService : IImportService
    {
        ITelemetryRepo _repo;
        DBContext _context;
        public ImportService(ITelemetryRepo repo, DBContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task UploadTelemetry(MoistureSQL val)
        {
            //await _repo.TelemetryContainer.CreateItemAsync(val, new PartitionKey(val.DeviceId));
            var newTransaction = await _context.Moisture.AddAsync(val);
            await _context.SaveChangesAsync();

        }
    }
}
