using System.ComponentModel;

namespace mvc_shop.Models
{
    public class Comment
    {
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Id { get; set; }
        public string Text { get; set; }
        public long Timestamp { get; set; }

        public Comment(int id, string text, long timestamp)
        {
            Id = id;
            Text = text;
            Timestamp = timestamp;
        }
    }
}
