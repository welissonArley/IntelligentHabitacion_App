using Homuai.Api.Binder;
using Homuai.Api.Filter.Authentication;
using Homuai.Application.UseCases.MyFoods.ChangeQuantityOfOneProduct;
using Homuai.Application.UseCases.MyFoods.DeleteMyFood;
using Homuai.Application.UseCases.MyFoods.GetMyFoods;
using Homuai.Application.UseCases.MyFoods.RegisterMyFood;
using Homuai.Application.UseCases.MyFoods.UpdateMyFood;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
    public class MyFoodController : BaseController
    {
        /// <summary>
        /// This function will return the list of foods
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseMyFoodJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MyFoods([FromServices] IGetMyFoodsUseCase useCase)
        {
            var response = await useCase.Execute();
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will add one food and return the Id
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="requestMyFood"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFood(
            [FromServices] IRegisterMyFoodUseCase useCase,
            [FromBody] RequestProductJson requestMyFood)
        {
            var response = await useCase.Execute(requestMyFood);
            WriteAutenticationHeader(response);

            return Created(string.Empty, response.ResponseJson);
        }

        /// <summary>
        /// This function will delete one user's food
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFoods(
            [FromServices] IDeleteMyFoodUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
        {
            var response = await useCase.Execute(id);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will change the quantity of one product. If the quantity is less or equals 0, the product will be deleted
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-quantity/{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeQuantity(
            [FromServices] IChangeQuantityOfOneProductUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
            [FromBody] RequestChangeQuantityMyFoodJson request)
        {
            var response = await useCase.Execute(id, request.Amount);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function do an update in the product
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="editMyFood"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditFood(
            [FromServices] IUpdateMyFoodUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
            RequestProductJson editMyFood)
        {
            var response = await useCase.Execute(id, editMyFood);
            WriteAutenticationHeader(response);

            return Ok();
        }
    }
}