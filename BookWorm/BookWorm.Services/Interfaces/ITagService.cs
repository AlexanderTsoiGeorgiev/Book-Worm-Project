namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Tag;

    public interface ITagService
    {
        //Index
        public Task<IEnumerable<TagDisplayViewModel>> AllTagsAsDisplayModelAsync();
        public Task<IEnumerable<TagDisplayViewModel>> NonDeletedTagsAsDisplayModelAsync();

        //Add
        public Task AddTagAsync(TagFormViewModel model);

        //Delete
        public Task SoftDeleteAsync(int id);

        //Restore
        public Task RestoreAsync(int id);

        //Validation
        public Task<bool> TagExistsByNameAsync(string name);
        public Task<bool> TagExistsByIdAsync(int id);
        public Task<bool> IsTagDeleted(int id);

        //Utility
        public Task<IEnumerable<string>> AllTagNamesAsync();
    }
}
