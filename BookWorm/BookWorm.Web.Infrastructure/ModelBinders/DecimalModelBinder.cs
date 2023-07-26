namespace BookWorm.Web.Infrastructure.ModelBinders
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}
