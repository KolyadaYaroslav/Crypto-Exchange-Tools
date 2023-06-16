using CryptoExchangeTools.Exceptions.Okx;

namespace CryptoExchangeTools.Exceptions.Okx;

[ErrorCode(58002)]
public class SavingsAccountNotActivated : OkxException
{
	public SavingsAccountNotActivated(string message) : base(message)
	{
	}
}

[ErrorCode(58003)]
public class SavingsDoesNotSupportThisCurrencyType : OkxException
{
	public SavingsDoesNotSupportThisCurrencyType(string message) : base(message)
    {
    }
}

[ErrorCode(58004)]
public class AccountBlocked : OkxException
{
	public AccountBlocked(string message) : base(message)
    {
    }
}

[ErrorCode(58005)]
public class TheAmountMustBeEqualToOrLessThanMin : OkxException
{
    public TheAmountMustBeEqualToOrLessThanMin(string message) : base(message)
    {
    }
}

[ErrorCode(58006)]
public class ServiceUnavailableForToken : OkxException
{
    public ServiceUnavailableForToken(string message) : base(message)
    {
    }
}

[ErrorCode(58007)]
public class AssetsInterfaceIsCurrentlyUnavailable : OkxException
{
    public AssetsInterfaceIsCurrentlyUnavailable(string message) : base(message)
    {
    }
}

[ErrorCode(58008)]
public class YouDoNotHaveAssetsInThisCurrency : OkxException
{
    public YouDoNotHaveAssetsInThisCurrency(string message) : base(message)
    {
    }
}

[ErrorCode(58009)]
public class CryptoPairDoesntExist : OkxException
{
    public CryptoPairDoesntExist(string message) : base(message)
    {
    }
}

[ErrorCode(58010)]
public class ChainIsntSupported : OkxException
{
    public ChainIsntSupported(string message) : base(message)
    {
    }
}

[ErrorCode(58011)]
public class ServicesAreUnavailableToUnverifiedUsers : OkxException
{
    public ServicesAreUnavailableToUnverifiedUsers(string message) : base(message)
    {
    }
}

[ErrorCode(58012)]
public class OkxDoesNotSupportAssetTransfersToUnverifiedUsers : OkxException
{
    public OkxDoesNotSupportAssetTransfersToUnverifiedUsers(string message) : base(message)
    {
    }
}

[ErrorCode(58013)]
public class WithdrawalsNotSupportedYet : OkxException
{
    public WithdrawalsNotSupportedYet(string message) : base(message)
    {
    }
}

[ErrorCode(58014)]
public class DepositsNotSupportedYet : OkxException
{
    public DepositsNotSupportedYet(string message) : base(message)
    {
    }
}

[ErrorCode(58015)]
public class TransfersNotSupportedYet : OkxException
{
    public TransfersNotSupportedYet(string message) : base(message)
    {
    }
}

[ErrorCode(58016)]
public class TheApiCanOnlyBeAccessedAndUsedByTheTradingTeamsMainAccount : OkxException
{
    public TheApiCanOnlyBeAccessedAndUsedByTheTradingTeamsMainAccount(string message) : base(message)
    {
    }
}

[ErrorCode(58207)]
public class WithdrawalAddressIsNotWhitelisted : OkxException
{
    public WithdrawalAddressIsNotWhitelisted(string message) : base(message)
    {
    }
}

[ErrorCode(58100)]
public class FundTransferOutFunctionSuspended : OkxException
{
    public FundTransferOutFunctionSuspended(string message) : base(message)
    {
    }

}

[ErrorCode(58101)]
public class TransferSuspended : OkxException
{
    public TransferSuspended(string message) : base(message)
    {
    }

}

[ErrorCode(58102)]
public class RateLimitReached : OkxException
{
    public RateLimitReached(string message) : base(message)
    {
    }

}

[ErrorCode(58103)]
public class AccountTransferFunctionIsTemporarilyUnavailable : OkxException
{
    public AccountTransferFunctionIsTemporarilyUnavailable(string message) : base(message)
    {
    }

}

[ErrorCode(58104)]
public class YouAreRestrictedFromMakingFundTransfers : OkxException
{
    public YouAreRestrictedFromMakingFundTransfers(string message) : base(message)
    {
    }

}

