using PosBox.BLL.DTOs;
using PosBox.DAL.Entity_Framework.Table_Models;
using System.Linq;
using System.Collections.Generic;

namespace PosBox.BLL
{
    public class CustomMapper
    {
        // AuditLog mapping (example)
        public static AuditLog Convert(AuditLogDTO a)
        {
            return new AuditLog
            {
                Id = a.Id,
                Action = a.Action,
                Details = a.Details,
                PerformedAt = a.PerformedAt,
                PerformedById = a.PerformedById,
                IsDeleted = a.IsDeleted
            };
        }

        public static AuditLogDTO Convert(AuditLog a)
        {
            return new AuditLogDTO
            {
                Id = a.Id,
                Action = a.Action,
                Details = a.Details,
                PerformedAt = a.PerformedAt,
                PerformedById = a.PerformedById,
                IsDeleted = a.IsDeleted
            };
        }

        //// Product
        //public static Product Convert(ProductDTO dto)
        //{
        //    return new Product
        //    {
        //        Id = dto.Id,
        //        Name = dto.Name,
        //        Cost = dto.Cost,
        //        CostCode = dto.CostCode,
        //        Stock = dto.Stock,
        //        Barcode = dto.Barcode,
        //        ProductImageUrl = dto.ProductImageUrl,
        //        AddingDate = dto.AddingDate,
        //        ExpireDate = dto.ExpireDate,
        //        Comment = dto.Comment,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        CategoryId = dto.CategoryId,
        //        BusinessId = dto.BusinessId,
        //        SupplierId = dto.SupplierId,
        //        EntryInvoiceId = dto.EntryInvoiceId
        //    };
        //}
        //public static ProductDTO Convert(Product entity)
        //{
        //    return new ProductDTO
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //        Cost = entity.Cost,
        //        CostCode = entity.CostCode,
        //        Stock = entity.Stock,
        //        Barcode = entity.Barcode,
        //        ProductImageUrl = entity.ProductImageUrl,
        //        AddingDate = entity.AddingDate,
        //        ExpireDate = entity.ExpireDate,
        //        Comment = entity.Comment,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        CategoryId = entity.CategoryId,
        //        BusinessId = entity.BusinessId,
        //        SupplierId = entity.SupplierId,
        //        EntryInvoiceId = entity.EntryInvoiceId
        //    };
        //}

