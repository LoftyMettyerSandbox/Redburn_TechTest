using Common.Enums;
using NServiceBus;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class TradeMessage : IEvent
    {

        public TradeMessage(string message) {
            ReceivedDate = DateTime.Now;
            Message = message;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string Message { get; set; }
        public MessageValidityType ValidityType { get; set; }
        //public int TradesInMessage
        //{
        //    get { return Trades.Count; }
        //}
        //public List<OMSTradeData>Trades { get; set; }

    }
}
