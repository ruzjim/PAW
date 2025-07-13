namespace MVC.Models;
// https://chatgpt.com/share/685b49bb-c294-8003-adb3-fe1e1138a55d
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
