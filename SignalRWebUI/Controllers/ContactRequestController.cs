using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.MailDtos;
using SignalRWebUI.Dtos.MessageDtos;
using System.Net.Http;

namespace SignalRWebUI.Controllers
{
    public class ContactRequestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactRequestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44303/api/Message");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44303/api/Message/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReplyContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44303/api/Message/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetMessageDto>(jsonData);
                ViewBag.ReceiverMail = values.Mail;
                ViewBag.Subject = values.Subject;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReplyContact(ReplyMessageDto replyMessageDto)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("SignalR Rezervasyon", "hhasanbozus0147@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", replyMessageDto.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = replyMessageDto.Body;

            mimeMessage.Subject = replyMessageDto.Subject;

            // Dosya ekleme kısmı
            if (replyMessageDto.Attachment != null && replyMessageDto.Attachment.Length > 0)
            {

                // Dosya içeriğini okuma
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    replyMessageDto.Attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                // MimePart oluşturma
                var attachmentPart = new MimePart
                {
                    Content = new MimeContent(new MemoryStream(fileBytes)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = replyMessageDto.Attachment.FileName
                };

                // MessageBody'ye ekleme
                bodyBuilder.Attachments.Add(attachmentPart);

            }
            mimeMessage.Body = bodyBuilder.ToMessageBody();


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("hhasanbozkus0147@gmail.com", "loqnnbwbxkyzfgbn");
            client.Send(mimeMessage);
            client.Disconnect(true);

            return RedirectToAction("Index");
        }
    }
}
