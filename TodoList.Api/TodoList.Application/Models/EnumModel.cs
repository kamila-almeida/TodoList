using System;
using TodoList.Shared;

namespace TodoList.Application.Models
{
    public class EnumModel
    {
        public EnumModel()
        {
        }

        public EnumModel(Enum enumItem)
        {
            this.Value = enumItem.GetEnumValue();
            this.Name = enumItem.GetEnumName();
            this.Description = enumItem.GetEnumDescription();
        }

        public int Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