[ErrorCode(58105)]
public class IdentityVerificationException : OkxException
{
    public IdentityVerificationException(string message) : base(message)
    {
    }

}

[ErrorCode(58110)]
public class TransfersAreSuspendedDueToMarketRiskControl : OkxException
{
    public TransfersAreSuspendedDueToMarketRiskControl(string message) : base(message)
    {
    }

}

[ErrorCode(58111)]
public class FundTransfersAreUnavailableWhilePerpetualContractsAreChargingFundingFees : OkxException
{
    public FundTransfersAreUnavailableWhilePerpetualContractsAreChargingFundingFees(string message) : base(message)
    {
    }

}

[ErrorCode(58112)]
public class TransferFailed : OkxException
{
    public TransferFailed(string message) : base(message)
    {
    }

}

[ErrorCode(58114)]
public class TransferAmountMustBeGreaterThan0 : OkxException
{
    public TransferAmountMustBeGreaterThan0(string message) : base(message)
    {
    }

}

[ErrorCode(58115)]
public class SubAccountDoesNotExist : OkxException
{
    public SubAccountDoesNotExist(string message) : base(message)
    {
    }

}

[ErrorCode(58116)]
public class TransferAmountExceedsTheLimit : OkxException
{
    public TransferAmountExceedsTheLimit(string message) : base(message)
    {
    }

}

[ErrorCode(58117)]
public class TransferFailedResolveAnyNegativeAssets : OkxException
{
    public TransferFailedResolveAnyNegativeAssets(string message) : base(message)
    {
    }

}

[ErrorCode(58119)]
public class SubAccountHasNoPermissionToTransferOut : OkxException
{
    public SubAccountHasNoPermissionToTransferOut(string message) : base(message)
    {
    }

}

[ErrorCode(58120)]
public class TransfersAreCurrentlyUnavailable : OkxException
{
    public TransfersAreCurrentlyUnavailable(string message) : base(message)
    {
    }

}

[ErrorCode(58121)]
public class ThisTransferWillResultInAHighRiskLevelOfYourPosition : OkxException
{
    public ThisTransferWillResultInAHighRiskLevelOfYourPosition(string message) : base(message)
    {
    }

}

[ErrorCode(58122)]
public class TransferAmountMayAffectCurrentSpotDerivativesRiskOffsetStructure : OkxException
{
    public TransferAmountMayAffectCurrentSpotDerivativesRiskOffsetStructure(string message) : base(message)
    {
    }

}

[ErrorCode(58123)]
public class TheFromParameterCannotBeTheSameAsTheToParameter : OkxException
{
    public TheFromParameterCannotBeTheSameAsTheToParameter(string message) : base(message)
    {
    }

}

[ErrorCode(58124)]
public class TransferIsBeingProcessedException : OkxException
{
    public TransferIsBeingProcessedException(string message) : base(message)
    {
    }

}

[ErrorCode(58125)]
public class NonTradableAssetsCanOnlyBeTransferredFromSubAccountsToMainAccounts : OkxException
{
    public NonTradableAssetsCanOnlyBeTransferredFromSubAccountsToMainAccounts(string message) : base(message)
    {
    }

}

[ErrorCode(58126)]
public class NonTradableAssetsCanOnlyBeTransferredBetweenFundingAccounts : OkxException
{
    public NonTradableAssetsCanOnlyBeTransferredBetweenFundingAccounts(string message) : base(message)
    {
    }

}

[ErrorCode(58127)]
public class MainAccountApiKeyDoesNotSupportCurrentTransferTypeParameter : OkxException
{
    public MainAccountApiKeyDoesNotSupportCurrentTransferTypeParameter(string message) : base(message)
    {
    }

}

[ErrorCode(58128)]
public class SubAccountApiKeyDoesNotSupportCurrentTransferTypeParameter : OkxException
{
    public SubAccountApiKeyDoesNotSupportCurrentTransferTypeParameter(string message) : base(message)
    {
    }

}

[ErrorCode(58129)]
public class ParamIsErrorOrParamDoesNotMatchWithType : OkxException
{
    public ParamIsErrorOrParamDoesNotMatchWithType(string message) : base(message)
    {
    }

}

