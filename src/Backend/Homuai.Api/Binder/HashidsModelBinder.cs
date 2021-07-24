using HashidsNet;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Api.Binder
{
    /// <summary>
    /// 
    /// </summary>
    public class HashidsModelBinder : IModelBinder
    {
        private readonly IHashids hashids;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashids"></param>
        public HashidsModelBinder(IHashids hashids)
        {
            this.hashids = hashids ?? throw new System.ArgumentNullException(nameof(hashids));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext is null)
                throw new System.ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            var ids = hashids.DecodeLong(value);

            if (ids.Length == 0)
                return Task.CompletedTask;

            bindingContext.Result = ModelBindingResult.Success(ids.First());

            return Task.CompletedTask;
        }
    }
}
