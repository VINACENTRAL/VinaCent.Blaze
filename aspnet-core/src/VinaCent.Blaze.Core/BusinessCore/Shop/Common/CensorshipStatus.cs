namespace VinaCent.Blaze.BusinessCore.Shop.Common
{
    public enum CensorshipStatus
    {
        /// <summary>
        /// Seller was submitted product to system
        /// </summary>
        SUBMITTED,
        /// <summary>
        /// Mod picked that product and reviewing
        /// </summary>
        UNDER_REVIEW,
        /// <summary>
        /// Product was OK and available to Sell
        /// </summary>
        CONFIRMED,
        /// <summary>
        /// Product was deneid
        /// </summary>
        DENIED,
        /// <summary>
        /// Product not OK and violate somethings so need to be remove and notify to user
        /// </summary>
        REMOVED
    }
}
