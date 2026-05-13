using System;

namespace Models
{
    public class DeliveryRequest
    {
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string SenderEmail { get; set; }
        public string PickupAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public string DeliveryAddress { get; set; }
        public string PetDescription { get; set; }
        public string Status { get; set; }
    }
}