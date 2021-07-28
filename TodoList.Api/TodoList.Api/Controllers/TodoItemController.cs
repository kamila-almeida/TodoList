using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Application.Services.Interfaces;

namespace TodoList.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly IUserService _userService;

        public TodoItemController(ITodoItemService todoItemService, IUserService userService)
        {
            _todoItemService = todoItemService;
            _userService = userService;
        }

        [HttpGet]
        [Route("ListAll")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get(bool? delayedItems, int page = 1, int pageSize = 10)
        {
            var result = await _todoItemService.ListAsync(page, pageSize, delayedItems);
            return Ok(result);
        }

        [HttpGet]
        [Route("ListByUser")]
        [Authorize]
        public async Task<IActionResult> GetByUser()
        {
            var userId = _userService.GetUserId(User.FindFirst(ClaimTypes.Email)?.Value);
            var result = await _todoItemService.ListByUserAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] TodoItemRegisterModel todoItemRegisterModel)
        {
            if (todoItemRegisterModel is null)
            {
                return BadRequest();
            }

            var userId = _userService.GetUserId(User.FindFirst(ClaimTypes.Email)?.Value);
            var response = await _todoItemService.InsertAsync(todoItemRegisterModel, userId);
            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] TodoItemRegisterModel todoItemRegisterModel)
        {            
            if (todoItemRegisterModel is null)
            {
                return BadRequest();
            }

            var userId = _userService.GetUserId(User.FindFirst(ClaimTypes.Email)?.Value);
            var response = await _todoItemService.UpdateAsync(todoItemRegisterModel, userId);
            return Ok(response);
        }

        [HttpPut]
        [Route("FinishItem")]
        [Authorize]
        public async Task<IActionResult> FinishItem(int itemId)
        {
            var userId = _userService.GetUserId(User.FindFirst(ClaimTypes.Email)?.Value);
            var response = await _todoItemService.FinishItemAsync(itemId, userId);
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userService.GetUserId(User.FindFirst(ClaimTypes.Email)?.Value);
            var response = await _todoItemService.DeleteAsync(id, userId);
            return Ok(response);
        }
    }
}