        //// Category
        //public static Category Convert(CategoryDTO dto)
        //{
        //    return new Category
        //    {
        //        Id = dto.Id,
        //        Name = dto.Name,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static CategoryDTO Convert(Category entity)
        //{
        //    return new CategoryDTO
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// Supplier
        //public static Supplier Convert(SupplierDTO dto)
        //{
        //    return new Supplier
        //    {
        //        Id = dto.Id,
        //        Name = dto.Name,
        //        Address = dto.Address,
        //        PaymentDue = dto.PaymentDue,
        //        Remarks = dto.Remarks,
        //        Phone = dto.Phone,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        BusinessId = dto.BusinessId,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static SupplierDTO Convert(Supplier entity)
        //{
        //    return new SupplierDTO
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //        Address = entity.Address,
        //        PaymentDue = entity.PaymentDue,
        //        Remarks = entity.Remarks,
        //        Phone = entity.Phone,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        BusinessId = entity.BusinessId,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// EntryInvoice
        //public static EntryInvoice Convert(EntryInvoiceDTO dto)
        //{
        //    return new EntryInvoice
        //    {
        //        Id = dto.Id,
        //        NetCost = dto.NetCost,
        //        PaymentDue = dto.PaymentDue,
        //        InvoiceImageUrl = dto.InvoiceImageUrl,
        //        InvoiceDateTime = dto.InvoiceDateTime,
        //        PaymentDueDateTime = dto.PaymentDueDateTime,
        //        Status = dto.Status,
        //        SupplierId = dto.SupplierId,
        //        BusinessId = dto.BusinessId,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static EntryInvoiceDTO Convert(EntryInvoice entity)
        //{
        //    return new EntryInvoiceDTO
        //    {
        //        Id = entity.Id,
        //        NetCost = entity.NetCost,
        //        PaymentDue = entity.PaymentDue,
        //        InvoiceImageUrl = entity.InvoiceImageUrl,
        //        InvoiceDateTime = entity.InvoiceDateTime,
        //        PaymentDueDateTime = entity.PaymentDueDateTime,
        //        Status = entity.Status,
        //        SupplierId = entity.SupplierId,
        //        BusinessId = entity.BusinessId,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// Business
        //public static Business Convert(BusinessDTO dto)
        //{
        //    return new Business
        //    {
        //        Id = dto.Id,
        //        Name = dto.Name,
        //        Address = dto.Address,
        //        Phone = dto.Phone,
        //        Email = dto.Email,
        //        LogoImageUrl = dto.LogoImageUrl,
        //        BusinessUserName = dto.BusinessUserName,
        //        Password = dto.Password,
        //        PreferredLanguage = dto.PreferredLanguage,
        //        PreferredTheme = dto.PreferredTheme,
        //        Cash = dto.Cash,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        LastLogin = dto.LastLogin,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static BusinessDTO Convert(Business entity)
        //{
        //    return new BusinessDTO
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //        Address = entity.Address,
        //        Phone = entity.Phone,
        //        Email = entity.Email,
        //        LogoImageUrl = entity.LogoImageUrl,
        //        BusinessUserName = entity.BusinessUserName,
        //        Password = entity.Password,
        //        PreferredLanguage = entity.PreferredLanguage,
        //        PreferredTheme = entity.PreferredTheme,
        //        Cash = entity.Cash,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        LastLogin = entity.LastLogin,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// Customer
        //public static Customer Convert(CustomerDTO dto)
        //{
        //    return new Customer
        //    {
        //        Id = dto.Id,
        //        Name = dto.Name,
        //        Address = dto.Address,
        //        Due = dto.Due,
        //        Remarks = dto.Remarks,
        //        Phone = dto.Phone,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        BusinessId = dto.BusinessId,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static CustomerDTO Convert(Customer entity)
        //{
        //    return new CustomerDTO
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //        Address = entity.Address,
        //        Due = entity.Due,
        //        Remarks = entity.Remarks,
        //        Phone = entity.Phone,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        BusinessId = entity.BusinessId,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// Transaction
        //public static Transaction Convert(TransactionDTO dto)
        //{
        //    return new Transaction
        //    {
        //        Id = dto.Id,
        //        Type = dto.Type,
        //        Reason = dto.Reason,
        //        TransactionTime = dto.TransactionTime,
        //        Amount = dto.Amount,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        UpdatedBy = dto.UpdatedBy,
        //        UpdatedAt = dto.UpdatedAt,
        //        BusinessId = dto.BusinessId,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static TransactionDTO Convert(Transaction entity)
        //{
        //    return new TransactionDTO
        //    {
        //        Id = entity.Id,
        //        Type = entity.Type,
        //        Reason = entity.Reason,
        //        TransactionTime = entity.TransactionTime,
        //        Amount = entity.Amount,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        UpdatedBy = entity.UpdatedBy,
        //        UpdatedAt = entity.UpdatedAt,
        //        BusinessId = entity.BusinessId,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// Transfer
        //public static Transfer Convert(TransferDTO dto)
        //{
        //    return new Transfer
        //    {
        //        Id = dto.Id,
        //        ProductId = dto.ProductId,
        //        Quantity = dto.Quantity,
        //        TransferTime = dto.TransferTime,
        //        Comment = dto.Comment,
        //        TransferCost = dto.TransferCost,
        //        Status = dto.Status,
        //        FromBusinessId = dto.FromBusinessId,
        //        ToBusinessId = dto.ToBusinessId,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        UpdatedBy = dto.UpdatedBy,
        //        UpdatedAt = dto.UpdatedAt,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static TransferDTO Convert(Transfer entity)
        //{
        //    return new TransferDTO
        //    {
        //        Id = entity.Id,
        //        ProductId = entity.ProductId,
        //        Quantity = entity.Quantity,
        //        TransferTime = entity.TransferTime,
        //        Comment = entity.Comment,
        //        TransferCost = entity.TransferCost,
        //        Status = entity.Status,
        //        FromBusinessId = entity.FromBusinessId,
        //        ToBusinessId = entity.ToBusinessId,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        UpdatedBy = entity.UpdatedBy,
        //        UpdatedAt = entity.UpdatedAt,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// Sell
        //public static Sell Convert(SellDTO dto)
        //{
        //    return new Sell
        //    {
        //        Id = dto.Id,
        //        InvoiceId = dto.InvoiceId,
        //        ProductId = dto.ProductId,
        //        CustomerId = dto.CustomerId,
        //        Quantity = dto.Quantity,
        //        UnitPrice = dto.UnitPrice,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        BusinessId = dto.BusinessId,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static SellDTO Convert(Sell entity)
        //{
        //    return new SellDTO
        //    {
        //        Id = entity.Id,
        //        InvoiceId = entity.InvoiceId,
        //        ProductId = entity.ProductId,
        //        CustomerId = entity.CustomerId,
        //        Quantity = entity.Quantity,
        //        UnitPrice = entity.UnitPrice,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        BusinessId = entity.BusinessId,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// SellInvoice
        //public static SellInvoice Convert(SellInvoiceDTO dto)
        //{
        //    return new SellInvoice
        //    {
        //        Id = dto.Id,
        //        GrossAmount = dto.GrossAmount,
        //        NetAmount = dto.NetAmount,
        //        DiscountTk = dto.DiscountTk,
        //        Due = dto.Due,
        //        InvoiceDateTime = dto.InvoiceDateTime,
        //        PaymentMethod = dto.PaymentMethod,
        //        Comment = dto.Comment,
        //        Cost = dto.Cost,
        //        Profit = dto.Profit,
        //        Status = dto.Status,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        BusinessId = dto.BusinessId,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static SellInvoiceDTO Convert(SellInvoice entity)
        //{
        //    return new SellInvoiceDTO
        //    {
        //        Id = entity.Id,
        //        GrossAmount = entity.GrossAmount,
        //        NetAmount = entity.NetAmount,
        //        DiscountTk = entity.DiscountTk,
        //        Due = entity.Due,
        //        InvoiceDateTime = entity.InvoiceDateTime,
        //        PaymentMethod = entity.PaymentMethod,
        //        Comment = entity.Comment,
        //        Cost = entity.Cost,
        //        Profit = entity.Profit,
        //        Status = entity.Status,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        BusinessId = entity.BusinessId,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// QuickSell
        //public static QuickSell Convert(QuickSellDTO dto)
        //{
        //    return new QuickSell
        //    {
        //        Id = dto.Id,
        //        InvoiceId = dto.InvoiceId,
        //        ProductName = dto.ProductName,
        //        CustomerId = dto.CustomerId,
        //        Quantity = dto.Quantity,
        //        UnitPrice = dto.UnitPrice,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedAt = dto.CreatedAt,
        //        BusinessId = dto.BusinessId,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static QuickSellDTO Convert(QuickSell entity)
        //{
        //    return new QuickSellDTO
        //    {
        //        Id = entity.Id,
        //        InvoiceId = entity.InvoiceId,
        //        ProductName = entity.ProductName,
        //        CustomerId = entity.CustomerId,
        //        Quantity = entity.Quantity,
        //        UnitPrice = entity.UnitPrice,
        //        CreatedBy = entity.CreatedBy,
        //        CreatedAt = entity.CreatedAt,
        //        BusinessId = entity.BusinessId,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// DailyReport
        //public static DailyReport Convert(DailyReportDTO dto)
        //{
        //    return new DailyReport
        //    {
        //        Id = dto.Id,
        //        BusinessName = dto.BusinessName,
        //        NetSell = dto.NetSell,
        //        NetCost = dto.NetCost,
        //        NetProfit = dto.NetProfit,
        //        SellNo = dto.SellNo,
        //        CreatedAt = dto.CreatedAt,
        //        BusinessId = dto.BusinessId,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static DailyReportDTO Convert(DailyReport entity)
        //{
        //    return new DailyReportDTO
        //    {
        //        Id = entity.Id,
        //        BusinessName = entity.BusinessName,
        //        NetSell = entity.NetSell,
        //        NetCost = entity.NetCost,
        //        NetProfit = entity.NetProfit,
        //        SellNo = entity.SellNo,
        //        CreatedAt = entity.CreatedAt,
        //        BusinessId = entity.BusinessId,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}

