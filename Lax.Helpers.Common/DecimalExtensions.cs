namespace Lax.Helpers.Common {

    public static class DecimalExtensions {

        public static decimal Abs(this decimal value) => value > 0m ? value : decimal.Negate(value);

    }

}