[ErrorCode(58131)]
public class WeReUnableToProvideServicesToUnverifiedUsers : OkxException
{
    public WeReUnableToProvideServicesToUnverifiedUsers(string message) : base(message)
    {
    }

}

[ErrorCode(58132)]
public class WeReUnableToProvideServicesToUsersWithBasicVerification_Level1 : OkxException
{
    public WeReUnableToProvideServicesToUsersWithBasicVerification_Level1(string message) : base(message)
    {
    }

}

[ErrorCode(58200)]
public class WithdrawalIsCurrentlyNotSupportedForThisCurrency : OkxException
{
    public WithdrawalIsCurrentlyNotSupportedForThisCurrency(string message) : base(message)
    {
    }

}

[ErrorCode(58201)]
public class WithdrawalAmountExceedsDailyWithdrawalLimit : OkxException
{
    public WithdrawalAmountExceedsDailyWithdrawalLimit(string message) : base(message)
    {
    }

}

[ErrorCode(58202)]
public class TheMinimumWithdrawalAmountForNeoIs1AndTheAmountMustBeAnInteger : OkxException
{
    public TheMinimumWithdrawalAmountForNeoIs1AndTheAmountMustBeAnInteger(string message) : base(message)
    {
    }

}

[ErrorCode(58203)]
public class PleaseAddAWithdrawalAddressException : OkxException
{
    public PleaseAddAWithdrawalAddressException(string message) : base(message)
    {
    }

}

[ErrorCode(58204)]
public class WithdrawalSuspendedDueToYourAccountActivityTriggeringRiskControl : OkxException
{
    public WithdrawalSuspendedDueToYourAccountActivityTriggeringRiskControl(string message) : base(message)
    {
    }

}

[ErrorCode(58205)]
public class WithdrawalAmountExceedsTheUpperLimit : OkxException
{
    public WithdrawalAmountExceedsTheUpperLimit(string message) : base(message)
    {
    }

}

[ErrorCode(58206)]
public class WithdrawalAmountIsLessThanTheLowerLimit : OkxException
{
    public WithdrawalAmountIsLessThanTheLowerLimit(string message) : base(message)
    {
    }

}

[ErrorCode(58208)]
public class WithdrawalFailed : OkxException
{
    public WithdrawalFailed(string message) : base(message)
    {
    }

}

[ErrorCode(58209)]
public class SubAccountsDontSupportWithdrawalsOrDeposits : OkxException
{
    public SubAccountsDontSupportWithdrawalsOrDeposits(string message) : base(message)
    {
    }

}

[ErrorCode(58210)]
public class WithdrawalFeeExceedsTheUpperLimit : OkxException
{
    public WithdrawalFeeExceedsTheUpperLimit(string message) : base(message)
    {
    }

}

[ErrorCode(58211)]
public class WithdrawalFeeIsLowerThanTheLowerLimit : OkxException
{
    public WithdrawalFeeIsLowerThanTheLowerLimit(string message) : base(message)
    {
    }

}

[ErrorCode(58212)]
public class WithdrawalFeeException : OkxException
{
    public WithdrawalFeeException(string message) : base(message)
    {
    }

}

[ErrorCode(58213)]
public class TheInternalTransferAddressIsIllegal : OkxException
{
    public TheInternalTransferAddressIsIllegal(string message) : base(message)
    {
    }

}

[ErrorCode(58214)]
public class WithdrawalsSuspendedDueToChainMaintenance : OkxException
{
    public WithdrawalsSuspendedDueToChainMaintenance(string message) : base(message)
    {
    }

}

[ErrorCode(58215)]
public class WithdrawalIdDoesNotExist : OkxException
{
    public WithdrawalIdDoesNotExist(string message) : base(message)
    {
    }

}

[ErrorCode(58216)]
public class OperationNotAllowed : OkxException
{
    public OperationNotAllowed(string message) : base(message)
    {
    }

}

[ErrorCode(58217)]
public class WithdrawalsAreTemporarilySuspendedForYourAccountDueToARiskDetectedInYourWithdrawalAddress : OkxException
{
    public WithdrawalsAreTemporarilySuspendedForYourAccountDueToARiskDetectedInYourWithdrawalAddress(string message) : base(message)
    {
    }

}

