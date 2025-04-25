using System;
using System.Collections.Generic;

namespace MVCNetCore;

public class CustomerDetailResponse
{
    public CustomerVM Data { get; set; }
    public string Message { get; set; }
    public string TransactionId { get; set; }
    public CustomerDetailResponse()
    {
        Success = true;
    }
    public bool Success { get; set; }
}
public class CustomerVM
{
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string NPWP { get; set; }
    public string DirectorName { get; set; }
    public string PICName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool AllowAccess { get; set; }
    public string FileNpwp { get; set; }
    public string FilePowerOfAttorey { get; set; }
}