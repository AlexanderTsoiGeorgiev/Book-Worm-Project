namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Tag;

    public interface ITagService
    {
        //Index
        public Task<IEnumerable<TagDisplayViewModel>> AllTagsAsDisplayModelAsync();

        //Add
        public Task AddTagAsync(TagFormViewModel model);

        //Delete
        public Task SoftDeleteAsync(int id);

        //Restore
        public Task RestoreAsync(int id);

        //Validation
        public Task<bool> TagExistsByNameAsync(string name);
        public Task<bool> TagExistsByIdAsync(int id);
    }
}
