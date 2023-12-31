﻿namespace BookWorm.Web.ViewModels.Book
{
    public class BookDisplayViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
