using System.ComponentModel;

namespace IntTrackerCrossPlatformMobile.Pages
{
    public enum AutofillHintType
    {
        [Description("")]
        None,
        [Description("creditCardExpirationDate")]
        CreditCardExpirationDate,
        [Description("creditCardExpirationDay")]
        CreditCardExpirationDay,
        [Description("creditCardExpirationMonth")]
        CreditCardExpirationMonth,
        [Description("creditCardExpirationYear")]
        CreditCardExpirationYear,
        [Description("creditCardNumber")]
        CreditCardNumber,
        [Description("creditCardSecurityCode")]
        CreditCardSecurityCode,
        [Description("emailAddress")]
        EmailAddress,
        [Description("name")]
        Name,
        [Description("username")]
        Username,
        [Description("password")]
        Password,
        [Description("phone")]
        Phone,
        [Description("postalAddress")]
        PostalAddress,
        [Description("postalCode")]
        PostalCode
       
    }
}
