using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using TodoList.Domain.Enums;
using TodoList.Shared;

namespace TodoList.Application.Models
{
    public class BaseModel<T> where T : class
    {
        public BaseModel()
        {
        }

        public BaseModel(bool success, EMessages message)
        {
            Success = success;
            Messages = new EnumModel[]
            {
                new EnumModel() {
                    Value = message.GetEnumValue(),
                    Name = message.GetEnumName(),
                    Description = message.GetEnumDescription()
                }
            };
        }

        public BaseModel(bool success, IList<ValidationFailure> validationErrors)
        {
            Success = success;
            Messages = validationErrors.Select(x => new EnumModel
            {
                Value = 99,
                Name = x.PropertyName,
                Description = x.ErrorMessage
            }).ToArray();
        }

        public BaseModel(bool success, EMessages message, T data) : this(success, message) => Data = data;
        public T Data { get; set; }
        public EnumModel[] Messages { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool Success { get; set; }
    }
}
