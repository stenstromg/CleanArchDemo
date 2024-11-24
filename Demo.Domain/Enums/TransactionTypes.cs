using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Enums
{
    public enum TransactionTypes
    {
        None            = 0,
        DEBIT           = 1,
        TRANSFER        = 2,
        DIRECT_DEPOSIT  = 3,
        OTHER           = 4,
        REFUND          = 5,
        DEPOSIT         = 6
    }
}
