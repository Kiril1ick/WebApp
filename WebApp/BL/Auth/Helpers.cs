using System.Transactions;

namespace WebApp.BL.Auth
{
    public class Helpers
    {
        public static int? StringToIntDef(string str, int? def)
        {
            int value;
            if (int.TryParse(str, out value))
                return value;
            return def;
        }

        public static TransactionScope CreateTransactionScope(int second = 60000)
        {
            return new TransactionScope(
                TransactionScopeOption.Required, new TimeSpan(0, 0, second), TransactionScopeAsyncFlowOption.Enabled);
        }

        internal static Guid? StringToGuidDef(string str)
        {
            Guid value;
            if (Guid.TryParse(str, out value))
                return value;
            return null;
        }
    }
}
