using System.ComponentModel;

namespace TodoList.Domain
{
    public enum EProfile
    {
        [Description("Administrator")]
        Administrator = 1,
        [Description("Client")]
        Client = 2
    }
}
