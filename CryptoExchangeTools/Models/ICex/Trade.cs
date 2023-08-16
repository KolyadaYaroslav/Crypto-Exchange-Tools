namespace CryptoExchangeTools.Models.ICex;

public enum OrderDirection
{
    Buy,
    Sell
}

public enum CalculationBase
{
    /// <summary>
    /// The amount will be calculated in currencyIn
    /// </summary>
    Base,

    /// <summary>
    /// The amount will be calculated in currencyOut
    /// </summary>
    Quote
}

internal class HelperMethods
{
    internal static CalculationBase ReverseCalculationBase(CalculationBase calculationBase)
    {
        if (calculationBase == CalculationBase.Base)
            return CalculationBase.Quote;

        else return CalculationBase.Base;
    }

    internal static OrderDirection ReverseOrderDirection(OrderDirection direction)
    {
        if (direction == OrderDirection.Buy)
            return OrderDirection.Sell;

        else return OrderDirection.Buy;
    }
}