        //// User
        //public static User Convert(UserDTO dto)
        //{
        //    return new User
        //    {
        //        Id = dto.Id,
        //        UserName = dto.UserName,
        //        Address = dto.Address,
        //        Phone = dto.Phone,
        //        Email = dto.Email,
        //        UserRole = dto.UserRole,
        //        Password = dto.Password,
        //        PreferredLanguage = dto.PreferredLanguage,
        //        PreferredTheme = dto.PreferredTheme,
        //        CreatedAt = dto.CreatedAt,
        //        EmailVerificationCode = dto.EmailVerificationCode,
        //        EmailVerificationExpiry = dto.EmailVerificationExpiry,
        //        LastLogin = dto.LastLogin,
        //        IsDeleted = dto.IsDeleted
        //    };
        //}
        //public static UserDTO Convert(User entity)
        //{
        //    return new UserDTO
        //    {
        //        Id = entity.Id,
        //        UserName = entity.UserName,
        //        Address = entity.Address,
        //        Phone = entity.Phone,
        //        Email = entity.Email,
        //        UserRole = entity.UserRole,
        //        Password = entity.Password,
        //        PreferredLanguage = entity.PreferredLanguage,
        //        PreferredTheme = entity.PreferredTheme,
        //        CreatedAt = entity.CreatedAt,
        //        EmailVerificationCode = entity.EmailVerificationCode,
        //        EmailVerificationExpiry = entity.EmailVerificationExpiry,
        //        LastLogin = entity.LastLogin,
        //        IsDeleted = entity.IsDeleted
        //    };
        //}
    }
}
