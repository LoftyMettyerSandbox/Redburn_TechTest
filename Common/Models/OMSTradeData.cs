using NServiceBus;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    // Class properties borrowed from https://www.investopedia.com/terms/o/oms.asp
    // They've got two properties called order type - are they deliberately trying to confuse me?
    // may or may not align with real world examples

    public class OMSTradeData : IEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Identifier { get; set; }
        public int OrderType { get; set; }
        public int OrderSize { get; set; }
        public int OrderSubType { get; set; }
        public string Instruction { get; set; }
        public string OrderTransmissionType { get; set; }
    }
}
