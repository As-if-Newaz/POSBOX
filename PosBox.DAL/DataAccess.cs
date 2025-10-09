using PosBox.DAL.Entity_Framework;
using PosBox.DAL.Interfaces;
using PosBox.DAL.Repositories;

namespace PosBox.DAL
{
    public class DataAccess
    {
        private readonly ApplicationDBContext db;

        public DataAccess(ApplicationDBContext dbContext)
        {
            db = dbContext;
        }
        public IAuditLog AuditData()
        {
            return new AuditLogRepository(db);
        }

        public IBusinessAccess BusinessAccessData()
        {
            return new BusinessAccessRepository(db);
        }
        
        public IBusiness BusinessData()
        {
            return new BusinessRepository(db);
        }

        public ICategory CategoryData()
        {
            return new CategoryRepository(db);
        }

        public ICustomer CustomerData()
        {
            return new CustomerRepository(db);
        }

        public IDailyReport DailyReportData()
        {
            return new DailyReportRepository(db);
        }

        public IEntryInvoice EntryInvoiceData()
        {
            return new EntryInvoiceRepository(db);
        }

        public IProduct ProductData()
        {
            return new ProductRepository(db);
        }

        public IQuickSell QuickSellData()
        {
            return new QuickSellRepository(db);
        }
        public ISell SellData()
        {
            return new SellRepository(db);
        }

        public ISellInvoice SellInvoiceData()
        {
            return new SellInvoiceRepository(db);
        }

        public IStock StockData()
        {
            return new StockRepository(db);
        }

        public IStockDiscardApplication StockDiscardApplicationData()
        {
            return new StockDiscardApplicationRepository(db);
        }

        public ISupplier SupplierData()
        {
            return new SupplierRepository(db);
        }

        public ITransaction TransactionData()
        {
            return new TransactionRepository(db);
        }

        public ITransfer TransferData()
        {
            return new TransferRepository(db);
        }

        public ITransferInvoice TransferInvoiceData()
        {
            return new TransferInvoiceRepository(db);
        }

        public IUser UserData()
        {
            return new UserRepository(db);
        }

    }
}
