using System;
namespace CryptoExchangeTools.Models.ICex;

public class WithdrawalRecord
{
	public required string TxId { get; set; }
    public string? TxHash { get; set; }
    public bool WaitedForApproval { get; set; }
    public decimal RequestedAmount { get; set; }
}

