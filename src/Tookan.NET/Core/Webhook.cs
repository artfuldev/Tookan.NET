using System.Collections.Generic;

namespace Tookan.NET.Core
{
    public class Webhook
    {
        public string JobLatitude { get; set; }
        public string FleetEmail { get; set; }
        public string IsRouted { get; set; }
        public string JobType { get; set; }
        public string TeamId { get; set; }
        public string AutoAssignment { get; set; }
        public string JobDescription { get; set; }
        public string Timezone { get; set; }
        public string FleetRating { get; set; }
        public string UserId { get; set; }
        public string JobId { get; set; }
        public string JobState { get; set; }
        public string HasDelivery { get; set; }
        public string PickupDeliveryRelationship { get; set; }
        public string JobHash { get; set; }
        public string JobAddress { get; set; }
        public string JobPickupLatitude { get; set; }
        public string JobPickupName { get; set; }
        public string JobStatus { get; set; }
        public string SignImage { get; set; }
        public string CustomerUsername { get; set; }
        public string CustomerEmail { get; set; }
        public string OrderId { get; set; }
        public string CustomerComment { get; set; }
        public string CustomerPhone { get; set; }
        public string IsActive { get; set; }
        public string JobLongitude { get; set; }
        public string DispatcherId { get; set; }
        public string JobPickupLongitude { get; set; }
        public string IsCustomerRated { get; set; }
        public string CompletedByAdmin { get; set; }
        public string CustomerId { get; set; }
        public string TookanSharedSecret { get; set; }
        public string TotalDistanceTravelled { get; set; }
        public string TotalTimeSpentAtTaskTillCompletion { get; set; }
        public string SessionId { get; set; }
        public string HasPickup { get; set; }
        public string JobToken { get; set; }
        public string JobPickupAddress { get; set; }
        public string JobPickupPhone { get; set; }
        public string FleetId { get; set; }
        public string FleetName { get; set; }
        public string JobPickupEmail { get; set; }
        public List<CustomField> CustomField { get; set; }
        public List<TaskHistory> TaskHistory { get; set; }
    }
}