using TodoRestApi.Utils;

namespace TodoRestApi.Dtos;

public class TodoItemPatchDto : PatchDtoBase
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
}