[ErrorCode(58218)]
public class TheInternalWithdrawalFailed : OkxException
{
    public TheInternalWithdrawalFailed(string message) : base(message)
    {
    }

}

[ErrorCode(58219)]
public class YouCannotWithdrawCryptoWithin24HoursAfterChangingCredentials : OkxException
{
    public YouCannotWithdrawCryptoWithin24HoursAfterChangingCredentials(string message) : base(message)
    {
    }

}

[ErrorCode(58220)]
public class WithdrawalRequestAlreadyCanceled : OkxException
{
    public WithdrawalRequestAlreadyCanceled(string message) : base(message)
    {
    }

}

[ErrorCode(58221)]
public class TheToaddrParameterFormatIsIncorrect : OkxException
{
    public TheToaddrParameterFormatIsIncorrect(string message) : base(message)
    {
    }

}

[ErrorCode(58222)]
public class InvalidWithdrawalAddress : OkxException
{
    public InvalidWithdrawalAddress(string message) : base(message)
    {
    }

}

[ErrorCode(58223)]
public class ThisIsAContractAddressWithHigherWithdrawalFees : OkxException
{
    public ThisIsAContractAddressWithHigherWithdrawalFees(string message) : base(message)
    {
    }

}

[ErrorCode(58224)]
public class ThisCryptoCurrentlyDoesnTSupportOnChainWithdrawalsToOkxAddresses : OkxException
{
    public ThisCryptoCurrentlyDoesnTSupportOnChainWithdrawalsToOkxAddresses(string message) : base(message)
    {
    }

}

[ErrorCode(58225)]
public class AssetTransfersToUnverifiedUsersAreNotSupportedDueToLocalLawsAndRegulations : OkxException
{
    public AssetTransfersToUnverifiedUsersAreNotSupportedDueToLocalLawsAndRegulations(string message) : base(message)
    {
    }

}

[ErrorCode(58226)]
public class ChainIsDelistedAndNotAvailableForCryptoWithdrawal : OkxException
{
    public ChainIsDelistedAndNotAvailableForCryptoWithdrawal(string message) : base(message)
    {
    }

}

[ErrorCode(58227)]
public class WithdrawalOfNonTradableAssetsCanBeWithdrawnAllAtOnceOnly : OkxException
{
    public WithdrawalOfNonTradableAssetsCanBeWithdrawnAllAtOnceOnly(string message) : base(message)
    {
    }

}

[ErrorCode(58228)]
public class WithdrawalOfNonTradableAssetsRequiresThatTheApiKeyMustBeBoundToAnIp : OkxException
{
    public WithdrawalOfNonTradableAssetsRequiresThatTheApiKeyMustBeBoundToAnIp(string message) : base(message)
    {
    }

}

[ErrorCode(58229)]
public class InsufficientFundingAccountBalanceToPayFees : OkxException
{
    public InsufficientFundingAccountBalanceToPayFees(string message) : base(message)
    {
    }

}

[ErrorCode(58230)]
public class YouWillNeedToCompleteYourIdentityVerification_Level1 : OkxException
{
    public YouWillNeedToCompleteYourIdentityVerification_Level1(string message) : base(message)
    {
    }

}

[ErrorCode(58231)]
public class TheRecipientHasNotCompletedPersonalInfoVerification_Level1 : OkxException
{
    public TheRecipientHasNotCompletedPersonalInfoVerification_Level1(string message) : base(message)
    {
    }

}

[ErrorCode(58232)]
public class YouVeReachedWithdrawalLimit : OkxException
{
    public YouVeReachedWithdrawalLimit(string message) : base(message)
    {
    }
}

[ErrorCode(58233)]
public class UnableToProvideServicesToUnverifiedUsers : OkxException
{
    public UnableToProvideServicesToUnverifiedUsers(string message) : base(message)
    {
    }

}

[ErrorCode(58234)]
public class TheRecipientCantReceiveYourTransferYet : OkxException
{
    public TheRecipientCantReceiveYourTransferYet(string message) : base(message)
    {
    }

}

[ErrorCode(58235)]
public class UnableToProvideServicesToUsersWithBasicVerification_Level1 : OkxException
{
    public UnableToProvideServicesToUsersWithBasicVerification_Level1(string message) : base(message)
    {
    }

}

