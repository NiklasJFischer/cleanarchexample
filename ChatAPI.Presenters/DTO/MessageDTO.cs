namespace ChatAPI.Presenters.DTO;

public class MessageDTO
{
    public string Id { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string Created { get; set; } = default!;
    public UserDTO Author { get; set; } = default!;
}
