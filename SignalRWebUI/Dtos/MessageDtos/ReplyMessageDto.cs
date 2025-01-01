namespace SignalRWebUI.Dtos.MessageDtos
{
    public class ReplyMessageDto
    {
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFile? Attachment { get; set; }
    }
}
