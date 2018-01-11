namespace TodoApp.Api.Models
{
    public class ItemModel
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public override string ToString() 
            => $"{nameof(Id)}: {Id}, {nameof(Text)}: {Text}";
    }
}