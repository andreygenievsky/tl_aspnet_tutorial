namespace TodoRestApi.Dtos;

public class TodoItemDto
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
}
