namespace TodoRestApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
    public string? SecretField { get; set; }
}
