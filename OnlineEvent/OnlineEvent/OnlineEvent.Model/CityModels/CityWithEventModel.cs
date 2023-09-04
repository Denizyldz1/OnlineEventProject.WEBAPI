using OnlineEvent.Core.Entities;
using OnlineEvent.Model.EventModels;

namespace OnlineEvent.Model.CityModels
{
    public class CityWithEventModel : CityModel
    {
        public List<EventModel>? Events { get; set; }

    }
}
