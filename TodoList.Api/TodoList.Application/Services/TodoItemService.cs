using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Application.Services.Interfaces;
using TodoList.Application.Validators;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;
using TodoList.Domain.Repositories;

namespace TodoList.Application.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IMapper _mapper;
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(IMapper mapper,
            ITodoItemRepository todoItemRepository)
        {
            _mapper = mapper;
            _todoItemRepository = todoItemRepository;
        }

    public async Task<BaseModel<TodoItemModel>> InsertAsync(TodoItemRegisterModel todoItemRegisterModel, int userId)
        {
            var validator = await new TodoItemValidator().ValidateAsync(todoItemRegisterModel);
            if (!validator.IsValid)
            {
                return new BaseModel<TodoItemModel>(false, validator.Errors);
            }

            var todoItem = _mapper.Map<TodoItem>(todoItemRegisterModel);
            todoItem.CreationDate = DateTime.Now;
            todoItem.UserId = userId;
            await _todoItemRepository.InsertAsync(todoItem);

            return new BaseModel<TodoItemModel>(true, EMessages.Success, _mapper.Map<TodoItemModel>(todoItem));
        }

        public async Task<BaseModel<TodoItemModel>> UpdateAsync(TodoItemRegisterModel todoItemRegisterModel, int userId)
        {
            var validator = await new TodoItemValidator().ValidateAsync(todoItemRegisterModel);
            if (!validator.IsValid)
            {
                return new BaseModel<TodoItemModel>(false, validator.Errors);
            }

            if(todoItemRegisterModel.Id == 0)
            {
                return new BaseModel<TodoItemModel>(false, EMessages.InvalidTodoItemId);
            }

            var todoItem = _mapper.Map<TodoItem>(await _todoItemRepository.GetById(todoItemRegisterModel.Id));

            if (todoItem.UserId != userId)
            {
                return new BaseModel<TodoItemModel>(false, EMessages.UserNotAuthorized);
            }

            if (todoItem.EndDate != null)
            {
                return new BaseModel<TodoItemModel>(false, EMessages.EditItemFinished);
            }

            todoItem.Description = todoItemRegisterModel.Description;
            todoItem.UpdateDate = DateTime.Now;
            await _todoItemRepository.UpdateAsync(todoItem);

            return new BaseModel<TodoItemModel>(true, EMessages.Success, _mapper.Map<TodoItemModel>(todoItem));
        }

        public async Task<BaseModel<TodoItemModel>> FinishItemAsync(int itemId, int userId)
        {
            if (itemId == 0)
            {
                return new BaseModel<TodoItemModel>(false, EMessages.InvalidTodoItemId);
            }

            var todoItem = _mapper.Map<TodoItem>(await _todoItemRepository.GetById(itemId));

            if (todoItem.UserId != userId)
            {
                return new BaseModel<TodoItemModel>(false, EMessages.UserNotAuthorized);
            }

            if (todoItem.EndDate != null)
            {
                return new BaseModel<TodoItemModel>(false, EMessages.ItemAlreadyFinished);
            }

            todoItem.EndDate = DateTime.Now;
            await _todoItemRepository.UpdateAsync(todoItem);

            return new BaseModel<TodoItemModel>(true, EMessages.Success, _mapper.Map<TodoItemModel>(todoItem));
        }

        public async Task<BaseModel<TodoItemModel>> DeleteAsync(int id, int userId)
        {
            var todoItem = _mapper.Map<TodoItem>(await _todoItemRepository.GetById(id));

            if (todoItem.UserId != userId)
            {
                return new BaseModel<TodoItemModel>(false, EMessages.UserNotAuthorized);
            }

            await _todoItemRepository.DeleteAsync(todoItem);
            return new BaseModel<TodoItemModel>(true, EMessages.Success);
        }

        public async Task<BaseModel<List<TodoItemResultModel>>> ListAsync(int page, int pageSize, bool? delayedItems)
        {
            var todoItem = _mapper.Map<List<TodoItem>>(await _todoItemRepository.ListAsync());
            todoItem = todoItem.Where(x => delayedItems == null || x.DueDate.Date < DateTime.Now.Date == delayedItems)
                .Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = _mapper.Map<List<TodoItemResultModel>>(todoItem);
            return new BaseModel<List<TodoItemResultModel>>(true, EMessages.Success, result);
        }

        public async Task<BaseModel<List<TodoItemModel>>> ListByUserAsync(int userId)
        {
            var result = _mapper.Map<List<TodoItemModel>>(await _todoItemRepository.ListByUserAsync(userId));
            result.ForEach(x => x.Delayed = x.DueDate.Date < DateTime.Now.Date);
            return new BaseModel<List<TodoItemModel>>(true, EMessages.Success, result);
        }
    }
}
