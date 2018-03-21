using System;

namespace TodoApp.Contract.Models
{
    public class Item
    {
        public Item()
        {
            CreatedAt = DateTime.Now;
            LastChange = CreatedAt;
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastChange { get; set; }

        public override string ToString()
            => $"{nameof(Id)}: {Id}, {nameof(Text)}: {Text}";
    }
}
