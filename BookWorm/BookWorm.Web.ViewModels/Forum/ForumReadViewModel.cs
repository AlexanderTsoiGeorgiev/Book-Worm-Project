namespace BookWorm.Web.ViewModels.Forum
{
    using System;
    using System.Collections.Generic;
    using BookWorm.Web.ViewModels.Reply;

    public class ForumReadViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string AuthorId { get; set; } = null!;

        public string AuthorName { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string TagName { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public IEnumerable<ReplyDisplayViewModel>? Replies { get; set; }
    }
}
