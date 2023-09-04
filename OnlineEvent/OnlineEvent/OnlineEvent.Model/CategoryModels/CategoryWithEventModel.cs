using OnlineEvent.Model.EventModels;

namespace OnlineEvent.Model.CategoryModels

{
    public class CategoryWithEventModel : CategoryModel
    {
        public List<EventModel>? Events { get; set; }
    }
}