[ErrorCode(58236)]
public class ARecipientWithBasicVerificationIsUnableToReceiveYourTransfer : OkxException
{
    public ARecipientWithBasicVerificationIsUnableToReceiveYourTransfer(string message) : base(message)
    {
    }

}

[ErrorCode(58300)]
public class DepositAddressCountExceedsTheLimit : OkxException
{
    public DepositAddressCountExceedsTheLimit(string message) : base(message)
    {
    }

}

[ErrorCode(58301)]
public class DepositAddressNotExist : OkxException
{
    public DepositAddressNotExist(string message) : base(message)
    {
    }

}

[ErrorCode(58302)]
public class DepositAddressNeedsTag : OkxException
{
    public DepositAddressNeedsTag(string message) : base(message)
    {
    }

}

[ErrorCode(58303)]
public class DepositForChainIsCurrentlyUnavailable : OkxException
{
    public DepositForChainIsCurrentlyUnavailable(string message) : base(message)
    {
    }

}

[ErrorCode(58304)]
public class FailedToCreateInvoice : OkxException
{
    public FailedToCreateInvoice(string message) : base(message)
    {
    }

}

[ErrorCode(58305)]
public class UnableToRetrieveDepositAddress : OkxException
{
    public UnableToRetrieveDepositAddress(string message) : base(message)
    {
    }

}

[ErrorCode(58306)]
public class VerificationNeededForDeposit : OkxException
{
    public VerificationNeededForDeposit(string message) : base(message)
    {
    }

}

[ErrorCode(58307)]
public class DepositLimitHitForPersonalVerification : OkxException
{
    public DepositLimitHitForPersonalVerification(string message) : base(message)
    {
    }

}

[ErrorCode(58308)]
public class CantDepositForUnverifiedUsers : OkxException
{
    public CantDepositForUnverifiedUsers(string message) : base(message)
    {
    }

}

[ErrorCode(58309)]
public class CantDepositForUnverifiedUsers_Level1 : OkxException
{
    public CantDepositForUnverifiedUsers_Level1(string message) : base(message)
    {
    }

}

[ErrorCode(58350)]
public class InsufficientBalance : OkxException
{
    public InsufficientBalance(string message) : base(message)
    {
    }

}

[ErrorCode(58351)]
public class InvoiceExpired : OkxException
{
    public InvoiceExpired(string message) : base(message)
    {
    }

}

[ErrorCode(58352)]
public class InvalidInvoice : OkxException
{
    public InvalidInvoice(string message) : base(message)
    {
    }

}

[ErrorCode(58353)]
public class DepositAmountMustBeWithinLimits : OkxException
{
    public DepositAmountMustBeWithinLimits(string message) : base(message)
    {
    }

}

[ErrorCode(58354)]
public class YouHaveReachedTheDailyLimitOf10000Invoices : OkxException
{
    public YouHaveReachedTheDailyLimitOf10000Invoices(string message) : base(message)
    {
    }

}

[ErrorCode(58355)]
public class PermissionDenied : OkxException
{
    public PermissionDenied(string message) : base(message)
    {
    }

}

[ErrorCode(58356)]
public class TheAccountsOfTheSameNodeDoNotSupportTheLightningNetworkDepositOrWithdrawal : OkxException
{
    public TheAccountsOfTheSameNodeDoNotSupportTheLightningNetworkDepositOrWithdrawal(string message) : base(message)
    {
    }

}

[ErrorCode(58358)]
public class TheFromccyParameterCannotBeTheSameAsTheToccyParameter : OkxException
{
    public TheFromccyParameterCannotBeTheSameAsTheToccyParameter(string message) : base(message)
    {
    }

}

[ErrorCode(58370)]
public class YouHaveExceededTheDailyLimitOfSmallAssetsConversion : OkxException
{
    public YouHaveExceededTheDailyLimitOfSmallAssetsConversion(string message) : base(message)
    {
    }

}

[ErrorCode(58371)]
public class SmallAssetsAmountExceedsTheMaximumLimit : OkxException
{
    public SmallAssetsAmountExceedsTheMaximumLimit(string message) : base(message)
    {
    }

}

[ErrorCode(58372)]
public class InsufficientSmallAssets : OkxException
{
    public InsufficientSmallAssets(string message) : base(message)
    {
    }

}